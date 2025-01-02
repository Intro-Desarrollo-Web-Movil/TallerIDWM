using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerIDWM.src.Interfaces
{
    public interface ICategoryRepository
    {
        Task<int> GetCategoryIdByName(string categoryName);
        Task<string> GetCategoryNameById(int categoryId);
    }
}