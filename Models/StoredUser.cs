namespace Project001.Models;

/// <summary>
/// Данные пользователя, прочитанные из Firestore для проверки входа.
/// </summary>
public sealed record StoredUser(
    string Id,
    string Name,
    string Email,
    string PasswordHash);
