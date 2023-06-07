using WatchMe.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WatchMe.Services;
using WatchMe.Repositories;
using WatchMe.DTOs;
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
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<BarRepository>();
            builder.Services.AddScoped<ShowsRepository>();
            builder.Services.AddScoped<ShowsService>();
            builder.Services.AddScoped<IBarPatcher, BarPatcher>();
            builder.Services.AddScoped<UserDTO>();
            builder.Services.AddScoped<AzureBlobStorageService>();
            builder.Services.AddControllers().AddNewtonsoftJson();
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
            builder.Services.AddMvc().AddMvcOptions(options => options.SuppressAsyncSuffixInActionNames = false);
            var app = builder.Build();
            app.UseAuthentication();

            app.UseCors("AllowSpecificOrigins");
            // Configure the HTTP request pipeline.
            using (var scope = app.Services.CreateScope())
            {
                var dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();
                dataContext.Database.EnsureCreated();
                // dataContext.Seed();
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
