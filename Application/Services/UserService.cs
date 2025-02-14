using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Models.Request;
using Application.Options;
using Microsoft.Extensions.Options;
using Model.Entities;
using Model.Repositories;

namespace Application.Services {
    public class UserService : IUserService{
        public readonly UserRepository _userRepository;
        public readonly IOptions<HashingOptions> _settingsOptions;

        public UserService(UserRepository userRepository, IOptions<HashingOptions> opt){
            _settingsOptions = opt;
            _userRepository = userRepository;

        }
        public async Task AddAsync(User entity) {
            entity.Password = BCrypt.Net.BCrypt.HashPassword(entity.Password + _settingsOptions.Value.SecretKey);
            _userRepository.Add(entity);
            await _userRepository.SaveChangesAsync();
        }

        public async Task<User> GetAsync(int id) {
            var user = await _userRepository.GetAsync(id);
            if (user == null) {
                throw new Exception("Wrong Id: "+id);
            }
            return user;
        }


        public async Task<User> VerifyUserAsync(LoginRequest loginRequest) {
            var user = await GetByEmail(loginRequest.Email);
            if (!IsPasswordValid(loginRequest.Password, user.Password)) throw new Exception("The Password is wrong");
            return user;
        }

        public async Task<User> GetByEmail(string Email) {
            var user = await _userRepository.GetByEmail(Email);
            if (user == null) {
                throw new Exception("The Email is Invalid");
            }
            return user;
        }

        public bool IsPasswordValid(string password, string hashedPassword) {
            return BCrypt.Net.BCrypt.Verify(password + _settingsOptions.Value.SecretKey, hashedPassword);
        }

        public async Task<IEnumerable<User>> GetAllAsync() {
            return await _userRepository.GetAllAsync();
        }
    }
}
