
using Domain.Models;
using System.Text.Json.Serialization;

namespace AuthUsers.Models
{
    public class UserModel
    {
        public string Login { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string SecondName { get; set; } = null!;
        public int? Age { get; set; }
        public string Password { get; set; } = null!;
    }
}
