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
        [EmailAddress]
        public required string Email { get; set; }
        [Birthdate]
        public required DateOnly BirthDate { get; set; }
        public bool IsActive { get; set; }
        public required IdentityRole<int> role {get; set;}
        public required int RoleId {get; set;}
        public required Gender Gender { get; set; }
        public required int GenderId { get; set; }
        [Rut]
        public required string Rut {get;set;}
        public string Token {get;set;} = null!;
    
    }
}