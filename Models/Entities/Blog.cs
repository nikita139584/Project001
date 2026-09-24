using Google.Cloud.Firestore;

[FirestoreData]
public class Blog
{
    [FirestoreDocumentId]
    public string BlogId { get; set; } = string.Empty;

    [FirestoreProperty]
    public string Title { get; set; } = string.Empty;

    [FirestoreProperty]
    public string Description { get; set; } = string.Empty;

    [FirestoreProperty]
    public DateTime CreatedAt { get; set; }
}