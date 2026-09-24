public interface IRatingService
{
    Task<List<Rating>> GetAllAsync();
    Task<Rating?> GetByIdAsync(string id);
    Task<List<Rating>> GetByRecipeIdAsync(string recipeId);
    Task CreateAsync(Rating rating);
    Task UpdateAsync(Rating rating);
    Task DeleteAsync(string id);
    Task<bool> ExistsAsync(string id);
}