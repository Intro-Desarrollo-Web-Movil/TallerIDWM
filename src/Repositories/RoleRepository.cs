using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TallerIDWM.src.Data;
using TallerIDWM.src.Interfaces;

namespace TallerIDWM.src.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;

        public RoleRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<string> GetRoleNameById(int roleId)
        {
            var role = await _context.Roles.FindAsync(roleId);
            return role?.Name ?? throw new KeyNotFoundException("Role not found");
        }

        public async Task<int> GetRoleIdByName(string roleName)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
            return role?.Id ?? throw new KeyNotFoundException("Role not found");
        }
    }
}