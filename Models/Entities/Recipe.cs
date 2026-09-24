using Google.Cloud.Firestore;

[FirestoreData]
public class Recipe
{
    [FirestoreDocumentId]
    public string RecipeId { get; set; } = string.Empty;

    [FirestoreProperty]
    public string Title { get; set; } = string.Empty;

    [FirestoreProperty]
    public string Description { get; set; } = string.Empty;

    [FirestoreProperty]
    public string Ingredients { get; set; } = string.Empty;

    [FirestoreProperty]
    public string Instructions { get; set; } = string.Empty;

    [FirestoreProperty]
    public DateTime CreatedAt { get; set; }
}