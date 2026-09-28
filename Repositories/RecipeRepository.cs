using Google.Cloud.Firestore;

public class RecipeRepository : IRecipeRepository
{
    private const string CollectionName = "recipes";
    private readonly FirestoreDb _db;

    public RecipeRepository(FirestoreDb db)
    {
        _db = db;
    }

    public async Task<List<Recipe>> GetAllAsync()
    {
        var snapshot = await _db.Collection(CollectionName).GetSnapshotAsync();
        return snapshot.Documents.Select(d => d.ConvertTo<Recipe>()).ToList();
    }

    public async Task<Recipe?> GetByIdAsync(string id)
    {
        var doc = await _db.Collection(CollectionName).Document(id).GetSnapshotAsync();
        return doc.Exists ? doc.ConvertTo<Recipe>() : null;
    }

    public async Task CreateAsync(Recipe recipe)
    {
        var docRef = _db.Collection(CollectionName).Document();
        recipe.RecipeId = docRef.Id;
        await docRef.SetAsync(recipe);
    }

    public async Task UpdateAsync(Recipe recipe)
    {
        await _db.Collection(CollectionName)
            .Document(recipe.RecipeId)
            .SetAsync(recipe, SetOptions.Overwrite);
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