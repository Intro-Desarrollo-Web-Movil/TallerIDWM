using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.src.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TallerIDWM.src.Data;
using TallerIDWM.src.DTOs;
using TallerIDWM.src.Interfaces;
using TallerIDWM.src.Repositories;


namespace TallerIDWM.src.Controllers
{
    public class UserController: BaseApiController
    {
        private readonly IUserRepository _userRepository;


        /// <summary>
        /// Constructor de la clase  UserController
        /// </summary>
        /// <param name="userRepository">repostiorio del usuario</param>
        /// <param name="genderRepository">repositorio del género</param> <summary>
        public UserController (IUserRepository userRepository){
            _userRepository = userRepository;

        }
        
        // OBTENER TODOS LOS USUARIOS
        // GET: api/user
        [HttpGet("")]
        public async Task<IResult> GetAllUsers(string? name, int pageSize = 10, int pageNumber = 1)
        {
            var users = await _userRepository.GetAllUser(name, pageSize, pageNumber);
            var totalItems = await _userRepository.CountUsers(name);

            if (users == null || !users.Any())
            {
                return TypedResults.NotFound("No se encontraron usuarios.");
            }

            var response = new
            {
                TotalItems = totalItems,
                PageSize = pageSize,
                PageNumber = pageNumber,
                TotalPages = (int)Math.Ceiling((double)totalItems / pageSize),
                Users = users
            };

            return TypedResults.Ok(response);
        }

        // BORRAR USUARIO POR ID
        // DELETE: api/user/delete/{id}
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute]int id){
            var user = await _userRepository.GetUserById(id);
            if (user is null){
                return NotFound();
            }
            var deletedUser = await _userRepository.DeleteUser(id);
            return Ok(deletedUser);
        }

        // ACTUALIZAR ESTADO DE USUARIO
        // PUT: api/user/status/{id}
        [HttpPut("status/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserStatus([FromRoute] int id, [FromBody]EnableUserDto enableUserDto){
            var user = await _userRepository.GetUserById(id);

            if(user is null){
                return NotFound();
            }

            var status = enableUserDto.IsActive;
            var updateUser = await _userRepository.UpdateUserStatus(id,status); 
            return Ok(updateUser);
        }

        // CREAR USUARIO
        // POST: api/user/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromForm] CreateUserDto createUserDto){
            var email = createUserDto.Email;
            if(await _userRepository.GetUserByEmail(email) is not null){
                return Conflict("El email ya está registrado en el sistema");
            }
            var user = await _userRepository.CreateUser(createUserDto);

            return Created($"/api/user/{user.Id}", user);
        }

        // ACTUALIZAR USUARIO
        // PUT: api/user/update/{id}
        [HttpPut("update/{id}")]
        [Authorize(Roles = "Admin, Customer")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UpdateUserDto updateUserDto){
            var user = await _userRepository.GetUserById(id);
            if(user is null){
                return NotFound();
            }
            var updatedUser = await _userRepository.UpdateUser(id, updateUserDto);
            return Ok(updatedUser);
        }
    }
}