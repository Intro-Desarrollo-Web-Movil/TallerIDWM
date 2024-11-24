using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerIDWM.src.DTOs
{
    public class EnableUserDto
    {
        public required int UserId {get; set;}
        public required bool IsActive {get; set;}
    }
}