<<<<<<< HEAD
﻿using Google.Cloud.Firestore;

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
=======
﻿namespace Project001.Models.Entities
{
    public class Comment
    {
    }
}
>>>>>>> b7ea5945b9b0ae07e3c87b3691ee5691d7ecb501
