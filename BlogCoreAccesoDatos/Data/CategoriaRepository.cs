using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogCore.AccesoDatos.Data
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly ApplicationDbContext _db;
        /// <summary>
        /// Constructor de la clase CategoriaRepository pasando el argumento db 
        /// </summary>
        /// <param name="db"></param>
        public CategoriaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public IEnumerable<SelectListItem> GetListaCategoria()
        {
            //Aquí me muestra todos los objetos que hay en ListaCategoria, el nombre y su Id.
            return _db.Categoria.Select(i => new SelectListItem() {
                Text = i.Nombre,
                Value = i.Id.ToString()
            }); 
        }

        public void Update(Categoria categoria)
        {
            var objDesdeDb = _db.Categoria.FirstOrDefault(s => s.Id == categoria.Id);
            objDesdeDb.Nombre = categoria.Nombre;
            objDesdeDb.Orden = categoria.Orden;
            //guardar los cambios en la base de datos actualizandolos.
            _db.SaveChanges();
        }
    }
}
