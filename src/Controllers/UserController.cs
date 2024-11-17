using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TallerIDWM.src.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController (ApplicatoinDbContext context){
            _context = context;
        }
        //get, update, delete, create

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
}