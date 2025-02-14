using Application.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Responses {
    public class UserResponse {
        public UserDTO User { get; set; }
        public string Token { get; set; }
    }
}
