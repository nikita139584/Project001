using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Project001.Tests.Unit.Services;

// ============================================================================
// Юнит-тесты сервисов.
// Репозиторий подменяется Moq-заглушкой, поэтому проверяется только логика
// самого сервиса: он должен правильно делегировать вызовы репозиторию
// и возвращать то, что тот вернул. База данных не используется.
// ============================================================================

/// <summary>Тесты сервиса рецептов: чтение и CRUD-операции.</summary>
public class RecipeServiceTests
{
    private readonly Mock<IRecipeRepository> _repository = new();
    private readonly RecipeService _service;

    public RecipeServiceTests()
    {
        _service = new RecipeService(_repository.Object);
    }

    // Сервис возвращает тот же самый список, что отдал репозиторий,
    // и обращается к репозиторию ровно один раз.
    [Fact]
    public async Task GetAllAsync_ReturnsItemsFromRepository()
    {
        var items = new List<Recipe>
        {
            new() { RecipeId = "recipe-1", Title = "Pasta" },
            new() { RecipeId = "recipe-2", Title = "Soup" }
        };
        _repository.Setup(r => r.GetAllAsync()).ReturnsAsync(items);

        var result = await _service.GetAllAsync();

        Assert.Same(items, result);
        Assert.Equal(2, result.Count);
        _repository.Verify(r => r.GetAllAsync(), Times.Once);
    }

    // Если рецепт с таким id есть, сервис возвращает именно его.
    [Fact]
    public async Task GetByIdAsync_ReturnsItem_WhenItExists()
    {
        var item = new Recipe { RecipeId = "recipe-1", Title = "Pasta" };
        _repository.Setup(r => r.GetByIdAsync("recipe-1")).ReturnsAsync(item);

        var result = await _service.GetByIdAsync("recipe-1");

        Assert.Same(item, result);
        _repository.Verify(r => r.GetByIdAsync("recipe-1"), Times.Once);
    }

    // Если рецепта нет, сервис возвращает null, а не бросает исключение.
    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenItDoesNotExist()
    {
        _repository.Setup(r => r.GetByIdAsync("missing")).ReturnsAsync((Recipe?)null);

        var result = await _service.GetByIdAsync("missing");

        Assert.Null(result);
    }

    // Создание: рецепт передаётся в репозиторий без изменений.
    [Fact]
    public async Task CreateAsync_PassesEntityToRepository()
    {
        var item = new Recipe { Title = "Pasta" };

        await _service.CreateAsync(item);

        _repository.Verify(r => r.CreateAsync(item), Times.Once);
    }

    // Обновление: изменённый рецепт передаётся в репозиторий.
    [Fact]
    public async Task UpdateAsync_PassesEntityToRepository()
    {
        var item = new Recipe { RecipeId = "recipe-1", Title = "Soup" };

        await _service.UpdateAsync(item);

        _repository.Verify(r => r.UpdateAsync(item), Times.Once);
    }

    // Удаление: в репозиторий уходит id рецепта.
    [Fact]
    public async Task DeleteAsync_PassesIdToRepository()
    {
        await _service.DeleteAsync("recipe-1");

        _repository.Verify(r => r.DeleteAsync("recipe-1"), Times.Once);
    }

    // Проверка существования: сервис возвращает ответ репозитория как есть
    // (тест запускается дважды: для true и для false).
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ExistsAsync_ReturnsRepositoryResult(bool exists)
    {
        _repository.Setup(r => r.ExistsAsync("recipe-1")).ReturnsAsync(exists);

        var result = await _service.ExistsAsync("recipe-1");

        Assert.Equal(exists, result);
    }
}

/// <summary>Тесты сервиса блогов: чтение и CRUD-операции.</summary>
public class BlogServiceTests
{
    private readonly Mock<IBlogRepository> _repository = new();
    private readonly BlogService _service;

    public BlogServiceTests()
    {
        _service = new BlogService(_repository.Object);
    }

