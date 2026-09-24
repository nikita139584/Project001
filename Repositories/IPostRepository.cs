public interface IPostRepository
{
    Task<List<Post>> GetAllAsync();
    Task<Post?> GetByIdAsync(string id);
    Task<List<Post>> GetByBlogIdAsync(string blogId);
    Task CreateAsync(Post post);
    Task UpdateAsync(Post post);
    Task DeleteAsync(string id);
    Task<bool> ExistsAsync(string id);
}