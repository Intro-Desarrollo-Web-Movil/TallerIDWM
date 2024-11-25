using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerIDWM.src.Models;

namespace TallerIDWM.src.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}