    // Список блогов берётся из репозитория и возвращается без изменений.
    [Fact]
    public async Task GetAllAsync_ReturnsItemsFromRepository()
    {
        var items = new List<Blog>
        {
            new() { BlogId = "blog-1", Title = "Cooking" },
            new() { BlogId = "blog-2", Title = "Travel" }
        };
        _repository.Setup(r => r.GetAllAsync()).ReturnsAsync(items);

        var result = await _service.GetAllAsync();

        Assert.Same(items, result);
        Assert.Equal(2, result.Count);
        _repository.Verify(r => r.GetAllAsync(), Times.Once);
    }

    // Существующий блог возвращается по id.
    [Fact]
    public async Task GetByIdAsync_ReturnsItem_WhenItExists()
    {
        var item = new Blog { BlogId = "blog-1", Title = "Cooking" };
        _repository.Setup(r => r.GetByIdAsync("blog-1")).ReturnsAsync(item);

        var result = await _service.GetByIdAsync("blog-1");

        Assert.Same(item, result);
        _repository.Verify(r => r.GetByIdAsync("blog-1"), Times.Once);
    }

    // Для несуществующего блога результат null.
    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenItDoesNotExist()
    {
        _repository.Setup(r => r.GetByIdAsync("missing")).ReturnsAsync((Blog?)null);

        var result = await _service.GetByIdAsync("missing");

        Assert.Null(result);
    }

    // Создание блога делегируется репозиторию.
    [Fact]
    public async Task CreateAsync_PassesEntityToRepository()
    {
        var item = new Blog { Title = "Cooking" };

        await _service.CreateAsync(item);

        _repository.Verify(r => r.CreateAsync(item), Times.Once);
    }

    // Обновление блога делегируется репозиторию.
    [Fact]
    public async Task UpdateAsync_PassesEntityToRepository()
    {
        var item = new Blog { BlogId = "blog-1", Title = "Travel" };

        await _service.UpdateAsync(item);

        _repository.Verify(r => r.UpdateAsync(item), Times.Once);
    }

    // Удаление блога: репозиторий получает id.
    [Fact]
    public async Task DeleteAsync_PassesIdToRepository()
    {
        await _service.DeleteAsync("blog-1");

        _repository.Verify(r => r.DeleteAsync("blog-1"), Times.Once);
    }

    // Ответ репозитория о существовании блога возвращается без изменений.
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ExistsAsync_ReturnsRepositoryResult(bool exists)
    {
        _repository.Setup(r => r.ExistsAsync("blog-1")).ReturnsAsync(exists);

        var result = await _service.ExistsAsync("blog-1");

        Assert.Equal(exists, result);
    }
}

/// <summary>Тесты сервиса постов: CRUD и выборка постов конкретного блога.</summary>
public class PostServiceTests
{
    private readonly Mock<IPostRepository> _repository = new();
    private readonly PostService _service;

    public PostServiceTests()
    {
        _service = new PostService(_repository.Object);
    }

    // Все посты берутся из репозитория без изменений.
    [Fact]
    public async Task GetAllAsync_ReturnsItemsFromRepository()
    {
        var items = new List<Post>
        {
            new() { PostId = "post-1", BlogId = "blog-1", Title = "First post" },
            new() { PostId = "post-2", BlogId = "blog-1", Title = "Second post" }
        };
        _repository.Setup(r => r.GetAllAsync()).ReturnsAsync(items);

        var result = await _service.GetAllAsync();

        Assert.Same(items, result);
        Assert.Equal(2, result.Count);
        _repository.Verify(r => r.GetAllAsync(), Times.Once);
    }

    // Существующий пост возвращается по id.
    [Fact]
    public async Task GetByIdAsync_ReturnsItem_WhenItExists()
    {
        var item = new Post { PostId = "post-1", BlogId = "blog-1", Title = "First post" };
        _repository.Setup(r => r.GetByIdAsync("post-1")).ReturnsAsync(item);

        var result = await _service.GetByIdAsync("post-1");

        Assert.Same(item, result);
        _repository.Verify(r => r.GetByIdAsync("post-1"), Times.Once);
    }

