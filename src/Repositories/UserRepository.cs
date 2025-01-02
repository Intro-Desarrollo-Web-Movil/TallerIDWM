using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
            var user = UserMapper.toUserFromCreateUser(createUserDto);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return  UserMapper.toUserDto(user);
        }

        /// <summary>
        /// Método que permite eliminar un usuario del sistema
        /// </summary>
        /// <param name="id">id único del usuario</param>
        /// <returns>el usuario eliminado como DTO</returns>
        /// <exception cref="Exception"></exception> <summary>
        public async Task<UserDto> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id)
                ?? throw new Exception("El usuario no existe");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user.toUserDto();
        }

        /// <summary>
        /// Método para obtener todos los usuarios del sistema
        /// </summary>
        /// <returns>todos los usuarios del sistema</returns> <summary>
        public async Task<List<UserDto>> GetAllUser()
        {
            return await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Gender)
                .Select(user => user.toUserDto())
                .ToListAsync();
        }

        public Task<UserDto> GetCurrentUser()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Métodod que obtiene al usuario mediante su ID
        /// </summary>
        /// <param name="id">id único del usuario</param>
        /// <returns>DTO del usuario con el identificador dado</returns>
        public async Task<UserDto?> GetUserById(int id)
        {
            var user = await _context.Users.Include(u=> u.Gender)
                .FirstOrDefaultAsync(u=> u.Id == id);
            return user is null ? null : UserMapper.toUserDto(user);
        }

        public  Task<UserDto> UpdateUser(int id, UpdateUserDto updateUserDto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Método que actualiza el estado del usuario en el sistema
        /// </summary>
        /// <param name="id">id único del usuario</param>
        /// <param name="IsActive">atributo booleano que indica si el usuario está activo o deshabilitado</param>
        /// <returns>Dto del usuario actualizado</returns>
        public async Task<UserDto> UpdateUserStatus(int id, bool IsActive)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id)
                ?? throw new Exception("El usuario no existe");
            user.IsActive = IsActive;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return UserMapper.toUserDto(user);
        }

        /// <summary>
        /// Método que obtiene a un usuario mediante su email
        /// </summary>
        /// <param name="email"> correo eléctronico del usuario</param>
        /// <returns>retorna si encuentra un usuario o no</returns>
        public async Task<UserDto?> GetUserByEmail(string email){
            var user = await _context.Users.FirstOrDefaultAsync(U=> U.Email == email);
            return user is null ? null : UserMapper.toUserDto(user);
        }
    }
}