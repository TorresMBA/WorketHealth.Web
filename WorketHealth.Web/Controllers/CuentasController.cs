﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WorketHealth.DataAccess.Models;
namespace WorketHealth.Web.Controllers
{
    public class CuentasController : Controller
    {
        private readonly UserManager<AppUsuario> _userManager;
        private readonly SignInManager<AppUsuario> _signInManager;

        public CuentasController(UserManager<AppUsuario> userManager, SignInManager<AppUsuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Registro(Models.RegistroViewModel rgViewModel)
        {
            if (ModelState.IsValid)
            {
                var usuario = new AppUsuario
                {
                    UserName = rgViewModel.UserName.Replace(" ", ""),
                    Email = rgViewModel.Email,
                };
                var resultado = await _userManager.CreateAsync(usuario, rgViewModel.Password);

                if (resultado.Succeeded)
                {

                    //Esta linea es para la asignacion  del usuario que se registra
                    await _userManager.AddToRoleAsync(usuario, "Registrado");

                    await _signInManager.SignInAsync(usuario, isPersistent: false);
                    return RedirectToAction("login", "Home");                    
                }
                ValidarErrores(resultado);
            }
            return View(rgViewModel);
        }

        private void ValidarErrores(IdentityResult resultado)
        {
            foreach (var error in resultado.Errors)
            {
                ModelState.AddModelError(String.Empty, error.Description);
            }
        }

        //Registro especial admin
        [HttpGet]
        [Authorize(Roles = "Administrador,Desarrollador")]
        public async Task<IActionResult> RegistroAdmin()
        {

            // Paara seleccion De rol
            List<SelectListItem> listaRoles = new List<SelectListItem>();
            listaRoles.Add(new SelectListItem()
            {
                Value = "Administrador",
                Text = "Administrador"
            });
            listaRoles.Add(new SelectListItem()
            {
                Value = "Desarrollador",
                Text = "Desarrollador"
            });
            listaRoles.Add(new SelectListItem()
            {
                Value = "Visitante",
                Text = "Visitante"
            });
            listaRoles.Add(new SelectListItem()
            {
                Value = "Registrado",
                Text = "Registrado"
            });

            Models.RegistroViewModel registroVM = new Models.RegistroViewModel()
            {
                ListaRoles = listaRoles
            };
            return View(registroVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador,Desarrollador")]
        public async Task<ActionResult> RegistroAdmin(Models.RegistroViewModel rgViewModel)
        {
            if (ModelState.IsValid)
            {
                var usuario = new AppUsuario {
                    UserName = rgViewModel.UserName.Replace(" ", ""),
                    Email = rgViewModel.Email,
                };
                var resultado = await _userManager.CreateAsync(usuario, rgViewModel.Password);

                if (resultado.Succeeded)
                {
                    //Para seleccion de rol en el registro
                    if(rgViewModel.RolSeleccionado != null && rgViewModel.RolSeleccionado.Length > 0 && rgViewModel.RolSeleccionado == "Administrador")
                    {
                        await _userManager.AddToRoleAsync(usuario, "Administrador");
                    }
                    else
                    {
                    //Esta linea es para la asignacion  del usuario que se registra
                        await _userManager.AddToRoleAsync(usuario, "Registrado");
                    }                   

                    await _signInManager.SignInAsync(usuario, isPersistent: false);
                    return RedirectToAction("login", "Home");
                }
                ValidarErrores(resultado);
            }

            // Paara seleccion De rol
            List<SelectListItem> listaRoles = new List<SelectListItem>();
            listaRoles.Add(new SelectListItem()
            {
                Value = "Administrador",
                Text = "Administrador"
            });
            listaRoles.Add(new SelectListItem()
            {
                Value = "Desarrollador",
                Text = "Desarrollador"
            });
            listaRoles.Add(new SelectListItem()
            {
                Value = "Visitante",
                Text = "Visitante"
            });
            listaRoles.Add(new SelectListItem()
            {
                Value = "Registrado",
                Text = "Registrado"
            });

            Models.RegistroViewModel registroVM = new Models.RegistroViewModel()
            {
                ListaRoles = listaRoles
            };

            return View(registroVM);
        }

        //Salir o cerra cession de la aplicacion (logout)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> SalirAplicacion()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Login), "Home");
        }
    }
}