    // Для несуществующего поста результат null.
    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenItDoesNotExist()
    {
        _repository.Setup(r => r.GetByIdAsync("missing")).ReturnsAsync((Post?)null);

        var result = await _service.GetByIdAsync("missing");

        Assert.Null(result);
    }

    // Посты блога: сервис возвращает список из репозитория,
    // и у каждого поста BlogId совпадает с запрошенным.
    [Fact]
    public async Task GetByBlogIdAsync_ReturnsItemsFromRepository()
    {
        var items = new List<Post>
        {
            new() { PostId = "post-1", BlogId = "blog-1" },
            new() { PostId = "post-2", BlogId = "blog-1" }
        };
        _repository.Setup(r => r.GetByBlogIdAsync("blog-1")).ReturnsAsync(items);

        var result = await _service.GetByBlogIdAsync("blog-1");

        Assert.Same(items, result);
        Assert.All(result, x => Assert.Equal("blog-1", x.BlogId));
        _repository.Verify(r => r.GetByBlogIdAsync("blog-1"), Times.Once);
    }

    // Создание поста делегируется репозиторию.
    [Fact]
    public async Task CreateAsync_PassesEntityToRepository()
    {
        var item = new Post { BlogId = "blog-1", Title = "First post" };

        await _service.CreateAsync(item);

        _repository.Verify(r => r.CreateAsync(item), Times.Once);
    }

    // Обновление поста делегируется репозиторию.
    [Fact]
    public async Task UpdateAsync_PassesEntityToRepository()
    {
        var item = new Post { PostId = "post-1", BlogId = "blog-1", Title = "Second post" };

        await _service.UpdateAsync(item);

        _repository.Verify(r => r.UpdateAsync(item), Times.Once);
    }

    // Удаление поста: репозиторий получает id.
    [Fact]
    public async Task DeleteAsync_PassesIdToRepository()
    {
        await _service.DeleteAsync("post-1");

        _repository.Verify(r => r.DeleteAsync("post-1"), Times.Once);
    }

    // Ответ репозитория о существовании поста возвращается без изменений.
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ExistsAsync_ReturnsRepositoryResult(bool exists)
    {
        _repository.Setup(r => r.ExistsAsync("post-1")).ReturnsAsync(exists);

        var result = await _service.ExistsAsync("post-1");

        Assert.Equal(exists, result);
    }
}

/// <summary>Тесты сервиса оценок: CRUD и выборка оценок конкретного рецепта.</summary>
public class RatingServiceTests
{
    private readonly Mock<IRatingRepository> _repository = new();
    private readonly RatingService _service;

    public RatingServiceTests()
    {
        _service = new RatingService(_repository.Object);
    }

    // Все оценки берутся из репозитория без изменений.
    [Fact]
    public async Task GetAllAsync_ReturnsItemsFromRepository()
    {
        var items = new List<Rating>
        {
            new() { RatingId = "rating-1", RecipeId = "recipe-1", Value = 5 },
            new() { RatingId = "rating-2", RecipeId = "recipe-1", Value = 4 }
        };
        _repository.Setup(r => r.GetAllAsync()).ReturnsAsync(items);

        var result = await _service.GetAllAsync();

        Assert.Same(items, result);
        Assert.Equal(2, result.Count);
        _repository.Verify(r => r.GetAllAsync(), Times.Once);
    }

    // Существующая оценка возвращается по id.
    [Fact]
    public async Task GetByIdAsync_ReturnsItem_WhenItExists()
    {
        var item = new Rating { RatingId = "rating-1", RecipeId = "recipe-1", Value = 5 };
        _repository.Setup(r => r.GetByIdAsync("rating-1")).ReturnsAsync(item);

        var result = await _service.GetByIdAsync("rating-1");

        Assert.Same(item, result);
        _repository.Verify(r => r.GetByIdAsync("rating-1"), Times.Once);
    }

