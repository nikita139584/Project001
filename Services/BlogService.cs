<<<<<<< HEAD
﻿public class BlogService : IBlogService
{
    private readonly IBlogRepository _repository;

    public BlogService(IBlogRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Blog>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Blog?> GetByIdAsync(string id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task CreateAsync(Blog blog)
    {
        await _repository.CreateAsync(blog);
    }

    public async Task UpdateAsync(Blog blog)
    {
        await _repository.UpdateAsync(blog);
    }

    public async Task DeleteAsync(string id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<bool> ExistsAsync(string id)
    {
        return await _repository.ExistsAsync(id);
    }
}
=======
﻿namespace Project001.Services
{
    public class BlogService
    {
    }
}
>>>>>>> b7ea5945b9b0ae07e3c87b3691ee5691d7ecb501
