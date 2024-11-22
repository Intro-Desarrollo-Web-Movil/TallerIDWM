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

namespace TallerIDWM.src.Controllers
{
    public class UserController: BaseApiController
    {
        private readonly IUserRepository _userRepository;


        public UserController (IUserRepository userRepository){
            _userRepository = userRepository;
        }

        [HttpGet]

        public async Task<IActionResult> GetAll(){
            var users = await _userRepository.GetAllUser();
          //  var  userDto = users.Select(u=> u.toUserDto());
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
    }
}