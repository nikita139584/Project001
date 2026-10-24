
using Google.Cloud.Firestore;
using Project001.Models;

namespace Project001.Services;

public class RegistrationRepository : IRegistrationStore
{
    private readonly FirestoreDb _firestore;
    private const string CollectionName = "registrations";

    public RegistrationRepository(FirestoreDb firestore)
    {
        _firestore = firestore;
    }

    public async Task<bool> EmailExistsAsync(
        string email,
        CancellationToken ct)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();

        var query = _firestore
            .Collection(CollectionName)
            .WhereEqualTo("EmailNormalized", normalizedEmail)
            .Limit(1);

        var snapshot = await query.GetSnapshotAsync(ct);

        return snapshot.Count > 0;
    }

    public async Task<string> SaveAsync(
        RegistrationViewModel model,
        CancellationToken ct)
    {
        var normalizedEmail = model.Email.Trim().ToLowerInvariant();

        var data = new Dictionary<string, object>
        {
            ["Name"] = model.Name.Trim(),
            ["Email"] = model.Email.Trim(),
            ["EmailNormalized"] = normalizedEmail,
            ["PasswordHash"] = PasswordHasher.Hash(model.Password),
            ["Phone"] = model.Phone.Trim(),
            ["Country"] = model.Country,
            ["CreatedAt"] = Timestamp.GetCurrentTimestamp()
        };

        if (model.BirthDate.HasValue)
        {
            data["BirthDate"] = Timestamp.FromDateTime(
                DateTime.SpecifyKind(
                    model.BirthDate.Value.Date,
                    DateTimeKind.Utc));
        }

        var document = await _firestore
            .Collection(CollectionName)
            .AddAsync(data, ct);

        return document.Id;
    }

    public async Task<StoredUser?> FindByEmailAsync(
        string email,
        CancellationToken ct)
    {
        var normalizedEmail = email.Trim().ToLowerInvariant();

        var query = _firestore
            .Collection(CollectionName)
            .WhereEqualTo("EmailNormalized", normalizedEmail)
            .Limit(1);

        var snapshot = await query.GetSnapshotAsync(ct);
        var document = snapshot.Documents.FirstOrDefault();

        if (document is null)
        {
            return null;
        }

        // Записи, созданные до появления паролей, остаются без PasswordHash —
        // войти по ним нельзя, но и падать на них не нужно.
        var name = document.ContainsField("Name")
            ? document.GetValue<string>("Name")
            : "";

        var storedEmail = document.ContainsField("Email")
            ? document.GetValue<string>("Email")
            : email.Trim();

        var passwordHash = document.ContainsField("PasswordHash")
            ? document.GetValue<string>("PasswordHash")
            : "";

        return new StoredUser(document.Id, name, storedEmail, passwordHash);
    }
}

