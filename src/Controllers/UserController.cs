using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TallerIDWM.src.Data;
using TallerIDWM.src.Interfaces;
using TallerIDWM.src.Mappers;

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
            var  userDto = users.Select(u=> u.toUserDto());
            return Ok(users);
        }
        //get, update, delete, create
/*
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin", "Cliente")]
        public async Task<IActionResult> deleteProduct (int id){
            var user = await _context.;
            if (user = null){
                return NotFound(new{ Message = "El usuario no existe"});
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Usuario eliminado exitosamente"});
        }
    }
    */
    }
}