    // Для несуществующей оценки результат null.
    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenItDoesNotExist()
    {
        _repository.Setup(r => r.GetByIdAsync("missing")).ReturnsAsync((Rating?)null);

        var result = await _service.GetByIdAsync("missing");

        Assert.Null(result);
    }

    // Оценки рецепта: возвращается список из репозитория,
    // и у каждой оценки RecipeId совпадает с запрошенным.
    [Fact]
    public async Task GetByRecipeIdAsync_ReturnsItemsFromRepository()
    {
        var items = new List<Rating>
        {
            new() { RatingId = "rating-1", RecipeId = "recipe-1" },
            new() { RatingId = "rating-2", RecipeId = "recipe-1" }
        };
        _repository.Setup(r => r.GetByRecipeIdAsync("recipe-1")).ReturnsAsync(items);

        var result = await _service.GetByRecipeIdAsync("recipe-1");

        Assert.Same(items, result);
        Assert.All(result, x => Assert.Equal("recipe-1", x.RecipeId));
        _repository.Verify(r => r.GetByRecipeIdAsync("recipe-1"), Times.Once);
    }

    // Создание оценки делегируется репозиторию.
    [Fact]
    public async Task CreateAsync_PassesEntityToRepository()
    {
        var item = new Rating { RecipeId = "recipe-1", Value = 5 };

        await _service.CreateAsync(item);

        _repository.Verify(r => r.CreateAsync(item), Times.Once);
    }

    // Обновление оценки делегируется репозиторию.
    [Fact]
    public async Task UpdateAsync_PassesEntityToRepository()
    {
        var item = new Rating { RatingId = "rating-1", RecipeId = "recipe-1", Value = 4 };

        await _service.UpdateAsync(item);

        _repository.Verify(r => r.UpdateAsync(item), Times.Once);
    }

    // Удаление оценки: репозиторий получает id.
    [Fact]
    public async Task DeleteAsync_PassesIdToRepository()
    {
        await _service.DeleteAsync("rating-1");

        _repository.Verify(r => r.DeleteAsync("rating-1"), Times.Once);
    }

    // Ответ репозитория о существовании оценки возвращается без изменений.
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ExistsAsync_ReturnsRepositoryResult(bool exists)
    {
        _repository.Setup(r => r.ExistsAsync("rating-1")).ReturnsAsync(exists);

        var result = await _service.ExistsAsync("rating-1");

        Assert.Equal(exists, result);
    }
}

/// <summary>Тесты сервиса комментариев: CRUD и выборка комментариев к посту.</summary>
public class CommentServiceTests
{
    private readonly Mock<ICommentRepository> _repository = new();
    private readonly CommentService _service;

    public CommentServiceTests()
    {
        _service = new CommentService(_repository.Object);
    }

    // Все комментарии берутся из репозитория без изменений.
    [Fact]
    public async Task GetAllAsync_ReturnsComments()
    {
        var comments = new List<Comment>
        {
            new() { CommentId = "1", Content = "Good post" },
            new() { CommentId = "2", Content = "Interesting" }
        };

        _repository.Setup(r => r.GetAllAsync())
            .ReturnsAsync(comments);

        var result = await _service.GetAllAsync();

        Assert.Same(comments, result);
        Assert.Equal(2, result.Count);
        _repository.Verify(r => r.GetAllAsync(), Times.Once);
    }

    // Комментарий возвращается по id.
    [Fact]
    public async Task GetByIdAsync_ReturnsComment()
    {
        var comment = new Comment
        {
            CommentId = "comment-1",
            Content = "Nice!"
        };

        _repository.Setup(r => r.GetByIdAsync("comment-1"))
            .ReturnsAsync(comment);

        var result = await _service.GetByIdAsync("comment-1");

        Assert.Same(comment, result);
    }

    // Для несуществующего комментария результат null.
    [Fact]
    public async Task GetByIdAsync_ReturnsNullForMissingComment()
    {
        _repository.Setup(r => r.GetByIdAsync("missing"))
            .ReturnsAsync((Comment?)null);

        var result = await _service.GetByIdAsync("missing");

        Assert.Null(result);
    }

