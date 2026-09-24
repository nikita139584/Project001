public class RatingService : IRatingService
{
    private readonly IRatingRepository _repository;

    public RatingService(IRatingRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Rating>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Rating?> GetByIdAsync(string id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<List<Rating>> GetByRecipeIdAsync(string recipeId)
    {
        return await _repository.GetByRecipeIdAsync(recipeId);
    }

    public async Task CreateAsync(Rating rating)
    {
        await _repository.CreateAsync(rating);
    }

    public async Task UpdateAsync(Rating rating)
    {
        await _repository.UpdateAsync(rating);
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