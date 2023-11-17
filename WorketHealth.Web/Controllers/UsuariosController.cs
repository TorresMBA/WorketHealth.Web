using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorketHealth.DataAccess;
using WorketHealth.DataAccess.Models;

namespace WorketHealth.Web.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private readonly UserManager<AppUsuario> _userManager;
        private readonly WorketHealthContext _contexto;

        public UsuariosController(UserManager<AppUsuario> userManager, WorketHealthContext contexto)
        {
            _userManager = userManager;
            _contexto = contexto;
        }
        [Authorize(Roles = "Administrador,Desarrollador")]
        [HttpGet]        
        public async Task<IActionResult> Index()
        {
            var usuarios = await _contexto.Users.ToListAsync();
            var rolesUsuario = await _contexto.UserRoles.ToListAsync();
            var roles = await _contexto.Roles.ToListAsync();
            List<Models.AppUsuario> listaUsuarios = new List<Models.AppUsuario>();

            foreach (var usuario in usuarios)
            {
                var rol = rolesUsuario.FirstOrDefault(u => u.UserId == usuario.Id);
                var appUsuario = new Models.AppUsuario {
                    Id = usuario.Id,
                    UserName = usuario.UserName,
                    Email = usuario.Email,
                    LockoutEnd = usuario.LockoutEnd,
                };

                if (rol == null)
                {
                    appUsuario.Rol = "Ninguno";
                }
                else
                {
                    //appUsuario.Rol = roles.FirstOrDefault(u => u.Id == rol.RoleId).Name;
                    // Si hay un rol, busca su nombre en la lista de roles
                    var roleName = roles.FirstOrDefault(r => r.Id == rol.RoleId)?.Name;
                    appUsuario.Rol = roleName ?? "Ninguno";
                }
                listaUsuarios.Add(appUsuario);
            }

            return View(listaUsuarios);
        }


        //editar Usuario (Asignacion de rol)
        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Editar(string id)
        {
            var usuarioBD = _contexto.Users.FirstOrDefault(u => u.Id == id);
            if(usuarioBD == null)
            {
                return NotFound();
            }
            Models.AppUsuario appUsuario = new Models.AppUsuario();
            appUsuario.UserName = usuarioBD.UserName;
            appUsuario.Email = usuarioBD.Email;
           
            //Obtener los roles actuales del usuario
            var rolUsuario = _contexto.UserRoles.ToList();
            var roles = _contexto.Roles.ToList();
            var rol = rolUsuario.FirstOrDefault(u => u.UserId == usuarioBD.Id);
            if (rol != null)
            {
                appUsuario.IdRol = roles.FirstOrDefault(u => u.Id == rol.RoleId).Id;
            }

            appUsuario.ListaRoles = _contexto.Roles.Select(u => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = u.Name,
                Value = u.Id
            });
            return View(appUsuario);
        }
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Models.AppUsuario usuario)
        {
            
            if (ModelState.IsValid)
            {
                var usuarioBD = _contexto.Users.FirstOrDefault(u => u.Id == usuario.Id);
                if (usuarioBD == null)
                {
                    return NotFound();
                }

                var rolUsuario = _contexto.UserRoles.FirstOrDefault(u => u.UserId == usuarioBD.Id);
                if (rolUsuario != null)
                {
                    //Obtener el rol actual

                    var rolActual = _contexto.Roles.Where(u => u.Id == rolUsuario.RoleId).Select(e => e.Name).FirstOrDefault();
                    // Elimnar el rol actual
                    await _userManager.RemoveFromRoleAsync(usuarioBD, rolActual);               
                }
                //Agregar usuario al nuevo rol seleccionado
                await _userManager.AddToRoleAsync(usuarioBD, _contexto.Roles.FirstOrDefault(u => u.Id == usuario.IdRol).Name);
                _contexto.SaveChanges();
                TempData["Correcto"] = "El rol se edito correctamente";
                return RedirectToAction("Index");
            }

            usuario.ListaRoles = _contexto.Roles.Select(u => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = u.Name,
                Value = u.Id
            });
            TempData["Error"] = "No se pudo editar el rol del usuario";
            return View(usuario);
        }

        //Metodo bloquear/desbloquear usuario
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BloquearDesbloquear(string idUsuario)
        {
            var usuarioBD = _contexto.Users.FirstOrDefault(u => u.Id == idUsuario);
            if(usuarioBD == null)
            {
                return NotFound();
            }
            if(usuarioBD.LockoutEnd != null && usuarioBD.LockoutEnd > DateTime.Now)
            {
                //El usuario se encuentra bloqueado y lo podemos desbloquear
                usuarioBD.LockoutEnd = DateTime.Now;
                TempData["Correcto"] = "Usuario Desbloqueado correctamente";
            }
            else
            {
                //El usuario no esta bloqueado y lo podemos bloquear
                usuarioBD.LockoutEnd = DateTime.Now.AddYears(100);
                TempData["Error"] = "Usuario Bloqueado correctamente";
            }
            _contexto.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //Metodo bloquear/desbloquear usuario
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Borrar(string idUsuario)
        {
            var usuarioBD = _contexto.Users.FirstOrDefault(u => u.Id == idUsuario);
            if (usuarioBD == null)
            {
                return NotFound();
            }
            
            _contexto.Users.Remove(usuarioBD);

            _contexto.SaveChanges();
            TempData["Correcto"] = "Usuario Borrado correctamente";
            return RedirectToAction(nameof(Index));
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
        public async Task<ActionResult> CambiarPassword(Models.CambiarPasswordViewModel cpViewModel, string email)
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
