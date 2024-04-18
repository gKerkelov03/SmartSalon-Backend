using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using SmartSalon.Application.Abstractions.Services;

namespace SmartSalon.Application.Services;

//TODO: extract to IEncryptor and IDecryptor
public class EncryptionHelper : IEncryptionHelper
{
    public string Encrypt<TModel>(TModel model, string key)
    {
        var textToEncrypt = JsonConvert.SerializeObject(model);

        using Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(key);

        aes.GenerateIV();

        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        using var memoryStream = new MemoryStream();
        using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

        var encryptedBytes = Encoding.UTF8.GetBytes(textToEncrypt);
        cryptoStream.Write(encryptedBytes, 0, encryptedBytes.Length);

        cryptoStream.FlushFinalBlock();

        var ciphertext = memoryStream.ToArray();
        var ivAndCiphertext = aes.IV.Concat(ciphertext).ToArray();

        return Convert.ToBase64String(ivAndCiphertext);
    }

    public TModel? DecryptTo<TModel>(string encryptedText, string key) where TModel : class
    {
        try
        {
            encryptedText = encryptedText.Replace(' ', '+');
            var ivAndCiphertext = Convert.FromBase64String(encryptedText);

            var iv = ivAndCiphertext.Take(16).ToArray();
            var ciphertext = ivAndCiphertext.Skip(16).ToArray();

            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;
            aes.Padding = PaddingMode.PKCS7;

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var memoryStream = new MemoryStream(ciphertext);

            using var reader = new StreamReader(new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read), Encoding.UTF8);
            var json = reader.ReadToEnd();

            return JsonConvert.DeserializeObject<TModel>(json);
        }
        catch
        {
            return null;
        }
    }
}