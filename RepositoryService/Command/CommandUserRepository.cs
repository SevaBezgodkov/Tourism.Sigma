using Domain.Models;
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

            await _context.AddAsync(model);
            await _context.SaveChangesAsync();
        }
    }
}
