using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorketHealth.DataAccess.Models.Registros;

namespace WorketHealth.DataAccess
{
    public class WorketHealthInitializer
    {
        public static void Initialize(WorketHealthContext context) //, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager
        {
            context.Database.Migrate();
          // //
          // //  // -- Roles --
          // //  // Verificar si ya existen roles
          // //  if (!roleManager.RoleExistsAsync("Administrador").Result)
          // //  {
          // //      var adminRole = new IdentityRole("Administrador");
          // //      roleManager.CreateAsync(adminRole).Wait();
          // //  }
          // //  if (!roleManager.RoleExistsAsync("Desarrollador").Result)
          // //  {
          // //      var adminRole = new IdentityRole("Desarrollador");
          // //      roleManager.CreateAsync(adminRole).Wait();
          // //  }
          // //  if (!roleManager.RoleExistsAsync("Visitante").Result)
          // //  {
          // //      var adminRole = new IdentityRole("Visitante");
          // //      roleManager.CreateAsync(adminRole).Wait();
          // //  }
          // //  if (!roleManager.RoleExistsAsync("Registrado").Result)
          // //  {
          // //      var adminRole = new IdentityRole("Registrado");
          // //      roleManager.CreateAsync(adminRole).Wait();
          // //  }
          // //
          // //  // Verificar si ya existen usuarios
          // //  if (userManager.FindByNameAsync("Administrador").Result == null)
          // //  {
          // //      // Crear usuarios
          // //      CreateUser(userManager, "Administrador", "Administrador@example.com", "AdminPassword123", "Administrador");
          // //      CreateUser(userManager, "Desarrollador1", "Desarrollador1@example.com", "Qwer@123", "Administrador");
          // //      CreateUser(userManager, "Desarrollador2", "Desarrollador2@example.com", "UserPassword123", "Administrador");
          // //  }
          // //
            // Verificar si ya existen datos

            //Aptitud
            if (!context.Aptitudes.Any())
            {
                var initialDataAptitudes = new List<Aptitud>
                {
                    new Aptitud { COD = "APTO", DESCRIPCION = "APTO" },
                    new Aptitud { COD = "NO APTO", DESCRIPCION = "NO APTO" },
                    new Aptitud { COD = "APTO CON RESTRICCIONES", DESCRIPCION = "APTO CON RESTRICCIONES" },
                    new Aptitud { COD = "OBSERVADO", DESCRIPCION = "OBSERVADO" },
                    new Aptitud { COD = "OTRO", DESCRIPCION = "OTRO" },
                    // Agregar más datos aquí
                };

                context.Aptitudes.AddRange(initialDataAptitudes);
                context.SaveChanges();
            }

            //EnfermedadComun
            if (!context.EnfermedadesComunes.Any())
            {
                var initialDataEnfermedadesComunes = new List<EnfermedadComun>
                {
                    new EnfermedadComun { COD = "", DESCRIPCION = "Sin Datos" },// Se evaluara
                    new EnfermedadComun { COD = "CODE1", DESCRIPCION = "PRUEBA1" },// Eliminar - (se usa de prueba)
                    new EnfermedadComun { COD = "CODE2", DESCRIPCION = "PRUEBA2" },// Eliminar - (se usa de prueba)
                    new EnfermedadComun { COD = "CODE3", DESCRIPCION = "PRUEBA3" },// Eliminar - (se usa de prueba)
                    // Agregar más datos aquí
                };

                context.EnfermedadesComunes.AddRange(initialDataEnfermedadesComunes);
                context.SaveChanges();
            }

            //EnfermedadProfesional
            if (!context.EnfermedadesProfesionales.Any())
            {
                var initialDataEnfermedadesProfesionales = new List<EnfermedadProfesional>
                {
                    new EnfermedadProfesional { COD = "", DESCRIPCION = "Sin Datos" }, // Se evaluara
                    new EnfermedadProfesional { COD = "CODE1", DESCRIPCION = "PRUEBA1" },// Eliminar - (se usa de prueba)
                    new EnfermedadProfesional { COD = "CODE2", DESCRIPCION = "PRUEBA2" },// Eliminar - (se usa de prueba)
                    new EnfermedadProfesional { COD = "CODE3", DESCRIPCION = "PRUEBA3" },// Eliminar - (se usa de prueba)
                    // Agregar más datos aquí
                };

                context.EnfermedadesProfesionales.AddRange(initialDataEnfermedadesProfesionales);
                context.SaveChanges();
            }

            //EnfermedadProfesional
            if (!context.EnfermedadesRelacionadasTrabajo.Any())
            {
                var initialDataEnfermedadesRelacionadasTrabajo = new List<EnfermedadRelacionadaTrabajo>
                {
                    new EnfermedadRelacionadaTrabajo { COD = "", DESCRIPCION = "Sin Datos" }, // Se evaluara
                    new EnfermedadRelacionadaTrabajo { COD = "CODE1", DESCRIPCION = "PRUEBA1" },// Eliminar - (se usa de prueba)
                    new EnfermedadRelacionadaTrabajo { COD = "CODE2", DESCRIPCION = "PRUEBA2" },// Eliminar - (se usa de prueba)
                    new EnfermedadRelacionadaTrabajo { COD = "CODE3", DESCRIPCION = "PRUEBA3" },// Eliminar - (se usa de prueba)
                    // Agregar más datos aquí
                };

                context.EnfermedadesRelacionadasTrabajo.AddRange(initialDataEnfermedadesRelacionadasTrabajo);
                context.SaveChanges();
            }

            //EnfermedadRelacionadaTrabajo
            if (!context.EnfermedadesRelacionadasTrabajo.Any())
            {
                var initialDataEnfermedadesRelacionadasTrabajo = new List<EnfermedadRelacionadaTrabajo>
                {
                    new EnfermedadRelacionadaTrabajo { COD = "", DESCRIPCION = "Sin Datos" }, // Se evaluara
                    new EnfermedadRelacionadaTrabajo { COD = "CODE1", DESCRIPCION = "PRUEBA1" },// Eliminar - (se usa de prueba)
                    new EnfermedadRelacionadaTrabajo { COD = "CODE2", DESCRIPCION = "PRUEBA2" },// Eliminar - (se usa de prueba)
                    new EnfermedadRelacionadaTrabajo { COD = "CODE3", DESCRIPCION = "PRUEBA3" },// Eliminar - (se usa de prueba)
                    // Agregar más datos aquí
                };

                context.EnfermedadesRelacionadasTrabajo.AddRange(initialDataEnfermedadesRelacionadasTrabajo);
                context.SaveChanges();
            }

            //TipoExamen
            if (!context.TipoExamenes.Any())
            {
                var initialDataTipoExamenes = new List<TipoExamen>
                {
                    new TipoExamen { COD = "EMPO", DESCRIPCION = "Examen Médico Pre Ocupacional" }, 
                    new TipoExamen { COD = "EMOA", DESCRIPCION = "Examen Médico Ocupacional Anual" },
                    new TipoExamen { COD = "EMOR", DESCRIPCION = "Examen Médico Ocupacional de Retiro" },
                    new TipoExamen { COD = "OTRO", DESCRIPCION = "OTRO" },
                    // Agregar más datos aquí
                };

                context.TipoExamenes.AddRange(initialDataTipoExamenes);
                context.SaveChanges();
            }
        }
        private static void CreateUser(UserManager<IdentityUser> userManager, string username, string email, string password, string roleName)
        {
            if (userManager.FindByNameAsync(username).Result == null)
            {
                var user = new IdentityUser
                {
                    UserName = username,
                    Email = email
                };

                var result = userManager.CreateAsync(user, password).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, roleName).Wait();
                }
            }
        }
    }
}
