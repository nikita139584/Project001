public interface ICommentRepository
{
    Task<List<Comment>> GetAllAsync();
    Task<Comment?> GetByIdAsync(string id);
    Task<List<Comment>> GetByPostIdAsync(string postId);
    Task CreateAsync(Comment comment);
    Task UpdateAsync(Comment comment);
    Task DeleteAsync(string id);
    Task<bool> ExistsAsync(string id);
}