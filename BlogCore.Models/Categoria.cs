using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication.ExtendedProtection;
using System.Text;

namespace BlogCore.Models
{
    //tabla Categoria en nuestra base de datos
    public class Categoria
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Ingresa un nombre para la categoria")]
        [Display(Name ="Nombre Categoria")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Ingresa un nombre para la categoria")]
        [Display(Name = "Orden de visualización")]
        public int Orden { get; set; }

    }
}
