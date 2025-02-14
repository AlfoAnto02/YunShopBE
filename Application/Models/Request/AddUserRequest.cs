using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request {
    public class AddUserRequest {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public User ToEntity() {
            return new User {
                Username = Name,
                Email = Email,
                Password = Password
            };
        }
    }
}
