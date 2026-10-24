using Project001.Models;

namespace Project001.Services;

public interface IRegistrationStore
{
    Task<bool> EmailExistsAsync(string email, CancellationToken ct);

    Task<string> SaveAsync(
        RegistrationViewModel model,
        CancellationToken ct);

    /// <summary>
    /// Ищет пользователя по почте для проверки входа.
    /// Возвращает null, если такого нет.
    /// </summary>
    Task<StoredUser?> FindByEmailAsync(string email, CancellationToken ct);
}
