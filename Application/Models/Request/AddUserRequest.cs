using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request {
    public class AddUserRequest {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Session_Id { get; set; } = string.Empty;
        public int Cart_Id { get; set; }

        public User ToEntity() {
            return new User {
                Username = UserName,
                Email = Email,
                Password = Password,
                Phone = PhoneNumber,
                Role = "User",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Session_Id = Session_Id,
                Cart_Id = Cart_Id
            };
        }
    }
}
