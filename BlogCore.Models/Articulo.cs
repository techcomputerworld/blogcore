using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogCore.Models
{
    public class Articulo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="El nombre es obligatorio")]
        [Display(Name ="Nombre de artículo")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [Display(Name ="Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = " Fecha de creación")]
        public string FechaCreacion { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]
        public string UrlImagen { get; set; }

        //una categoria puede tener muchos artículos y aquí vamos a realizar una relación de 1 a muchos.
        //El Id de la categoria a la que pertenece el artículo 
        [Required]
        public int CategoriaId { get; set; }
        [ForeignKey("CategoriaId")]
        //A que tabla esta referenciando creamos este campo tal cual
        public Categoria Categoria { get; set; }
    }
}