    // Комментарии к посту: возвращается список из репозитория,
    // и у каждого комментария PostId совпадает с запрошенным.
    [Fact]
    public async Task GetByPostIdAsync_ReturnsCommentsForPost()
    {
        var comments = new List<Comment>
        {
            new() { CommentId = "1", PostId = "post-1" },
            new() { CommentId = "2", PostId = "post-1" }
        };

        _repository.Setup(r => r.GetByPostIdAsync("post-1"))
            .ReturnsAsync(comments);

        var result = await _service.GetByPostIdAsync("post-1");

        Assert.Same(comments, result);
        Assert.All(result, c => Assert.Equal("post-1", c.PostId));
        _repository.Verify(r => r.GetByPostIdAsync("post-1"), Times.Once);
    }

    // Создание комментария делегируется репозиторию.
    [Fact]
    public async Task CreateAsync_PassesCommentToRepository()
    {
        var comment = new Comment
        {
            Content = "Test comment",
            PostId = "post-1"
        };

        _repository.Setup(r => r.CreateAsync(comment))
            .Returns(Task.CompletedTask);

        await _service.CreateAsync(comment);

        _repository.Verify(r => r.CreateAsync(comment), Times.Once);
    }

    // Обновление комментария делегируется репозиторию.
    [Fact]
    public async Task UpdateAsync_PassesCommentToRepository()
    {
        var comment = new Comment
        {
            CommentId = "comment-1",
            Content = "Updated comment"
        };

        _repository.Setup(r => r.UpdateAsync(comment))
            .Returns(Task.CompletedTask);

        await _service.UpdateAsync(comment);

        _repository.Verify(r => r.UpdateAsync(comment), Times.Once);
    }

    // Удаление комментария: репозиторий получает id.
    [Fact]
    public async Task DeleteAsync_PassesIdToRepository()
    {
        _repository.Setup(r => r.DeleteAsync("comment-1"))
            .Returns(Task.CompletedTask);

        await _service.DeleteAsync("comment-1");

        _repository.Verify(r => r.DeleteAsync("comment-1"), Times.Once);
    }
}

// ============================================================================
// Юнит-тесты контроллеров.
// Сервис подменяется заглушкой. Проверяется, какой результат (NotFound,
// View, Redirect) возвращает действие и вызывается ли сервис.
// ============================================================================

/// <summary>Тесты контроллера рецептов: Details, Create, Edit, Delete.</summary>
public class RecipesControllerTests
{
    private readonly Mock<IRecipeService> _service = new();
    private readonly RecipesController _controller;

    public RecipesControllerTests()
    {
        _controller = new RecipesController(_service.Object);
    }

    // Details без id: 404, к сервису не обращаемся.
    // (В исходном файле имя метода было разорвано переносами строк
    // и начиналось с "_ReturnsNotFound..."; исправлено на Details_...)
    [Fact]
    public async Task Details_ReturnsNotFound_WhenIdIsNull()
    {
        var result = await _controller.Details(null);

        Assert.IsType<NotFoundResult>(result);
        _service.Verify(s => s.GetByIdAsync(It.IsAny<string>()), Times.Never);
    }

    // Details с id несуществующего рецепта: 404.
    [Fact]
    public async Task Details_ReturnsNotFound_WhenItemDoesNotExist()
    {
        _service.Setup(s => s.GetByIdAsync("missing")).ReturnsAsync((Recipe?)null);

        var result = await _controller.Details("missing");

        Assert.IsType<NotFoundResult>(result);
    }

    // Details для существующего рецепта: возвращается View, моделью служит этот рецепт.
    [Fact]
    public async Task Details_ReturnsViewWithItem_WhenItExists()
    {
        var item = new Recipe { RecipeId = "recipe-1", Title = "Pasta" };
        _service.Setup(s => s.GetByIdAsync("recipe-1")).ReturnsAsync(item);

        var result = await _controller.Details("recipe-1");

        var view = Assert.IsType<ViewResult>(result);
        Assert.Same(item, view.Model);
    }

