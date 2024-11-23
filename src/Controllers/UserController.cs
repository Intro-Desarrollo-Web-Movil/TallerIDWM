using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TallerIDWM.src.Data;
using TallerIDWM.src.DTOs;
using TallerIDWM.src.Interfaces;
using TallerIDWM.src.Mappers;
using TallerIDWM.src.Models;
using TallerIDWM.src.Repositories;

namespace TallerIDWM.src.Controllers
{
    public class UserController: BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IGenderRepository _genderRepository;


        public UserController (IUserRepository userRepository, GenderRepository genderRepository){
            _userRepository = userRepository;
            _genderRepository = genderRepository;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            var users = await _userRepository.GetAllUser();
            return Ok(users);
        }

        [HttpDelete ("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute]int id){
            var user = await _userRepository.GetUserById(id);
            if (user is null){
                return NotFound();
            }
            var deletedUser = await _userRepository.DeleteUser(id);
            return Ok(deletedUser);
        }

        [HttpPut("{id}")]
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

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto){
            var email = createUserDto.Email;
            if(await _userRepository.GetUserByEmail(email) is not null){
                return Conflict("El email ya está registrado en el sistema");
            }
            var gender = createUserDto.Gender;
            if (!await _genderRepository.ExistGender(gender)){
                return BadRequest("El género no existe.");
            }
            var user = await _userRepository.CreateUser(createUserDto);

            return Created($"/api/user/{user.UserId}", user);
        }
    }
}