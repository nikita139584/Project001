using Microsoft.AspNetCore.Mvc;

<<<<<<< HEAD
public class CommentsController : Controller
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    // GET: Comments
    public async Task<IActionResult> Index()
    {
        return View(await _commentService.GetAllAsync());
    }

    // GET: Comments/Details/id
    public async Task<IActionResult> Details(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var comment = await _commentService.GetByIdAsync(id);

        if (comment == null)
        {
            return NotFound();
        }

        return View(comment);
    }

    // GET: Comments/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Comments/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Comment comment)
    {
        if (ModelState.IsValid)
        {
            await _commentService.CreateAsync(comment);
            return RedirectToAction(nameof(Index));
        }

        return View(comment);
    }

    // GET: Comments/Edit/id
    public async Task<IActionResult> Edit(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var comment = await _commentService.GetByIdAsync(id);

        if (comment == null)
        {
            return NotFound();
        }

        return View(comment);
    }

    // POST: Comments/Edit/id
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, Comment comment)
    {
        if (id != comment.CommentId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _commentService.UpdateAsync(comment);
            return RedirectToAction(nameof(Index));
        }

        return View(comment);
    }

    // GET: Comments/Delete/id
    public async Task<IActionResult> Delete(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var comment = await _commentService.GetByIdAsync(id);

        if (comment == null)
        {
            return NotFound();
        }

        return View(comment);
    }

    // POST: Comments/Delete/id
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var comment = await _commentService.GetByIdAsync(id);

        if (comment != null)
        {
            await _commentService.DeleteAsync(id);
        }

        return RedirectToAction(nameof(Index));
=======
namespace Project001.Controllers
{
    public class CommentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            return View();
        }

        public IActionResult Delete(int id)
        {
            return View();
        }
>>>>>>> b7ea5945b9b0ae07e3c87b3691ee5691d7ecb501
    }
}