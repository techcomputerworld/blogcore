using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.AccesoDatos.Data.Repository
{
    public interface IArticuloRepository : IRepository<Articulo>
    {
        //IEnumerable<SelectListItem> GetListaCategoria();

        void Update(Articulo articulo);
    }
}