    // Create (POST) с валидной моделью: рецепт сохраняется, затем редирект на Index.
    [Fact]
    public async Task Create_Post_ValidModel_CreatesItemAndRedirectsToIndex()
    {
        var item = new Recipe { Title = "Pasta" };

        var result = await _controller.Create(item);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirect.ActionName);
        _service.Verify(s => s.CreateAsync(item), Times.Once);
    }

    // Create (POST) с ошибкой валидации: форма показывается снова
    // с теми же данными, рецепт не сохраняется.
    [Fact]
    public async Task Create_Post_InvalidModel_ReturnsViewAndDoesNotCreate()
    {
        var item = new Recipe { Title = "Pasta" };
        _controller.ModelState.AddModelError("Title", "Required");

        var result = await _controller.Create(item);

        var view = Assert.IsType<ViewResult>(result);
        Assert.Same(item, view.Model);
        _service.Verify(s => s.CreateAsync(It.IsAny<Recipe>()), Times.Never);
    }

    // Edit (POST), если id из адреса не совпадает с id в модели: 404, обновления нет.
    [Fact]
    public async Task Edit_Post_ReturnsNotFound_WhenIdDoesNotMatchModel()
    {
        var item = new Recipe { RecipeId = "recipe-1", Title = "Pasta" };

        var result = await _controller.Edit("recipe-2", item);

        Assert.IsType<NotFoundResult>(result);
        _service.Verify(s => s.UpdateAsync(It.IsAny<Recipe>()), Times.Never);
    }

    // Edit (POST) с валидной моделью: рецепт обновляется, затем редирект на Index.
    [Fact]
    public async Task Edit_Post_ValidModel_UpdatesItemAndRedirectsToIndex()
    {
        var item = new Recipe { RecipeId = "recipe-1", Title = "Pasta" };

        var result = await _controller.Edit("recipe-1", item);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirect.ActionName);
        _service.Verify(s => s.UpdateAsync(item), Times.Once);
    }

    // Подтверждённое удаление существующего рецепта: рецепт удаляется, редирект на Index.
    [Fact]
    public async Task DeleteConfirmed_DeletesItemAndRedirects_WhenItExists()
    {
        var item = new Recipe { RecipeId = "recipe-1" };
        _service.Setup(s => s.GetByIdAsync("recipe-1")).ReturnsAsync(item);

        var result = await _controller.DeleteConfirmed("recipe-1");

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirect.ActionName);
        _service.Verify(s => s.DeleteAsync("recipe-1"), Times.Once);
    }

    // Удаление несуществующего рецепта: ничего не удаляется, но пользователя
    // всё равно перенаправляют (без ошибки).
    [Fact]
    public async Task DeleteConfirmed_DoesNotDeleteButRedirects_WhenItemIsMissing()
    {
        _service.Setup(s => s.GetByIdAsync("missing")).ReturnsAsync((Recipe?)null);

        var result = await _controller.DeleteConfirmed("missing");

        Assert.IsType<RedirectToActionResult>(result);
        _service.Verify(s => s.DeleteAsync(It.IsAny<string>()), Times.Never);
    }
}

/// <summary>Тесты контроллера оценок: Details, Create, Edit, Delete.</summary>
public class RatingsControllerTests
{
    private readonly Mock<IRatingService> _service = new();
    private readonly RatingsController _controller;

    public RatingsControllerTests()
    {
        _controller = new RatingsController(_service.Object);
    }

    // Details без id: 404, к сервису не обращаемся.
    [Fact]
    public async Task Details_ReturnsNotFound_WhenIdIsNull()
    {
        var result = await _controller.Details(null);

        Assert.IsType<NotFoundResult>(result);
        _service.Verify(s => s.GetByIdAsync(It.IsAny<string>()), Times.Never);
    }

