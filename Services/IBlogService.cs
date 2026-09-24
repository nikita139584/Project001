<<<<<<< HEAD
﻿public interface IBlogService
{
    Task<List<Blog>> GetAllAsync();
    Task<Blog?> GetByIdAsync(string id);
    Task CreateAsync(Blog blog);
    Task UpdateAsync(Blog blog);
    Task DeleteAsync(string id);
    Task<bool> ExistsAsync(string id);
}
=======
﻿namespace Project001.Services
{
    public interface IBlogService
    {
    }
}
>>>>>>> b7ea5945b9b0ae07e3c87b3691ee5691d7ecb501
