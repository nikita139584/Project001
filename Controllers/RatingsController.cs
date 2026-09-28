using Microsoft.AspNetCore.Mvc;

public class RatingsController : Controller
{
    private readonly IRatingService _ratingService;

    public RatingsController(IRatingService ratingService)
    {
        _ratingService = ratingService;
    }

    // GET: Ratings
    public async Task<IActionResult> Index()
    {
        return View(await _ratingService.GetAllAsync());
    }

    // GET: Ratings/Details/id
    public async Task<IActionResult> Details(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var rating = await _ratingService.GetByIdAsync(id);

        if (rating == null)
        {
            return NotFound();
        }

        return View(rating);
    }

    // GET: Ratings/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Ratings/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Rating rating)
    {
        if (ModelState.IsValid)
        {
            await _ratingService.CreateAsync(rating);
            return RedirectToAction(nameof(Index));
        }

        return View(rating);
    }

    // GET: Ratings/Edit/id
    public async Task<IActionResult> Edit(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var rating = await _ratingService.GetByIdAsync(id);

        if (rating == null)
        {
            return NotFound();
        }

        return View(rating);
    }

    // POST: Ratings/Edit/id
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, Rating rating)
    {
        if (id != rating.RatingId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _ratingService.UpdateAsync(rating);
            return RedirectToAction(nameof(Index));
        }

        return View(rating);
    }

    // GET: Ratings/Delete/id
    public async Task<IActionResult> Delete(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var rating = await _ratingService.GetByIdAsync(id);

        if (rating == null)
        {
            return NotFound();
        }

        return View(rating);
    }

    // POST: Ratings/Delete/id
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var rating = await _ratingService.GetByIdAsync(id);

        if (rating != null)
        {
            await _ratingService.DeleteAsync(id);
        }

        return RedirectToAction(nameof(Index));
    }
}