    // Details с id несуществующей оценки: 404.
    [Fact]
    public async Task Details_ReturnsNotFound_WhenItemDoesNotExist()
    {
        _service.Setup(s => s.GetByIdAsync("missing")).ReturnsAsync((Rating?)null);

        var result = await _controller.Details("missing");

        Assert.IsType<NotFoundResult>(result);
    }

    // Details для существующей оценки: возвращается View с этой оценкой в модели.
    [Fact]
    public async Task Details_ReturnsViewWithItem_WhenItExists()
    {
        var item = new Rating { RatingId = "rating-1", RecipeId = "recipe-1", Value = 5 };
        _service.Setup(s => s.GetByIdAsync("rating-1")).ReturnsAsync(item);

        var result = await _controller.Details("rating-1");

        var view = Assert.IsType<ViewResult>(result);
        Assert.Same(item, view.Model);
    }

    // Create (POST) с валидной моделью: оценка сохраняется, затем редирект на Index.
    [Fact]
    public async Task Create_Post_ValidModel_CreatesItemAndRedirectsToIndex()
    {
        var item = new Rating { RecipeId = "recipe-1", Value = 5 };

        var result = await _controller.Create(item);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirect.ActionName);
        _service.Verify(s => s.CreateAsync(item), Times.Once);
    }

    // Create (POST) с ошибкой валидации: форма показывается снова, оценка не сохраняется.
    [Fact]
    public async Task Create_Post_InvalidModel_ReturnsViewAndDoesNotCreate()
    {
        var item = new Rating { RecipeId = "recipe-1", Value = 5 };
        _controller.ModelState.AddModelError("Title", "Required");

        var result = await _controller.Create(item);

        var view = Assert.IsType<ViewResult>(result);
        Assert.Same(item, view.Model);
        _service.Verify(s => s.CreateAsync(It.IsAny<Rating>()), Times.Never);
    }

    // Edit (POST), если id из адреса не совпадает с id в модели: 404, обновления нет.
    [Fact]
    public async Task Edit_Post_ReturnsNotFound_WhenIdDoesNotMatchModel()
    {
        var item = new Rating { RatingId = "rating-1", RecipeId = "recipe-1", Value = 5 };

        var result = await _controller.Edit("rating-2", item);

        Assert.IsType<NotFoundResult>(result);
        _service.Verify(s => s.UpdateAsync(It.IsAny<Rating>()), Times.Never);
    }

    // Edit (POST) с валидной моделью: оценка обновляется, затем редирект на Index.
    [Fact]
    public async Task Edit_Post_ValidModel_UpdatesItemAndRedirectsToIndex()
    {
        var item = new Rating { RatingId = "rating-1", RecipeId = "recipe-1", Value = 5 };

        var result = await _controller.Edit("rating-1", item);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirect.ActionName);
        _service.Verify(s => s.UpdateAsync(item), Times.Once);
    }

    // Подтверждённое удаление существующей оценки: оценка удаляется, редирект на Index.
    [Fact]
    public async Task DeleteConfirmed_DeletesItemAndRedirects_WhenItExists()
    {
        var item = new Rating { RatingId = "rating-1" };
        _service.Setup(s => s.GetByIdAsync("rating-1")).ReturnsAsync(item);

        var result = await _controller.DeleteConfirmed("rating-1");

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirect.ActionName);
        _service.Verify(s => s.DeleteAsync("rating-1"), Times.Once);
    }

    // Удаление несуществующей оценки: ничего не удаляется, но редирект всё равно есть.
    [Fact]
    public async Task DeleteConfirmed_DoesNotDeleteButRedirects_WhenItemIsMissing()
    {
        _service.Setup(s => s.GetByIdAsync("missing")).ReturnsAsync((Rating?)null);

        var result = await _controller.DeleteConfirmed("missing");

        Assert.IsType<RedirectToActionResult>(result);
        _service.Verify(s => s.DeleteAsync(It.IsAny<string>()), Times.Never);
    }
}