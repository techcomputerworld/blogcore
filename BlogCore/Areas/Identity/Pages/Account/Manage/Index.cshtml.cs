using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BlogCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlogCore.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [Display(Name = "Email de Usuario")]
        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Teléfono")]
            public string PhoneNumber { get; set; }
            public string Nombre { get; set; }
            public string Direccion { get; set; }
            public string Ciudad { get; set; }
            [Display(Name = "País")]
            public string Pais { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;
            //poniendo los campos Nombre, Ciudad, Direccion lo que estamos haciendo es mostrar los campos que hay
            //en la base de datos, los datos que tiene.
            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Nombre = user.Nombre,
                Ciudad = user.Ciudad,
                Direccion = user.Direccion,
                Pais = user.Pais
            };
        }
        /// <summary>
        /// Load data the object on the async method OnGetAsync()
        /// Cargar los datos del objeto en el método asincrono OnGetAsync()
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            //podemos colocar varios if de este tipo para los demás campos según las necesidades que vea en la
            //aplicación.
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }
            //esto es para poder actualizar los cambios que pongamos en la vista de los campos correspondientes.
            //google translate
            //this is to be able to update the changes that we put in the view of the corresponding fields.
            user.Nombre = Input.Nombre;
            user.PhoneNumber = Input.PhoneNumber;
            user.Ciudad = Input.Ciudad;
            user.Direccion = Input.Direccion;
            user.Pais = Input.Pais;

            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            //StatusMessage = "Your profile has been updated";
            StatusMessage = "Su perfil ha sido actualizado";
            return RedirectToPage();
        }
    }
}
