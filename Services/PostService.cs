

public class PostService : IPostService
{
    private readonly IPostRepository _repository;

    public PostService(IPostRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Post>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Post?> GetByIdAsync(string id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<List<Post>> GetByBlogIdAsync(string blogId)
    {
        return await _repository.GetByBlogIdAsync(blogId);
    }

    public async Task CreateAsync(Post post)
    {
        await _repository.CreateAsync(post);
    }

    public async Task UpdateAsync(Post post)
    {
        await _repository.UpdateAsync(post);
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