using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WorketHealth.DataAccess;

namespace WorketHealth.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class RolesController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly WorketHealthContext _contexto;
        public RolesController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, WorketHealthContext contexto)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _contexto = contexto;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var roles = _contexto.Roles.ToList();
            return View(roles);
        }
        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(IdentityRole rol)
        {
            if (await _roleManager.RoleExistsAsync(rol.Name))
            {
                TempData["Error"] = "El rol ya existe";
                return RedirectToAction(nameof(Index));
            }

            await _roleManager.CreateAsync(new IdentityRole() { Name = rol.Name });
            TempData["Correcto"] = "Rol Creado correctamente";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Editar(String id)
        {
            if (String.IsNullOrEmpty(id))
            {
                   return View();
            }
            else
            {
                // Actualizar el rol
                var rolBd = _contexto.Roles.FirstOrDefault(r => r.Id == id);
                return View(rolBd);
            }
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(IdentityRole rol)
        {
            if (await _roleManager.RoleExistsAsync(rol.Name))
            {
                TempData["Error"] = "El rol ya existe";
                return RedirectToAction(nameof(Index));
            }

            var rolBd = _contexto.Roles.FirstOrDefault(r => r.Id == rol.Id);

            if (rolBd == null)
            {
                return RedirectToAction(nameof(Index));
            }

            rolBd.Name = rol.Name;
            rolBd.NormalizedName = rol.Name.ToUpper();
            var resultado = await _roleManager.UpdateAsync(rolBd);
            TempData["Correcto"] = "Rol editado correctamente";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Borrar(string id)
        {
            var rolBd = _contexto.Roles.FirstOrDefault(r => r.Id == id);

            if (rolBd == null)
            {
                TempData["Correcto"] = "No existe el rol";
                return RedirectToAction(nameof(Index));
            }

            var usuariosParaEsteRol = _contexto.UserRoles.Where(u => u.RoleId == id).Count();
            if(usuariosParaEsteRol > 0)
            {
                TempData["Error"] = "El rol tiene usuarios, no se puede borrar";
                return RedirectToAction(nameof(Index));
            }

            await _roleManager.DeleteAsync(rolBd);
            TempData["Correcto"] = "Se borro el rol correctamente";
            return RedirectToAction(nameof(Index));
        }
    }
}
