using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogCore.AccesoDatos.Data
{
    public class UsuarioRepository : Repository<ApplicationUser>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _db;
        /// <summary>
        /// Constructor de la clase CategoriaRepository pasando el argumento db 
        /// </summary>
        /// <param name="db"></param>
        public UsuarioRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        //se podria mejorar indicando el tiempo de bloqueo o algo así por ejemplo con otro parametro o con varios
        //parametros.
        public void BloquearUsuario(string IdUsuario)
        {
            var usuarioDesdeDb = _db.ApplicationUser.FirstOrDefault(u => u.Id == IdUsuario);
            //bloquearemos el usuario durante 2 dias se puede usar otros como: AddYears para bloquearlo por años
            usuarioDesdeDb.LockoutEnd = DateTime.Now.AddDays(2);
            _db.SaveChanges();
        }

        public void DesbloquearUsuario(string IdUsuario)
        {
            var usuarioDesdeDb = _db.ApplicationUser.FirstOrDefault(u => u.Id == IdUsuario);
            //bloquearemos el usuario durante 2 dias se puede usar otros como: AddYears para bloquearlo por años
            usuarioDesdeDb.LockoutEnd = DateTime.Now;
            _db.SaveChanges();
        }
    }
}
