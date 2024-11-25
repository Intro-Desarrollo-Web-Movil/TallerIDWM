using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TallerIDWM.src.DTOs.Auth
{
    public class LoginDto
    {
        [EmailAddress]

        public required string Email {get; set;} = null!;

        [StringLength(20, MinimumLength = 8)]
        public required string Password {get;set;} = null!;
    }
}