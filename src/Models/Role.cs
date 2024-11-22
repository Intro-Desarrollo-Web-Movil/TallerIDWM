using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerIDWM.src.Models
{
    /*Esta clase o tabla es necesaria para especificar los roles de Administrador y de Cliente
    aplicando los principios normalizacion entre tablas y seguridad de la informacion*/
    
    public class Role
    {
        // Atributos
        public int RoleId { get; set; } // PK
        public string Name { get; set; } = string.Empty;

        // No tiene EF Relationships debido a que es inútil almacenar una lista de usuarios en cada rol
        // Definiremos estos roles en el DataSeeder ya que son estáticos
    }
}