using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerIDWM.src.Data;
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
                Password = user.Password,
                Email = user.Email,
                BirthDate = user.BirthDate,
                IsActive = user.IsActive,
                Rut = user.Rut,
                Role = user.Role.Name,
                Gender = user.Gender.Name
            };
        }

        /// <summary>
        /// Método para pasar de userDto a entidad usuario
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public static User toUser (this UserDto userDto, DataContext context){
            var role = context.Roles.FirstOrDefault(r => r.Name == userDto.Role);
            var gender = context.Genders.FirstOrDefault(g => g.Name == userDto.Gender);

            if (role == null || gender == null)
            {
                throw new Exception("Role or Gender not found");
            }

            return new User
            {
                Name = userDto.Name,
                Password = userDto.Password,
                Email = userDto.Email,
                BirthDate = userDto.BirthDate,
                IsActive = userDto.IsActive,
                Rut = userDto.Rut,
                RoleId = role.Id,
                GenderId = gender.GenderId
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