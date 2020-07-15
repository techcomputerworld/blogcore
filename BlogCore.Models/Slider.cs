﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BlogCore.Models
{
    public class Slider
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombre Slider")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El estado es obligatorio")]
        [Display(Name = "Estado")]
        public bool Estado { get; set; }
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string UrlImagen { get; set; }
    }
}
