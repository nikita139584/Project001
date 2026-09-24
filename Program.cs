<<<<<<< HEAD
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;

=======
>>>>>>> b7ea5945b9b0ae07e3c87b3691ee5691d7ecb501
namespace Project001
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

<<<<<<< HEAD
            // Репозитории
            builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
            builder.Services.AddScoped<IBlogRepository, BlogRepository>();
            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<IRatingRepository, RatingRepository>();

            builder.Services.AddScoped<IRecipeService, RecipeService>();
            builder.Services.AddScoped<IBlogService, BlogService>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<ICommentService, CommentService>();
            builder.Services.AddScoped<IRatingService, RatingService>();

            builder.Services.AddControllersWithViews();

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

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
=======
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
>>>>>>> b7ea5945b9b0ae07e3c87b3691ee5691d7ecb501
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
<<<<<<< HEAD
            app.UseAuthorization();

            app.MapStaticAssets();

=======

            app.UseAuthorization();

            app.MapStaticAssets();
>>>>>>> b7ea5945b9b0ae07e3c87b3691ee5691d7ecb501
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> b7ea5945b9b0ae07e3c87b3691ee5691d7ecb501
