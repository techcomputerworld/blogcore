using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Models;
//using BlogCore.AccesoDatos.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;


namespace BlogCore.AccesoDatos.Data
{
    public class ContenedorTrabajo : IContenedorTrabajo
    {
        private readonly ApplicationDbContext _db;
        public ContenedorTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Categoria = new CategoriaRepository(_db);
            Articulo = new ArticuloRepository(_db);
            Slider = new SliderRepository(_db);
        }
        public ICategoriaRepository Categoria { get; private set; }
        public IArticuloRepository Articulo { get; private set; }
        public ISliderRepository Slider { get; private set; }
        //ICategoriaRepository IContenedorTrabajo.Articulo => throw new NotImplementedException();
        /*esta de debajo es la linea que tengo que agregar si quiero que me funcione ICategoriaRepository IContenedorTrabajo
        * me gustaría saber porque me pasa esto. 
*/
        //ICategoriaRepository IContenedorTrabajo.Articulo => throw new NotImplementedException();

        public void Dispose()
        {
            //Dispose() se usa para liberar recursos y matar el proceso
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
        
    }

}
