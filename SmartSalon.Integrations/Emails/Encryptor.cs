using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using SmartSalon.Application.Abstractions.Services;

namespace SmartSalon.Application.Services;

public class Encryptor : IEncryptor
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
}