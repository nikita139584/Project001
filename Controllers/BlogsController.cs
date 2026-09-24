using Microsoft.AspNetCore.Mvc;

public class BlogsController : Controller
{
    private readonly IBlogService _blogService;

    public BlogsController(IBlogService blogService)
    {
        _blogService = blogService;
    }

    // GET: Blogs
    public async Task<IActionResult> Index()
    {
        return View(await _blogService.GetAllAsync());
    }

    // GET: Blogs/Details/id
    public async Task<IActionResult> Details(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var blog = await _blogService.GetByIdAsync(id);

        if (blog == null)
        {
            return NotFound();
        }

        return View(blog);
    }

    // GET: Blogs/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Blogs/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Blog blog)
    {
        if (ModelState.IsValid)
        {
            await _blogService.CreateAsync(blog);
            return RedirectToAction(nameof(Index));
        }

        return View(blog);
    }

    // GET: Blogs/Edit/id
    public async Task<IActionResult> Edit(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var blog = await _blogService.GetByIdAsync(id);

        if (blog == null)
        {
            return NotFound();
        }

        return View(blog);
    }

    // POST: Blogs/Edit/id
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, Blog blog)
    {
        if (id != blog.BlogId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _blogService.UpdateAsync(blog);
            return RedirectToAction(nameof(Index));
        }

        return View(blog);
    }

    // GET: Blogs/Delete/id
    public async Task<IActionResult> Delete(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var blog = await _blogService.GetByIdAsync(id);

        if (blog == null)
        {
            return NotFound();
        }

        return View(blog);
    }

    // POST: Blogs/Delete/id
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var blog = await _blogService.GetByIdAsync(id);

        if (blog != null)
        {
            await _blogService.DeleteAsync(id);
        }

        return RedirectToAction(nameof(Index));
    }
}