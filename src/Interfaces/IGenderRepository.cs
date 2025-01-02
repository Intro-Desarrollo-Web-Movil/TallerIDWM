using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerIDWM.src.Models;

namespace TallerIDWM.src.Interfaces
{
    public interface IGenderRepository
    {
        Task<bool> ExistGender(Gender gender);
        Task<string> GetGenderNameById(int genderId);
        Task<int> GetGenderIdByName(string genderName);
    }
        
}