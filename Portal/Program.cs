using Microsoft.EntityFrameworkCore;
using UrlShortener.Contexts;
using UrlShortener.Shortener;

namespace Portal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ShortenerContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Shortener"));
            });

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IShortenerService, ShortenerService>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<ShortenerContext>();
                context.Database.EnsureCreated();
            }

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