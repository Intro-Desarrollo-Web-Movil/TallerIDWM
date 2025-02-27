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
        public required string Name { get; set; } = null!;
        [Birthdate]
        public required DateTime BirthDate { get; set; }
        public required int GenderId { get; set; }
        public required Gender Gender { get; set; } = null!;
    }
}