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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int> GetCategoryIdByName(string categoryName)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);
            return category?.CategoryId ?? throw new KeyNotFoundException("Category not found");
        }

        public async Task<string> GetCategoryNameById(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            return category?.Name ?? throw new KeyNotFoundException("Category not found");
        }
    }
}