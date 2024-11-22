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
        public static UserDto toUserDto (this User user){
            return new UserDto {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                BirthDate = user.BirthDate,
                IsActive = user.IsActive,
                RoleId = user.RoleId,
                role = user.role,
                GenderId = user.GenderId,
                Gender = user.Gender,
                Rut = user.Rut
            };
        }
        public static User toUser (this UserDto userDto){
            return new User {
                UserId = userDto.UserId,
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password,
                BirthDate = userDto.BirthDate,
                IsActive = userDto.IsActive,
                RoleId = userDto.RoleId,
                role = userDto.role,
                GenderId = userDto.GenderId,
                Gender = userDto.Gender,
                Rut = userDto.Rut
            };
        }
    }
}