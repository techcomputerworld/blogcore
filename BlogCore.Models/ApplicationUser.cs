using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogCore.Models
{
    //heredando de IdentityUser podremos añadir nuestros propios campos aquí en esta clase que hemos creado
    public class ApplicationUser : IdentityUser
    {
        //añadir campos personalizados
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        [Required(ErrorMessage = "La ciudad es obligatorio")]
        public string Ciudad { get; set; }
        [Required(ErrorMessage = "El pais es obligatorio")]
        public string Pais { get; set; }

    }
}
