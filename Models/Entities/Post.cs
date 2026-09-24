using Google.Cloud.Firestore;

[FirestoreData]
public class Post
{
    [FirestoreDocumentId]
    public string PostId { get; set; } = string.Empty;

    [FirestoreProperty]
    public string BlogId { get; set; } = string.Empty; // вместо навигации Blog

    [FirestoreProperty]
    public string Title { get; set; } = string.Empty;

    [FirestoreProperty]
    public string Content { get; set; } = string.Empty;

    [FirestoreProperty]
    public DateTime CreatedAt { get; set; }
}