using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string SecondName { get; set; } = null!;
        public int? Age { get; set; }
        public string Password { get; set; } = null!;

        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;
    }
}
