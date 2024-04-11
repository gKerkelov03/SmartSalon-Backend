
using SmartSalon.Application.Abstractions.Lifetime;

namespace SmartSalon.Application.Abstractions.Services;

public interface IEncryptionHelper : ISingletonLifetime
{
    string Encrypt<TModel>(TModel model, string encryptionKey);
    TModel? DecryptTo<TModel>(string textToDecrypt, string encryptionKey);
}