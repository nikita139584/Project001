<<<<<<< HEAD
﻿public interface ICommentService
{
    Task<List<Comment>> GetAllAsync();
    Task<Comment?> GetByIdAsync(string id);
    Task<List<Comment>> GetByPostIdAsync(string postId);
    Task CreateAsync(Comment comment);
    Task UpdateAsync(Comment comment);
    Task DeleteAsync(string id);
    Task<bool> ExistsAsync(string id);
}
=======
﻿namespace Project001.Services
{
    public interface ICommentService
    {
    }
}
>>>>>>> b7ea5945b9b0ae07e3c87b3691ee5691d7ecb501
