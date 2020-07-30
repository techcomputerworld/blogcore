using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.Models.ViewModels
{
    /* Home View Model para mostrar los Sliders y mostrar los Articulos en la vista de home lo primero que verá
     * el usuario. 
     */
    public class HomeVM
    {
        public IEnumerable<Slider> Slider { get; set; }
        //Esta variable contendra los articulos o lista de articulos
        public IEnumerable<Articulo> Articulos { get; set; }

    }
}
