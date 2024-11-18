using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerIDWM.src.DTOs
{
    public class UserDto
    {
        public int UserId { get; set; } // PK
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; }
        public bool IsActive { get; set; }



        // Entityframework Relationships : Relaciones para interactuar entre Modelos


        // Relación User y Role
        public int RoleId { get; set; } // FK Role
        

        // Relación User y Gender
        public int GenderId { get; set; } // FK Gender

    }
}