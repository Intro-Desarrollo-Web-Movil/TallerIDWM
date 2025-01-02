using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerIDWM.src.Interfaces
{
    public interface IRoleRepository
    {
        Task<string> GetRoleNameById(int roleId);
        Task<int> GetRoleIdByName(string roleName);

    }
}