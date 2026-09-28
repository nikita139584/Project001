using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Identity;

public class RatingRepository : IRatingRepository
{
    private const string CollectionName = "ratings";
    private readonly FirestoreDb _db;

    public RatingRepository(FirestoreDb db) => _db = db;

    public async Task<List<Rating>> GetAllAsync()
    {
        var snapshot = await _db.Collection(CollectionName).GetSnapshotAsync();
        return snapshot.Documents.Select(d => d.ConvertTo<Rating>()).ToList();
    }

    public async Task<Rating?> GetByIdAsync(string id)
    {
        var doc = await _db.Collection(CollectionName).Document(id).GetSnapshotAsync();
        return doc.Exists ? doc.ConvertTo<Rating>() : null;
    }

    public async Task<List<Rating>> GetByRecipeIdAsync(string recipeId)
    {
        var query = _db.Collection(CollectionName).WhereEqualTo("RecipeId", recipeId);
        var snapshot = await query.GetSnapshotAsync();
        return snapshot.Documents.Select(d => d.ConvertTo<Rating>()).ToList();
    }

    public async Task CreateAsync(Rating rating)
    {
        var docRef = _db.Collection(CollectionName).Document();
        rating.RatingId = docRef.Id;
        await docRef.SetAsync(rating);
    }

    public async Task UpdateAsync(Rating rating)
    {
        await _db.Collection(CollectionName)
            .Document(rating.RatingId)
            .SetAsync(rating, SetOptions.Overwrite);
    }

    public async Task DeleteAsync(string id)
    {
        await _db.Collection(CollectionName).Document(id).DeleteAsync();
    }

    public async Task<bool> ExistsAsync(string id)
    {
        var doc = await _db.Collection(CollectionName).Document(id).GetSnapshotAsync();
        return doc.Exists;
    }
}