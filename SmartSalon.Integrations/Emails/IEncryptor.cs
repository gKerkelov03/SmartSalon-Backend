
using SmartSalon.Application.Abstractions.Lifetime;

namespace SmartSalon.Application.Abstractions.Services;

public interface IEncryptor : ISingletonLifetime
{
    string Encrypt<TModel>(TModel model, string encryptionKey);
}