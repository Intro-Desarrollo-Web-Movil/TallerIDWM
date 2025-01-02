using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TallerIDWM.src.DTOs
{
    public class CreateProductViewModel
    {
        public CreateProductDto Product { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}