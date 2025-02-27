using AuthUsers.Mapper;
using AuthUsers.Services;
using AuthUsers.Services.Interfaces;
using Domain.Services;
using Domain.Services.Interfaces;

namespace AuthUsers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //test
            var builder = WebApplication.CreateBuilder(args);

            var services = builder.Services;

            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddSingleton<IPublisherServiceBus, PublisherServiceBus>();
            services.AddTransient<IUserService, UserService>();

            services.AddAutoMapper(typeof(AppMappingProfile));

            var app = builder.Build();

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
