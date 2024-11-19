using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TallerIDWM.src.Interfaces;
using TallerIDWM.src.Models;
using Microsoft.EntityFrameworkCore;
using TallerIDWM.src.DTOs;
using TallerIDWM.src.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace TallerIDWM.src.Controllers
{

    [Route("api/product")]
    [ApiController]

    public class ProductController
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        [HttpGet("")]
        public async Task<IResult> GetAllProducts(string? name, string? Category, string? sort)
        {
            if (sort != null && sort.ToLower() != "asc" && sort.ToLower() != "desc")
        {
            return TypedResults.BadRequest("El valor de 'sort' debe ser 'asc' o 'desc'.");
        }

        if  (Category != null && 
            Category.ToLower() != "Poleras" && 
            Category.ToLower() != "Gorros" && 
            Category.ToLower() != "Jugueteria" && 
            Category.ToLower() != "Alimentacion" &&
            Category.ToLower() != "Libros")
        {
            return TypedResults.BadRequest("El valor de 'Category' no es válido.");
        }

            var users = await _productRepository.GetAllProducts(name, Category, sort);
            return TypedResults.Ok(users);
        }
        

    }

}