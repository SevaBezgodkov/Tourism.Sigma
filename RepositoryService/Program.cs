
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using RepositoryService.Background;
using RepositoryService.Background.Interfaces;
using RepositoryService.Command;
using RepositoryService.Command.Interfaces;

namespace RepositoryService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var services = builder.Services;

            var conString = builder.Configuration.GetConnectionString("PostgreConnectionString");
            builder.Services.AddDbContextPool<AppDbContext>(options => options.UseNpgsql(conString));

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddHostedService<ConsumeRabbitMQHostedService>();
            services.AddSingleton<AppDbContext>();

            services.AddTransient<ICommandUserRepository, CommandUserRepository>();

            services.AddSingleton<IBackgroundHandler, BackgroundHandler>();
            services.AddSingleton<IRabbitMqConsumer, RabbitMqConsumer>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
