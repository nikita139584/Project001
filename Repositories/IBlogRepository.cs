public interface IBlogRepository
{
    Task<List<Blog>> GetAllAsync();
    Task<Blog?> GetByIdAsync(string id);
    Task CreateAsync(Blog blog);
    Task UpdateAsync(Blog blog);
    Task DeleteAsync(string id);
    Task<bool> ExistsAsync(string id);
}