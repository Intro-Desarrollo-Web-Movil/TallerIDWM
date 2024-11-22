using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerIDWM.src.Models
{
    /*Esta clase o tabla es necesaria para especificar los Géneros de Usuario existentes
    aplicando los principios normalizacion entre tablas y seguridad de la informacion
    logrando filtrar la información de mejor manera*/

    public class Gender
    {
        // Atributos
        public int GenderId { get; set; } // PK
        public string Name { get; set; } = string.Empty;
        
    }
}