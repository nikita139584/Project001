using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using System.Net;
using Xunit;

namespace Project001.Tests.Integration;

// ============================================================================
// Интеграционные тесты: приложение запускается целиком в памяти
// (маршрутизация, контроллеры, Razor-представления), а HTTP-запросы
// отправляются через тестовый клиент. Подменён только IRecipeService,
// поэтому реальная база данных не нужна.
// ============================================================================

/// <summary>
/// Фабрика тестового хоста. Запускает приложение в окружении "Testing"
/// и заменяет настоящий IRecipeService на Moq-заглушку,
/// доступную тестам через RecipeServiceMock.
/// </summary>
public sealed class Project001WebApplicationFactory : WebApplicationFactory<Project001.Program>
{
    public Mock<IRecipeService> RecipeServiceMock { get; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureTestServices(services =>
        {
            // Убираем зарегистрированный сервис и подставляем заглушку.
            services.RemoveAll<IRecipeService>();
            services.AddSingleton(RecipeServiceMock.Object);
        });
    }
}

/// <summary>Проверка главной страницы.</summary>
public class HomeEndpointTests : IClassFixture<Project001WebApplicationFactory>
{
    private readonly Project001WebApplicationFactory _factory;

    public HomeEndpointTests(Project001WebApplicationFactory factory)
    {
        _factory = factory;
    }

    // GET /Home/Index отвечает 200 OK, а в HTML есть название сайта "Recipe.Site".
    [Fact]
    public async Task GetHome_ReturnsOkAndRendersHomePage()
    {
        using var client = _factory.CreateClient();

        var response = await client.GetAsync("/Home/Index");
        var body = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("Recipe.Site", body);
    }
}

/// <summary>Проверка страниц рецептов: список и детали.</summary>
public class RecipesEndpointsTests : IClassFixture<Project001WebApplicationFactory>
{
    private readonly Project001WebApplicationFactory _factory;

    public RecipesEndpointsTests(Project001WebApplicationFactory factory)
    {
        _factory = factory;

        // Фабрика общая для всех тестов класса: сбрасываем настройки заглушки,
        // чтобы тесты не влияли друг на друга.
        _factory.RecipeServiceMock.Reset();
    }

    // GET /Recipes: заглушка отдаёт один рецепт, страница отвечает 200 OK
    // и показывает его название. Сервис вызван один раз.
    [Fact]
    public async Task GetRecipes_ReturnsOkAndRendersRecipeData()
    {
        var recipes = new List<Recipe>
        {
            new()
            {
                RecipeId = "recipe-1",
                Title = "Test Pasta",
                Description = "Simple test recipe",
                Ingredients = "Pasta",
                Instructions = "Cook pasta"
            }
        };

        _factory.RecipeServiceMock
            .Setup(s => s.GetAllAsync())
            .ReturnsAsync(recipes);

        using var client = _factory.CreateClient();

        var response = await client.GetAsync("/Recipes");
        var body = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("Test Pasta", body);
        _factory.RecipeServiceMock.Verify(s => s.GetAllAsync(), Times.Once);
    }

    // GET /Recipes/Details?id=recipe-1 для существующего рецепта:
    // 200 OK, на странице есть название рецепта, сервис вызван с нужным id.
    [Fact]
    public async Task GetRecipeDetails_ReturnsOkForExistingRecipe()
    {
        var recipe = new Recipe
        {
            RecipeId = "recipe-1",
            Title = "Test Soup",
            Description = "Test description",
            Ingredients = "Water",
            Instructions = "Boil"
        };

        _factory.RecipeServiceMock
            .Setup(s => s.GetByIdAsync("recipe-1"))
            .ReturnsAsync(recipe);

        using var client = _factory.CreateClient();

        var response = await client.GetAsync("/Recipes/Details?id=recipe-1");
        var body = await response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains("Test Soup", body);
        _factory.RecipeServiceMock.Verify(s => s.GetByIdAsync("recipe-1"), Times.Once);
    }

    // GET /Recipes/Details?id=missing: рецепта нет, ответ 404 Not Found.
    [Fact]
    public async Task GetRecipeDetails_ReturnsNotFoundWhenRecipeDoesNotExist()
    {
        _factory.RecipeServiceMock
            .Setup(s => s.GetByIdAsync("missing"))
            .ReturnsAsync((Recipe?)null);

        using var client = _factory.CreateClient();

        var response = await client.GetAsync("/Recipes/Details?id=missing");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    // GET /Recipes/Details без параметра id: ответ 404 Not Found.
    [Fact]
    public async Task GetRecipeDetails_ReturnsNotFoundWhenIdIsMissing()
    {
        using var client = _factory.CreateClient();

        var response = await client.GetAsync("/Recipes/Details");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}