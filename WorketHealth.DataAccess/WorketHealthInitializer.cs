using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WorketHealth.DataAccess.Models;
using WorketHealth.DataAccess.Models.Registros;
using WorketHealth.DataAccess.Models.Tablas;

namespace WorketHealth.DataAccess
{
    public class WorketHealthInitializer
    {
        public static void Initialize(WorketHealthContext context, UserManager<AppUsuario> userManager, RoleManager<IdentityRole> roleManager) //
        {
            context.Database.Migrate();
          
            // -- Roles --
            // Verificar si ya existen roles
            if (!roleManager.RoleExistsAsync("Administrador").Result)
            {
                var adminRole = new IdentityRole("Administrador");
                roleManager.CreateAsync(adminRole).Wait();
            }
            if (!roleManager.RoleExistsAsync("Desarrollador").Result)
            {
                var adminRole = new IdentityRole("Desarrollador");
                roleManager.CreateAsync(adminRole).Wait();
            }
            if (!roleManager.RoleExistsAsync("Visitante").Result)
            {
                var adminRole = new IdentityRole("Visitante");
                roleManager.CreateAsync(adminRole).Wait();
            }
            if (!roleManager.RoleExistsAsync("Registrado").Result)
            {
                var adminRole = new IdentityRole("Registrado");
                roleManager.CreateAsync(adminRole).Wait();
            }
          
            // Verificar si ya existen usuarios
            if (userManager.FindByNameAsync("Administrador").Result == null)
            {
                // Crear usuarios
                CreateUser(userManager, "Administrador", "Administrador@example.com", "Qwer@123?", "Administrador");
                CreateUser(userManager, "Desarrollador1", "Desarrollador1@example.com", "Qwer@123?", "Desarrollador");
                CreateUser(userManager, "Desarrollador2", "Desarrollador2@example.com", "Qwer@123?", "Desarrollador");
            }
          
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

            //EnfermedadProfesional
            if (!context.EnfermedadesProfesionales.Any())
            {
                var initialDataEnfermedadesProfesionales = new List<EnfermedadProfesional>
                {
                    new EnfermedadProfesional { COD = "", DESCRIPCION = "Sin Datos" }, // Se evaluara
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

            //EnfermedadComun
            if (!context.EnfermedadesComunes.Any())
            {
                var initialDataEnfermedadesComunes = new List<EnfermedadComun>
                {
                    new EnfermedadComun { COD = "", DESCRIPCION = "Sin Datos" },// Se evaluara   
                    // Agregar más datos aquí
                };

                context.EnfermedadesComunes.AddRange(initialDataEnfermedadesComunes);
                context.SaveChanges();
            }

            //Ruc
            if (!context.Ruc.Any())
            {
                var initialDataRuc = new List<Ruc>
                {
                    new Ruc { COD_RUC = 1, NOM_RUC = "Proyecto-Consorcio", DESCRIPCION_RUC = "Proyecto-Consorcio Vial Puquio" }, 
                    new Ruc { COD_RUC = 2, NOM_RUC = "Proyecto Trujillo", DESCRIPCION_RUC = "Proyecto Trujillo" }, 
                    new Ruc { COD_RUC = 3, NOM_RUC = "Proyecto Servicio de Reciclado", DESCRIPCION_RUC = "Proyecto Servicio de Reciclado y Recapeo CarreteraPuno -Ilave -Desaguadero Tramo: Puno - Ilave, Ilave - desaguadero" }, 
                    new Ruc { COD_RUC = 4, NOM_RUC = "Proyecto Red Vial 6", DESCRIPCION_RUC = "Proyecto Red Vial 6" }, 
                    new Ruc { COD_RUC = 5, NOM_RUC = "Proyecto Piura", DESCRIPCION_RUC = "Proyecto Piura" }, 
                    new Ruc { COD_RUC = 6, NOM_RUC = "Proyecto Coasia", DESCRIPCION_RUC = "Proyecto Coasia" }, 
                    new Ruc { COD_RUC = 7, NOM_RUC = "Proyecto Carumas", DESCRIPCION_RUC = "Proyecto Carumas" }, 
                    new Ruc { COD_RUC = 8, NOM_RUC = "Paquete 4A- Contruccións", DESCRIPCION_RUC = "Paquete 4A- Contrucción de las conducciones de la Quebrada San Idelfonso Para el Proyecto Paquete 1 Soluciones Integrales Quebradas San Idelfonso y San Carlos" }, 
                    new Ruc { COD_RUC = 9, NOM_RUC = "IIRSA Sur Tramo N°05", DESCRIPCION_RUC = "IIRSA Sur Tramo N°05" }, 
                    new Ruc { COD_RUC = 10, NOM_RUC = "Corredor Vial Oxapampa", DESCRIPCION_RUC = "Corredor Vial Oxapampa" }, 
                    new Ruc { COD_RUC = 11, NOM_RUC = "Construcción del Puente Cascajal", DESCRIPCION_RUC = "Construcción del Puente Cascajal de la Via Evitamiento de Chimbote Red Vial N°4: Pativilca - Santa - Trujillo y Puerto Salaverry" }, 
                    new Ruc { COD_RUC = 12, NOM_RUC = "Construcción de la Segunda calsada", DESCRIPCION_RUC = "Construcción de la Segunda calsada de la Autopista del Sol Tramo Trujillo - Sullana -Obras Obligatorias Evitamiento Guadalupe-EV-06e" }, 
                    // Agregar más datos aquí
                };

                context.Ruc.AddRange(initialDataRuc);
                context.SaveChanges();
            }

        }
        private static void CreateUser(UserManager<AppUsuario> userManager, string username, string email, string password, string roleName)
        {
            if (userManager.FindByNameAsync(username).Result == null)
            {
                var user = new AppUsuario
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
