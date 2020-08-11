using BlogCore.Models;
using BlogCore.Utilidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogCore.AccesoDatos.Data.Inicializador
{
    //clase inicializadora de los datos
    public class InitDb : IInitDb
    {
        private readonly ApplicationDbContext _db;
        //no entiendo que use IdentityUser cuando nuestro objeto es ApplicationUser
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public InitDb(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Inicializar()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    //este método aplica las migraciones pendientes.
                    _db.Database.Migrate();
                }
            }
            catch (Exception)
            {

            }
            if (_db.Roles.Any(ro => ro.Name == CNT.Admin)) return;
            //crea los roles Admin y Usuario en la tabla de roles
            _roleManager.CreateAsync(new IdentityRole(CNT.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(CNT.Usuario)).GetAwaiter().GetResult();

            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "onzulin@gmail.com",
                Email = "onzulin@gmail.com",
                EmailConfirmed = true,
                Nombre = "José Luis"

            }, "N98smxztLs@An").GetAwaiter().GetResult();
            ApplicationUser usuario = _db.ApplicationUser.
                Where(us => us.Email == "onzulin@gmail.com").FirstOrDefault();

            _userManager.AddToRoleAsync(usuario, CNT.Admin).GetAwaiter().GetResult();

        }
    }
}
