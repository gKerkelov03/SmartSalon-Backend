
using SmartSalon.Application.Abstractions.Lifetime;

namespace SmartSalon.Application.Abstractions.Services;

public interface IDecryptor : ISingletonLifetime
{
    TModel? DecryptTo<TModel>(string textToDecrypt, string encryptionKey) where TModel : class;
}