using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerIDWM.src.Models
{
    /*Esta clase o tabla es necesaria para especificar los Géneros de Usuario existentes
    aplicando los principios normalizacion entre tablas y seguridad de la informacion
    logrando filtrar la información de mejor manera*/

    public class Category
    {
        public int CategoryId { get; set; } // PK
        public string Name { get; set; } = string.Empty;
        

        // No tiene EF Relationships debido a que es inútil almacenar una lista de productos en cada type
        // Se puede filtrar los productos por tipo de otras formas
        // Definiremos estos type en el DataSeeder ya que son estáticos
    }
}