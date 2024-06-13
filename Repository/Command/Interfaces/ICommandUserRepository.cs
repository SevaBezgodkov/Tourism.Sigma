using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Command.Interfaces
{
    public interface ICommandUserRepository
    {
        Task AddAsync();
    }
}
