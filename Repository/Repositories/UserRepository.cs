using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationsContext _context;

        public UserRepository(ApplicationsContext context)
        {
            _context = context;
        }

        private async Task Add(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
