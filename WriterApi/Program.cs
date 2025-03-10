using Shared.Custom;
using Shared.Models;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using WriterApi.Hosted;

namespace WriterApi;

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
        builder.Services.AddSingleton<IConnection>((opt) =>
        {
            var factory = new ConnectionFactory()
            {
                HostName = "rabbit-mq-server",
                Port = 5672,
            };
            var x = factory.CreateConnectionAsync().GetAwaiter().GetResult();
            return x;
        });

        
        // builder.Services.AddHostedService<MessageReciever>();
        builder.Services.AddControllers();
        var app = builder.Build();
        
        // Configure the HTTP request pipeline.
        app.MapGet("/healthy", () => "WRITER APIw ARE HEALTHY");
        app.UseHttpsRedirection();
        
        app.UseAuthorization();     

        app.MapControllers();

        app.Run();
    }
}       