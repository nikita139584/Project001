

using Google.Cloud.Firestore;
using Project001.Services;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Authentication.Cookies;
namespace Project001;

public class Program
{
    public static void Main(string[] args)
    {

            
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddLocalization(options =>
        {
            options.ResourcesPath = "Resources";
        });

        // Репозитории
        builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
        builder.Services.AddScoped<IBlogRepository, BlogRepository>();
        builder.Services.AddScoped<ICommentRepository, CommentRepository>();
        builder.Services.AddScoped<IPostRepository, PostRepository>();
        builder.Services.AddScoped<IRatingRepository, RatingRepository>();

        // Сервисы
        builder.Services.AddScoped<IRecipeService, RecipeService>();
        builder.Services.AddScoped<IBlogService, BlogService>();
        builder.Services.AddScoped<IPostService, PostService>();
        builder.Services.AddScoped<ICommentService, CommentService>();
        builder.Services.AddScoped<IRatingService, RatingService>();

        // Регистрация пользователей
        builder.Services.AddScoped<IRegistrationStore, RegistrationRepository>();

        // Аутентификация по cookie: держит сессию пользователя между запросами.
        builder.Services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/Login";
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
                options.SlidingExpiration = true;
            });

        builder.Services
            .AddControllersWithViews()
            .AddViewLocalization();
        // Firestore
        builder.Services.AddSingleton(provider =>
        {
            var config = provider.GetRequiredService<IConfiguration>();
            var projectId = config["Firebase:ProjectId"]!;

            var credentialsPath = Path.Combine(
                AppContext.BaseDirectory,
                config["Firebase:CredentialsPath"]!);

            Environment.SetEnvironmentVariable(
                "GOOGLE_APPLICATION_CREDENTIALS",
                credentialsPath);

            return FirestoreDb.Create(projectId);
        });

        var app = builder.Build();


        var localizationOptions = new RequestLocalizationOptions()
            .SetDefaultCulture("ru-RU")
            .AddSupportedCultures("uk-UA", "ru-RU")
            .AddSupportedUICultures("uk-UA", "ru-RU");

        app.UseRequestLocalization(localizationOptions);

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        // Порядок важен: сначала кто пользователь, потом что ему можно.
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapStaticAssets();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }
}

