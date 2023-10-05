using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorketHealth.DataAccess;
using WorketHealth.Web.Models;

namespace WorketHealth.Web.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly WorketHealthContext _contexto;

        public UsuariosController(UserManager<IdentityUser> userManager, WorketHealthContext contexto)
        {
            _userManager = userManager;
            _contexto = contexto;
        }
        public IActionResult Index()
        {
            return View();
        }

        //Editar Perfil
        [HttpGet]
        public IActionResult EditarPerfil(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var usuarioBd = _contexto.Users.Find(id);
            if (usuarioBd == null)
            {
                return NotFound();
            }
            return View(usuarioBd);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> EditarPerfil(IdentityUser identityUsuario)
        {
            if (ModelState.IsValid) 
            {
                var usuario = await _contexto.Users.FindAsync(identityUsuario.Id);
                usuario.UserName = identityUsuario.UserName.Replace(" ", "");
                //usuario.Email = identityUsuario.Email;

                await _userManager.UpdateAsync(usuario);

                return RedirectToAction(nameof(Index),"Home");
            }
            return View(identityUsuario);
        }
        //Editar Contraseña
        [HttpGet]
        [Authorize]
        public IActionResult CambiarPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> CambiarPassword(CambiarPasswordViewModel cpViewModel, string email)
        {
            if (ModelState.IsValid)
            {

                var _email = await _userManager.FindByNameAsync(email);
                email = _email.Email;

                var usuario = await _userManager.FindByEmailAsync(email);
                if(usuario == null)
                {
                    return RedirectToAction("Error");
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);
                var resultado = await _userManager.ResetPasswordAsync(usuario, token, cpViewModel.Password);

                if (resultado.Succeeded)
                {
                    return RedirectToAction("ConfirmacionCambioPassword");
                }
                else
                {
                    foreach (var error in resultado.Errors)
                    {
                        if (error.Code == "PasswordRequiresNonAlphanumeric")
                        {
                            ModelState.AddModelError(string.Empty, "Las contraseñas deben tener al menos un carácter no alfanumérico.");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        
                    }
                    return View(cpViewModel);
                }
            }
            return View(cpViewModel);
        }
        [HttpGet]
        public IActionResult ConfirmacionCambioPassword()
        {
            return View();
        }
    }
}
