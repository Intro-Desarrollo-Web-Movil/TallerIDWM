using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TallerIDWM.src.Models;
using TallerIDWM.src.ValidationAttributes;

namespace TallerIDWM.src.DTOs
{
    public class UserDto
    {

        public required int Id {get;set;} 

        [StringLength(255, MinimumLength = 8)]
        public required string Name { get; set; }

        [StringLength(255, MinimumLength = 8)]
        public required string Password { get; set; }

        [EmailAddress]
        public required string Email { get; set; }
        [Birthdate]
        public required DateOnly BirthDate { get; set; }
        public bool IsActive { get; set; }
        public required string Role {get; set;} = string.Empty;
        public required string Gender { get; set; } = string.Empty;
        [Rut]
        public required string Rut {get;set;}
        public string Token {get;set;} = null!;
    
    }
}