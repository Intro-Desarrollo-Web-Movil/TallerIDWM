using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallerIDWM.src.DTOs.Auth;
using TallerIDWM.src.Models;

namespace TallerIDWM.src.Controllers
{
    public class AuthController: BaseApiController
    {
        private readonly UserManager<User> _userManager;
      //  private readonly

      public AuthController(UserManager<User> userManager){
        _userManager = userManager;
      }

      [HttpPost("login")]

      public async Task<IActionResult> Login(LoginDto loginDto){
        try{
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

        }
      }
    }
}