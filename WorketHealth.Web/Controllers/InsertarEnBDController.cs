using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WorketHealth.DataAccess;
using WorketHealth.DataAccess.Models.Registros;
using WorketHealth.DataAccess.Models.Personal;
//using WorketHealth.Web.Models;

namespace WorketHealth.Web.Controllers
{
    public class InsertarEnBDController : Controller
    {
        private readonly WorketHealthContext _contexto;
        public InsertarEnBDController(WorketHealthContext contexto)
        {
            _contexto = contexto;
        }

        [HttpPost]
        public IActionResult CargarDatosEnBD(List<F_SEG_19> excelDataList)
        {
            //if (excelDataList != null && excelDataList.Count > 0)
            //{
            //    try
            //    {
            //       // foreach (var data in excelDataList)
            //       // {
            //       //     // Realiza el mapeo de propiedades comunes a ambas tablas
            //       //     var tabla1Data = new Tabla1
            //       //     {
            //       //         ColumnaComun = data.ColumnaComun,
            //       //         ColumnaEspecificaTabla1 = data.ColumnaTabla1
            //       //     };
            //       //
            //       //     var tabla2Data = new Tabla2
            //       //     {
            //       //         ColumnaComun = data.ColumnaComun,
            //       //         ColumnaEspecificaTabla2 = data.ColumnaTabla2
            //       //     };
            //       //
            //       //     // Inserta en Tabla1
            //       //     _contexto.Tabla1.Add(tabla1Data);
            //       //
            //       //     // Inserta en Tabla2
            //       //     _contexto.Tabla2.Add(tabla2Data);
            //       // }
            //
            //        _contexto.SaveChanges();
            //
            //        ViewBag.Message = "Datos insertados en la base de datos correctamente.";
            //
            //    }
            //    catch (Exception ex)
            //    {
            //        ViewBag.Error = "Error al insertar en la base de datos: " + ex.Message;
            //    }
            //}
            //else
            //{
            //    ViewBag.Error = "No hay datos para insertar en la base de datos.";
            //}

            // Redirige a la vista principal o a donde desees.

            if (excelDataList != null && excelDataList.Count > 0)
            {
                try
                {
                    // Listas para almacenar datos
                    List<SeguimientoMedico> SeguimientoMedicoDataList = new List<SeguimientoMedico>();
                    List<Seguimiento_EC> seguimiento_ecDataList = new List<Seguimiento_EC>();
                 
                    foreach (var data in excelDataList)
                    {
                        var PersonalData = new Personal
                        {
                            Dni = data.DNI,
                            Primer_Nombre = data.PrimerNombre,
                            Segundo_Nombre = data.SegundoNombre,
                            Primer_Apellido = data.PrimerApellido,
                            Segundo_Apellido = data.SegundoApellido,
                            Fec_Nacimiento = (DateTime)data.FechaNacimiento,
                            //Fec_Nacimiento = data.FechaNacimiento != null ? (DateTime)data.FechaNacimiento : DateTime.MinValue,
                            Sexo = data.Sexo
                        };
                 
                        //         // Crear una nueva entrada en la tabla SeguimientoMedico
                        //         SeguimientoMedico SeguimientoMedicoData = new SeguimientoMedico
                        //         {
                        //             // Asigna las propiedades de SeguimientoMedico a partir de los datos en F_SEG_19
                        //             // Por ejemplo: Nombre = data.Nombre
                        //         };
                        //
                        //         // Divide los IDs separados por comas en data.ID_EC
                        //         string[] idsEC = data.ID_EC.Split(',');
                        //
                        //         foreach (string idEC in idsEC)
                        //         {
                        //             if (int.TryParse(idEC, out int idECInt))
                        //             {
                        //                 // Crea una entrada en la tabla Seguimiento_EC que relaciona SeguimientoMedico y ID_ENFERMEDAD
                        //                 Seguimiento_EC seguimiento_ecData = new Seguimiento_EC
                        //                 {
                        //                     SeguimientoMedico = SeguimientoMedicoData, // Relación con SeguimientoMedico
                        //                     Id_Enfermedad = idECInt // ID de la enfermedad
                        //                 };
                        //
                        //                 seguimiento_ecDataList.Add(seguimiento_ecData);
                        //             }
                        //         }
                        //
                        //         // Agrega la entrada de SeguimientoMedico a la lista
                        //         SeguimientoMedicoDataList.Add(SeguimientoMedicoData);
                 
                 
                 
                        _contexto.Personal.Add(PersonalData);
                    }

           //        // Inserta los datos en SeguimientoMedico
           //        _contexto.SeguimientoMedicos.AddRange(SeguimientoMedicoDataList);
           //
           //        // Inserta los datos en Seguimiento_EC
           //        _contexto.Seguimiento_ECs.AddRange(seguimiento_ecDataList);

                    _contexto.SaveChanges();

                    TempData["Correcto"] = "Datos insertados en la base de datos correctamente.";
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Error al insertar en la base de datos: " + ex.Message;
                }
            }
            else
            {
                TempData["Error"] = "No hay datos para insertar en la base de datos.";
            }

            // Redirige a la vista principal o a donde desees.
            return RedirectToAction("F_SIG_19", "Proyecto");
        }
    }
}
