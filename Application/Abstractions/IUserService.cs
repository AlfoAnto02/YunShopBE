using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Request;
using Model.Entities;

namespace Application.Abstractions {
    public interface IUserService : GeneralService<User>{
        public Task<User> GetByEmail(string Email);
        public Task<User> VerifyUserAsync(LoginRequest loginRequest);
    }
}
