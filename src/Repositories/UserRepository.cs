using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TallerIDWM.src.Data;
using TallerIDWM.src.DTOs;
using TallerIDWM.src.Interfaces;
using TallerIDWM.src.Mappers;
using TallerIDWM.src.Models;

namespace TallerIDWM.src.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context){
            _context = context;
        }

        public Task<UserDto> CreateUser(CreateUserDto createUserDto)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> GetAllUser()
        {
            IQueryable<User> userQuery = _context.Users;
            var users = await userQuery.Include(u => u.Gender).ToListAsync();
            return await _context.Users.ToListAsync();
        }

        public Task<UserDto?> GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> UpdateUser(int id, UpdateUserDto updateUserDto)
        {
            throw new NotImplementedException();
        }
    }
}