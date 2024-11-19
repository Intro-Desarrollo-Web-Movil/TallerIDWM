using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TallerIDWM.src.Models;
using TallerIDWM.src.ValidationAttributes;

namespace TallerIDWM.src.DTOs
{
    public class CreateUserDto
    {
        [StringLength(255, MinimumLength = 8)]
        public required string Name { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        [StringLength(20, MinimumLength = 8)]
        public required string Password { get; set; }
        [Birthdate]
        public required DateOnly BirthDate { get; set; }
        public bool IsActive { get; set; }
        
        public int RoleId { get; set; } 
        public required  Role role {get; set;}

        public int GenderId { get; set; } 
        public required Gender Gender { get; set; }
    }
}