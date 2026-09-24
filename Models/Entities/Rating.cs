using Google.Cloud.Firestore;

[FirestoreData]
public class Rating
{
    [FirestoreDocumentId]
    public string RatingId { get; set; } = string.Empty;

    [FirestoreProperty]
    public string RecipeId { get; set; } = string.Empty;

    [FirestoreProperty]
    public int UserId { get; set; }

    [FirestoreProperty]
    public int Value { get; set; }

    [FirestoreProperty]
    public DateTime CreatedAt { get; set; }
}