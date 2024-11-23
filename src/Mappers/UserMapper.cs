using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerIDWM.src.DTOs;
using TallerIDWM.src.Models;

namespace TallerIDWM.src.Mappers
{
    public static class UserMapper
    {
        // Para retornar datos al client
        public static UserDto toUserDto (this User user){
            return new UserDto {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                BirthDate = user.BirthDate,
                IsActive = user.IsActive,
                role = user.role,
                Gender = user.Gender,
                Rut = user.Rut
            };
        }
        public static User toUser (this UserDto userDto){
            return new User {
                Name = userDto.Name,
                Email = userDto.Email,
                BirthDate = userDto.BirthDate,
                IsActive = userDto.IsActive,
                role = userDto.role,
                Gender = userDto.Gender,
                Rut = userDto.Rut
            };
        }

        public static User toUserFromCreateUser (this CreateUserDto createUserDto){
            return new User {
                Name = createUserDto.Name,
                Email = createUserDto.Email,
                Password = createUserDto.Password,
                BirthDate = createUserDto.BirthDate,
                IsActive = true,
                RoleId = createUserDto.RoleId,
                GenderId = createUserDto.GenderId,
                Rut = createUserDto.Rut
            };
        }
    }
}