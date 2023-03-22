using WatchMe.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WatchMe.Services;
namespace WatchMe
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            /*
            builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DataContext")))*/
            builder.Services.AddDbContext<DataContext>(
                options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("WatchMeDb"))
            );
            builder.Services.AddScoped<BarService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                    name: "AllowSpecificOrigins",
                    policy =>
                    {
                        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    }
                );
            });
            var jwtSecretKey = builder.Configuration
                .GetSection("AuthService")
                .GetValue<string>("SecretKey");
            builder.Services.AddSingleton(new AuthService(jwtSecretKey));
            builder.Services.AddMvc().AddMvcOptions(e => e.EnableEndpointRouting = false);
            var app = builder.Build();
            app.UseAuthentication();

            app.UseCors("AllowSpecificOrigins");
            // Configure the HTTP request pipeline.
            using (var scope = app.Services.CreateScope())
            {
                var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
                dataContext.Database.EnsureCreated();
                dataContext.Seed();
            }

            // app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
