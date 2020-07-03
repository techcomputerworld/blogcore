using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BlogCore.AccesoDatos.Data.Repository
{
    //una interfaz que tiene que tener acceso la clase Repository con los métodos que vamos a usar 
    public interface IRepository<T> where T : class
    {
        //método para obtener una tupla o un registro
        T Get(int id);
        //método para obtener todos las tuplas o registros, ya sea filtrados con alguna expresión o ordenandolos.
        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null
        );
        //Métdo opara obtener el primer registro o tupla
        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
        );
        //Método para añadir un registro o tupla a nuestra base de datos
        void Add(T entity);
        //Método para borrar un registro o tupla (en este caso lo suyo es quitarla de que este disponible 
        //y no borrarla. A través de su id
        void Remove(int id);
        //Método para borrar un registro o tupla (en este caso lo suyo es quitarla de que este disponible 
        //y no borrarla. A través de su entidad
        void Remove(T entity);
    }
}
