<<<<<<< HEAD
﻿using Google.Cloud.Firestore;

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
=======
﻿namespace Project001.Models.Entities
{
    public class Blog
    {
    }
}
>>>>>>> b7ea5945b9b0ae07e3c87b3691ee5691d7ecb501
