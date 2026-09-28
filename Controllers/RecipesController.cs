using Microsoft.AspNetCore.Mvc;

public class RecipesController : Controller
{
    private readonly IRecipeService _recipeService;

    public RecipesController(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    // GET: Recipes
    public async Task<IActionResult> Index()
    {
        return View(await _recipeService.GetAllAsync());
    }

    // GET: Recipes/Details/id
    public async Task<IActionResult> Details(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recipe = await _recipeService.GetByIdAsync(id);

        if (recipe == null)
        {
            return NotFound();
        }

        return View(recipe);
    }

    // GET: Recipes/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Recipes/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Recipe recipe)
    {
        if (ModelState.IsValid)
        {
            await _recipeService.CreateAsync(recipe);
            return RedirectToAction(nameof(Index));
        }

        return View(recipe);
    }

    // GET: Recipes/Edit/id
    public async Task<IActionResult> Edit(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recipe = await _recipeService.GetByIdAsync(id);

        if (recipe == null)
        {
            return NotFound();
        }

        return View(recipe);
    }

    // POST: Recipes/Edit/id
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, Recipe recipe)
    {
        if (id != recipe.RecipeId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _recipeService.UpdateAsync(recipe);
            return RedirectToAction(nameof(Index));
        }

        return View(recipe);
    }

    // GET: Recipes/Delete/id
    public async Task<IActionResult> Delete(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recipe = await _recipeService.GetByIdAsync(id);

        if (recipe == null)
        {
            return NotFound();
        }

        return View(recipe);
    }

    // POST: Recipes/Delete/id
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var recipe = await _recipeService.GetByIdAsync(id);

        if (recipe != null)
        {
            await _recipeService.DeleteAsync(id);
        }

        return RedirectToAction(nameof(Index));
    }
}