using Shared.Custom;
using Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace ReaderApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<ProducersContext>(opt =>
        {
            var config = new MySqlConfig();
            builder.Configuration.GetSection("Databases:MySql").Bind(config);
            opt.UseMySql(config.GetConnectionString(), ServerVersion.Parse(config.Version));
        });

        builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
        
        builder.Services.AddControllers();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.MapGet("/healthy", () => "READER API ARE HEALTHY");
        
        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}