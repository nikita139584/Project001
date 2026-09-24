using Microsoft.AspNetCore.Mvc;

<<<<<<< HEAD
public class PostsController : Controller
{
    private readonly IPostService _postService;

    public PostsController(IPostService postService)
    {
        _postService = postService;
    }

    // GET: Posts
    public async Task<IActionResult> Index()
    {
        return View(await _postService.GetAllAsync());
    }

    // GET: Posts/Details/id
    public async Task<IActionResult> Details(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var post = await _postService.GetByIdAsync(id);

        if (post == null)
        {
            return NotFound();
        }

        return View(post);
    }

    // GET: Posts/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Posts/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Post post)
    {
        if (ModelState.IsValid)
        {
            await _postService.CreateAsync(post);
            return RedirectToAction(nameof(Index));
        }

        return View(post);
    }

    // GET: Posts/Edit/id
    public async Task<IActionResult> Edit(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var post = await _postService.GetByIdAsync(id);

        if (post == null)
        {
            return NotFound();
        }

        return View(post);
    }

    // POST: Posts/Edit/id
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, Post post)
    {
        if (id != post.PostId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _postService.UpdateAsync(post);
            return RedirectToAction(nameof(Index));
        }

        return View(post);
    }

    // GET: Posts/Delete/id
    public async Task<IActionResult> Delete(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var post = await _postService.GetByIdAsync(id);

        if (post == null)
        {
            return NotFound();
        }

        return View(post);
    }

    // POST: Posts/Delete/id
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var post = await _postService.GetByIdAsync(id);

        if (post != null)
        {
            await _postService.DeleteAsync(id);
        }

        return RedirectToAction(nameof(Index));
=======
namespace Project001.Controllers
{
    public class PostsController : Controller
    {
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            return View();
        }
>>>>>>> b7ea5945b9b0ae07e3c87b3691ee5691d7ecb501
    }
}