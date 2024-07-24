using CSharpChallenge.Evaluator;
using CSharpChallenge.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CSharpChallenge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Register the configuration
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

            // Register your DAOs
            builder.Services.AddTransient<ProblemDAO>();
            builder.Services.AddTransient<UsersDAO>();
            builder.Services.AddTransient<SecurityService>();
            builder.Services.AddTransient<SolutionEvaluator>();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Home/Index";
                options.LogoutPath = "/Login/Index";
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
