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

        public async Task<UserDto> CreateUser(CreateUserDto createUserDto)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id)
            ?? throw new Exception ("El usuario no existe");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return UserMapper.toUserDto(user);
        }

        public async Task<List<User>> GetAllUser()
        {
            IQueryable<User> userQuery = _context.Users;
            var users = await userQuery.Include(u => u.Gender).ToListAsync();
            return await _context.Users.ToListAsync();
        }

        public async Task<UserDto?> GetUserById(int id)
        {
            var user = await _context.Users.Include(u=> u.Gender)
                .FirstOrDefaultAsync(u=> u.UserId == id);
            return user is null ? null : UserMapper.toUserDto(user);
        }

        public async Task<UserDto> UpdateUser(int id, UpdateUserDto updateUserDto)
        {
            throw new NotImplementedException();
        }
    }
}