using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Identity;

public class CommentRepository : ICommentRepository
{
    private const string CollectionName = "comments";
    private readonly FirestoreDb _db;

    public CommentRepository(FirestoreDb db) => _db = db;

    public async Task<List<Comment>> GetAllAsync()
    {
        var snapshot = await _db.Collection(CollectionName).GetSnapshotAsync();
        return snapshot.Documents.Select(d => d.ConvertTo<Comment>()).ToList();
    }

    public async Task<Comment?> GetByIdAsync(string id)
    {
        var doc = await _db.Collection(CollectionName).Document(id).GetSnapshotAsync();
        return doc.Exists ? doc.ConvertTo<Comment>() : null;
    }

    public async Task<List<Comment>> GetByPostIdAsync(string postId)
    {
        var query = _db.Collection(CollectionName).WhereEqualTo("PostId", postId);
        var snapshot = await query.GetSnapshotAsync();
        return snapshot.Documents.Select(d => d.ConvertTo<Comment>()).ToList();
    }

    public async Task CreateAsync(Comment comment)
    {
        var docRef = _db.Collection(CollectionName).Document();
        comment.CommentId = docRef.Id;
        await docRef.SetAsync(comment);
    }

    public async Task UpdateAsync(Comment comment)
    {
        await _db.Collection(CollectionName)
            .Document(comment.CommentId)
            .SetAsync(comment, SetOptions.Overwrite);
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