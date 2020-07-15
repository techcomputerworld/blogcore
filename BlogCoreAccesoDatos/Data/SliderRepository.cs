using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.AccesoDatos.Data
{
    public class SliderRepository : Repository<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext _db;
        /// <summary>
        /// Constructor de la clase SliderRepository pasando el argumento db 
        /// </summary>
        /// <param name="db"></param>
        public SliderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        //El método Update(Entidad entidad) lo suyo es usar un método para cada entidad distinto
        public void Update(Slider slider)
        {
            var objDesdeDb = _db.Slider.FirstOrDefault(s => s.Id == slider.Id);
            objDesdeDb.Nombre = slider.Nombre;
            objDesdeDb.Estado = slider.Estado;
            objDesdeDb.UrlImagen = slider.UrlImagen;
            //para actualizar tenemos que guardar los cambios en la base de datos
            _db.SaveChanges();
            
        }

       
    }
}
