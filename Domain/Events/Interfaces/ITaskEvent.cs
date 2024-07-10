using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Events.Interfaces
{
    public interface ITaskEvent<T>
    {
        T Model { get; set; }
        static string EventName { get; set; }
    }
}
