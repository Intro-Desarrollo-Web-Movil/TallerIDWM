using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerIDWM.src.Models
{
    
    public class User
    {
        // Atributos
        public int UserId { get; set; } // PK
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }
        public bool IsActive { get; set; } = true;



        // Entityframework Relationships : Relaciones para interactuar entre Modelos


        // Relación User y Role
        public int RoleId { get; set; } // FK Role
        public Role role {get; set;} = null!; // Relación a 1

        // Relación User y Gender
        public int GenderId { get; set; } // FK Gender
        public Gender Gender { get; set; } = null!; // Relación a 1

        public string Rut {get; set;} = null!;
    
    }
}