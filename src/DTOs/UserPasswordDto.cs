using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TallerIDWM.src.DTOs
{
    public class UserPasswordDto
    {
        
        [StringLength(20, MinimumLength = 8)]
        public required string CurrentPassword {get; set;}
        [StringLength(20, MinimumLength = 8)]
        public required string NewPassword {get; set;}
        [StringLength(20, MinimumLength = 8)]
        public required string ConfirmPassword {get; set;}
    }
}