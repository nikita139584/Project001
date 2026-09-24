using Google.Cloud.Firestore;

[FirestoreData]
public class Comment
{
    [FirestoreDocumentId]
    public string CommentId { get; set; } = string.Empty;

    [FirestoreProperty]
    public string PostId { get; set; } = string.Empty;

    [FirestoreProperty]
    public string Author { get; set; } = string.Empty;

    [FirestoreProperty]
    public string Content { get; set; } = string.Empty;

    [FirestoreProperty]
    public DateTime CreatedAt { get; set; }
}