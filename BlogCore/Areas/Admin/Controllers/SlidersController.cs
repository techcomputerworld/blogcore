using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.AccesoDatos.Data.Repository;
using BlogCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace BlogCore.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        //con esta variable ya podremos trabajar con subida de archivos
        private readonly IWebHostEnvironment _hostingEnvironment;

        public SlidersController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostingEnvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet] 
        public IActionResult Create()
        {
            return View();
        } 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slider slider)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                
                //Los nombres de las imagenes por id no me gusta, prefiero que sea el usuario el que elija el nombre 
                string nombreArchivo = Guid.NewGuid().ToString();
                var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");
                var extension = Path.GetExtension(archivos[0].FileName);

                using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                {
                    archivos[0].CopyTo(fileStreams);
                }
                slider.UrlImagen = @"\imagenes\sliders\" + nombreArchivo + extension;
                //se podria poner una fecha de creación del slider
                //slider.FechaCreacion = DateTime.Now.ToString();

                _contenedorTrabajo.Slider.Add(slider);
                _contenedorTrabajo.Save();

                return RedirectToAction(nameof(Index));
                
            }
            return View();
        } // fin método Create(Slider slider)
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                //aquí con este método sacamos el slider que es a traves del id del slider
                var slider = _contenedorTrabajo.Slider.Get(id.GetValueOrDefault());
                return View(slider);
            }
            return View();
        }
        public IActionResult Edit(Slider slider)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                var sliderDesdeDb = _contenedorTrabajo.Slider.Get(slider.Id);
                //esto es la hora de subir una imagen
                if (archivos.Count() > 0)
                {
                    //Los nombres de las imagenes por id no me gusta, prefiero que sea el usuario el que elija el nombre 
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\sliders");
                    var extension = Path.GetExtension(archivos[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }
                    slider.UrlImagen = @"\imagenes\sliders\" + nombreArchivo + extension;
                    //se podria poner una fecha de creación del slider
                    //slider.FechaCreacion = DateTime.Now.ToString();
                    _contenedorTrabajo.Slider.Update(slider);
                    _contenedorTrabajo.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    slider.UrlImagen = sliderDesdeDb.UrlImagen;
                }
                _contenedorTrabajo.Slider.Update(slider);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));

               

            }
            return View();
        }
        #region Llamadas a la API
        [HttpGet]
        public IActionResult GetAll()
        {
            //GetAll() método del Repository y del IRespository
            //para acceder a la Categoria hay que ponerlo en articulos.js, categoria.nombre
            return Json(new { data = _contenedorTrabajo.Slider.GetAll() });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            /*Obtener el objeto del slider desde la base de datos esto se ejecuta en el Javascript y se comunica
              con nuestra aplicación web en el backend.*/
            var objDesdeDb = _contenedorTrabajo.Slider.Get(id);
            if (objDesdeDb == null)
            {
                return Json(new { success = false, message = "Error borrando slider" });
            }
            _contenedorTrabajo.Slider.Remove(objDesdeDb);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Slider borrado correctamente" });

        }
        #endregion
    }
}