using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs {
    public class UserDTO {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public UserDTO(User user) {
            Id = user.Id;
            UserName = user.Username;
            Email = user.Email;
            Password = user.Password;
        }
    }
}
