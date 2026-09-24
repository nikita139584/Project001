<<<<<<< HEAD
﻿using Google.Cloud.Firestore;

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
=======
﻿namespace Project001.Models.Entities
{
    public class Post
    {
    }
}
>>>>>>> b7ea5945b9b0ae07e3c87b3691ee5691d7ecb501
