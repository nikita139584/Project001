public class RecipeService : IRecipeService
{
    private readonly IRecipeRepository _repository;

    public RecipeService(IRecipeRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Recipe>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Recipe?> GetByIdAsync(string id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task CreateAsync(Recipe recipe)
    {
        await _repository.CreateAsync(recipe);
    }

    public async Task UpdateAsync(Recipe recipe)
    {
        await _repository.UpdateAsync(recipe);
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