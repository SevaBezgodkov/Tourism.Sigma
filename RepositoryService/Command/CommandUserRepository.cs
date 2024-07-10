using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RepositoryService.Background.Interfaces;
using RepositoryService.Command.Interfaces;
using System.Text;

namespace RepositoryService.Command
{
    public class CommandUserRepository : ICommandUserRepository
    {
        private readonly AppDbContext _context;

        public CommandUserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(User model)
        {
            model.RoleId = 2;
            model.Id = Guid.NewGuid();

            await _context.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Guid userId, User model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(i => i.Id == userId);

            if(user is not null)
            {
                user.Login = model.Login;
                user.FirstName = model.FirstName;
                user.SecondName = model.SecondName;
                user.Age = model.Age;

                await _context.SaveChangesAsync();
            }
        }
    }
}
