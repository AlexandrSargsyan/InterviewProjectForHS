using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Common.Users
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string Nickname { get; set; }

        public string Password { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public UserType Type { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int PostCount { get; set; }
    }
}
