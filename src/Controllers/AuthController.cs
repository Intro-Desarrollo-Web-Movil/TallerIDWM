using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TallerIDWM.src.DTOs;
using TallerIDWM.src.DTOs.Auth;
using TallerIDWM.src.Interfaces;
using TallerIDWM.src.Models;

namespace TallerIDWM.src.Controllers
{
    public class AuthController: BaseApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly  ITokenService _tokenService;

      public AuthController(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager){
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
      }

      [HttpPost("login")]

      public async Task<IActionResult> Login(LoginDto loginDto){
        try{
          if(!ModelState.IsValid) return BadRequest(ModelState);
          var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
          if(user == null) return Unauthorized("Invalid username or password");


          var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
          if(!result.Succeeded) return Unauthorized("Invalid user or password");

          return Ok(
            new UserDto{
              UserId = user.UserId,
              Name = user.Name!,
              Email = user.Email!,
              BirthDate = user.BirthDate,
              role = user.role,
              Gender = user.Gender,
              Rut = user.Rut,
              Token = _tokenService.CreateToken(user)
            }
          );
        }
        catch(Exception ex){
          return StatusCode(500, ex.Message);
        }
      }
    }
}