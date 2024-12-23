using Bookshop.Repositories.implementations;
using Bookshop.Repositories.Interfaces;

namespace Bookshop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularOrigin", policy =>
                {
                    policy.WithOrigins("http://localhost:4200") // Angular app's URL
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            // Add controllers
            builder.Services.AddControllers();

            // Register Swagger services
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();  // This is essential for Swagger generation

            // Register other dependencies (example)
            builder.Services.AddScoped<IPKG_Books, PKG_Books>();

            var app = builder.Build();

            // Redirect root to Swagger UI
            app.MapGet("/", () => Results.Redirect("/swagger"));

            // Enable Swagger middleware
            app.UseSwagger();

            // Enable Swagger UI middleware
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Bookshop API v1");
                options.RoutePrefix = string.Empty; // Makes Swagger accessible at the root URL
            });

            // Use middleware in the correct order
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowAngularOrigin");
            app.UseAuthorization();

            // Map controllers
            app.MapControllers();

            app.Run();
        }
    }
}
