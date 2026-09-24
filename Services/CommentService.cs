<<<<<<< HEAD
﻿public class CommentService : ICommentService
{
    private readonly ICommentRepository _repository;

    public CommentService(ICommentRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Comment>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Comment?> GetByIdAsync(string id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<List<Comment>> GetByPostIdAsync(string postId)
    {
        return await _repository.GetByPostIdAsync(postId);
    }

    public async Task CreateAsync(Comment comment)
    {
        await _repository.CreateAsync(comment);
    }

    public async Task UpdateAsync(Comment comment)
    {
        await _repository.UpdateAsync(comment);
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
    public class CommentService
    {
    }
}
>>>>>>> b7ea5945b9b0ae07e3c87b3691ee5691d7ecb501
