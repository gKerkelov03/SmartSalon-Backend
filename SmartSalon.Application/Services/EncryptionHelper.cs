using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using SmartSalon.Application.Abstractions.Services;

namespace SmartSalon.Application.Services;

public class EncryptionHelper : IEncryptionHelper
{
    public string Encrypt<TModel>(TModel model, string encryptionKey)
    {
        var textToEncrypt = JsonConvert.SerializeObject(model);
        using var Aes = System.Security.Cryptography.Aes.Create();

        Aes.Key = Sha256(encryptionKey);
        Aes.GenerateIV();

        var encryptor = Aes.CreateEncryptor(Aes.Key, Aes.IV);

        using var memoryStream = new MemoryStream();
        using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        using var streamWrite = new StreamWriter(cryptoStream);

        streamWrite.Write(textToEncrypt);

        var encryptedBytes = memoryStream.ToArray();
        var result = new byte[Aes.IV.Length + encryptedBytes.Length];

        Buffer.BlockCopy(Aes.IV, 0, result, 0, Aes.IV.Length);
        Buffer.BlockCopy(encryptedBytes, 0, result, Aes.IV.Length, encryptedBytes.Length);

        return Convert.ToBase64String(result);
    }

    public TModel? DecryptTo<TModel>(string encryptedText, string encryptionKey)
    {
        byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

        using var Aes = System.Security.Cryptography.Aes.Create();

        Aes.Key = Sha256(encryptionKey);
        byte[] iv = new byte[Aes.IV.Length];
        Buffer.BlockCopy(encryptedBytes, 0, iv, 0, iv.Length);
        Aes.IV = iv;

        var decryptor = Aes.CreateDecryptor(Aes.Key, Aes.IV);
        using var memoryStream = new MemoryStream();
        using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write);

        cryptoStream.Write(encryptedBytes, iv.Length, encryptedBytes.Length - iv.Length);
        cryptoStream.FlushFinalBlock();

        var json = Encoding.UTF8.GetString(memoryStream.ToArray());
        return JsonConvert.DeserializeObject<TModel>(json);
    }

    private byte[] Sha256(string input)
    {
        using SHA256 hashAlgorithm = SHA256.Create();
        return hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
    }
}
