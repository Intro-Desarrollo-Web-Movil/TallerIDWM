using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerIDWM.src.DTOs;
using TallerIDWM.src.Models;

namespace TallerIDWM.src.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDto> CreateUser(CreateUserDto createUserDto);
        Task<UserDto> DeleteUser (int id);
        Task<UserDto?> GetUserById(int id);
        Task<List<UserDto>> GetAllUser();
        Task<UserDto> UpdateUser(int id, UpdateUserDto updateUserDto);
        Task<UserDto> GetCurrentUser();
        Task<UserDto> UpdateUserStatus(int id, bool IsActive);
        Task<UserDto?> GetUserByEmail(string email);
    }
}