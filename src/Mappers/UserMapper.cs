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
        /// <summary>
        /// Método para pasar de entidad usuario a DTO 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns> <summary>
        public static UserDto toUserDto (this User user){
            return new UserDto {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                BirthDate = user.BirthDate,
                IsActive = user.IsActive,
                role = user.Role,
                Gender = user.Gender,
                Rut = user.Rut,
                RoleId = user.RoleId,
                GenderId = user.GenderId
            };
        }

        /// <summary>
        /// Método para pasar de userDto a entidad usuario
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public static User toUser (this UserDto userDto){
            return new User {
                Name = userDto.Name,
                Email = userDto.Email,
                BirthDate = userDto.BirthDate,
                IsActive = userDto.IsActive,
                Role = userDto.role,
                Gender = userDto.Gender,
                Rut = userDto.Rut,
                RoleId = userDto.RoleId,
                GenderId = userDto.GenderId
            };
        }

        /// <summary>
        /// Método para pasar de createdUserDto a entidad usuario
        /// </summary>
        /// <param name="createUserDto"></param>
        /// <returns></returns> <summary>
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