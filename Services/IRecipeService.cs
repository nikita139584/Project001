<<<<<<< HEAD
﻿public interface IRecipeService
{
    Task<List<Recipe>> GetAllAsync();
    Task<Recipe?> GetByIdAsync(string id);
    Task CreateAsync(Recipe recipe);
    Task UpdateAsync(Recipe recipe);
    Task DeleteAsync(string id);
    Task<bool> ExistsAsync(string id);
}
=======
﻿namespace Project001.Services
{
    public interface IRecipeService
    {
    }
}
>>>>>>> b7ea5945b9b0ae07e3c87b3691ee5691d7ecb501
