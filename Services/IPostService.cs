<<<<<<< HEAD
﻿public interface IPostService
{
    Task<List<Post>> GetAllAsync();
    Task<Post?> GetByIdAsync(string id);
    Task<List<Post>> GetByBlogIdAsync(string blogId);
    Task CreateAsync(Post post);
    Task UpdateAsync(Post post);
    Task DeleteAsync(string id);
    Task<bool> ExistsAsync(string id);
}
=======
﻿namespace Project001.Services
{
    public interface IPostService
    {
    }
}
>>>>>>> b7ea5945b9b0ae07e3c87b3691ee5691d7ecb501
