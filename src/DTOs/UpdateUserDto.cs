using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TallerIDWM.src.Models;
using TallerIDWM.src.ValidationAttributes;

namespace TallerIDWM.src.DTOs
{
    public class UpdateUserDto
    {
        [StringLength(255, MinimumLength = 8)]
        public required string Name { get; set; }
        [Birthdate]
        public required DateOnly BirthDate { get; set; }
        public int GenderId { get; set; } 
        public required Gender Gender { get; set; }
    }
}