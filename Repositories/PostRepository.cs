using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Identity;

public class PostRepository : IPostRepository
{
    private const string CollectionName = "posts";
    private readonly FirestoreDb _db;

    public PostRepository(FirestoreDb db) => _db = db;

    public async Task<List<Post>> GetAllAsync()
    {
        var snapshot = await _db.Collection(CollectionName).GetSnapshotAsync();
        return snapshot.Documents.Select(d => d.ConvertTo<Post>()).ToList();
    }

    public async Task<Post?> GetByIdAsync(string id)
    {
        var doc = await _db.Collection(CollectionName).Document(id).GetSnapshotAsync();
        return doc.Exists ? doc.ConvertTo<Post>() : null;
    }

    public async Task<List<Post>> GetByBlogIdAsync(string blogId)
    {
        var query = _db.Collection(CollectionName).WhereEqualTo("BlogId", blogId);
        var snapshot = await query.GetSnapshotAsync();
        return snapshot.Documents.Select(d => d.ConvertTo<Post>()).ToList();
    }

    public async Task CreateAsync(Post post)
    {
        var docRef = _db.Collection(CollectionName).Document();
        post.PostId = docRef.Id;
        await docRef.SetAsync(post);
    }

    public async Task UpdateAsync(Post post)
    {
        await _db.Collection(CollectionName)
            .Document(post.PostId)
            .SetAsync(post, SetOptions.Overwrite);
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