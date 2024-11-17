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
        public int GenderId { get; set; } // FK Gender
        public int RoleId { get; set; } // FK Role
        public bool IsActive { get; set; }
        
        
    }
}