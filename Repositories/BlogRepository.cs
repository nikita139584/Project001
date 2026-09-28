using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Identity;

public class BlogRepository : IBlogRepository
{
    private const string CollectionName = "blogs";
    private readonly FirestoreDb _db;

    public BlogRepository(FirestoreDb db) => _db = db;

    public async Task<List<Blog>> GetAllAsync()
    {
        var snapshot = await _db.Collection(CollectionName).GetSnapshotAsync();
        return snapshot.Documents.Select(d => d.ConvertTo<Blog>()).ToList();
    }

    public async Task<Blog?> GetByIdAsync(string id)
    {
        var doc = await _db.Collection(CollectionName).Document(id).GetSnapshotAsync();
        return doc.Exists ? doc.ConvertTo<Blog>() : null;
    }

    public async Task CreateAsync(Blog blog)
    {
        var docRef = _db.Collection(CollectionName).Document();
        blog.BlogId = docRef.Id;
        await docRef.SetAsync(blog);
    }

    public async Task UpdateAsync(Blog blog)
    {
        await _db.Collection(CollectionName)
            .Document(blog.BlogId)
            .SetAsync(blog, SetOptions.Overwrite);
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