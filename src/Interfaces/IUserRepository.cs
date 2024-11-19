using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerIDWM.src.DTOs;

namespace TallerIDWM.src.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDto> CreateUser(CreateUserDto createUserDto);
        Task<UserDto> DeleteUser (int id);
        Task<UserDto?> GetUserById(int id);
        Task<UserDto> UpdateUser(int id, UpdateUserDto updateUserDto);
    }
}