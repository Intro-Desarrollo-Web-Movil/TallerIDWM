using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TallerIDWM.src.Data;
using TallerIDWM.src.Interfaces;
using TallerIDWM.src.Models;

namespace TallerIDWM.src.Repositories
{
    public class GenderRepository: IGenderRepository
    {
        private readonly DataContext _context;

        public GenderRepository(DataContext context){
            _context = context;
        }

        public async Task<bool> ExistGender(Gender gender)
        {
            return await _context.Genders.AnyAsync(x => x.Name == gender.Name);
        }
    }
}