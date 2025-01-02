using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerIDWM.src.DTOs;
using Microsoft.AspNetCore.Identity;


namespace TallerIDWM.src.Models
{
    
    public class User : IdentityUser<int>
    {
        // Atributos
        //public int UserId { get; set; } // PK (SERÁ STRING POR EL IDENTITY USER)
        public string Name { get; set; } = string.Empty;
        public new string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }
        public bool IsActive { get; set; } = true;



        // Entityframework Relationships : Relaciones para interactuar entre Modelos


        // Relación User y Role
        public int RoleId { get; set; } // FK Role
        public IdentityRole<int> Role {get; set;} = null!; // Relación a 1

        // Relación User y Gender
        public int GenderId { get; set; } // FK Gender
        public Gender Gender { get; set; } = null!; // Relación a 1

        public string Rut {get; set;} = null!;
    }
}