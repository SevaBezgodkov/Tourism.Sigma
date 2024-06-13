using Repository.Command.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Command
{
    public class CommandUserRepository : ICommandUserRepository
    {
        public Task AddAsync()
        {
            throw new NotImplementedException();
        }
    }
}
