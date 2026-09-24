<<<<<<< HEAD
﻿public interface IRatingService
{
    Task<List<Rating>> GetAllAsync();
    Task<Rating?> GetByIdAsync(string id);
    Task<List<Rating>> GetByRecipeIdAsync(string recipeId);
    Task CreateAsync(Rating rating);
    Task UpdateAsync(Rating rating);
    Task DeleteAsync(string id);
    Task<bool> ExistsAsync(string id);
}
=======
﻿namespace Project001.Services
{
    public interface IRatingService
    {
    }
}
>>>>>>> b7ea5945b9b0ae07e3c87b3691ee5691d7ecb501
