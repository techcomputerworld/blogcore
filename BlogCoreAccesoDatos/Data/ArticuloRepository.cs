using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogCore.AccesoDatos.Data
{
    public class ArticuloRepository : Repository<Articulo>, IArticuloRepository
    {
        private readonly ApplicationDbContext _db;
        /// <summary>
        /// Constructor de la clase CategoriaRepository pasando el argumento db 
        /// </summary>
        /// <param name="db"></param>
        public ArticuloRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        

        public void Update(Articulo articulo)
        {
            var objDesdeDb = _db.Articulo.FirstOrDefault(s => s.Id == articulo.Id);
            objDesdeDb.Nombre = articulo.Nombre;
            objDesdeDb.Descripcion = articulo.Descripcion;
            objDesdeDb.UrlImagen = articulo.UrlImagen;
            objDesdeDb.CategoriaId = articulo.CategoriaId;
            //guardar los cambios en la base de datos actualizandolos.
            //Lo comentamos porque el guardado lo vamos a hacer desde el controlador
            //_db.SaveChanges();
        }
    }
}
