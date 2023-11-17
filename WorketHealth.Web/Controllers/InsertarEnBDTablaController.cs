using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WorketHealth.DataAccess;
using WorketHealth.DataAccess.Models.Personal;
using WorketHealth.DataAccess.Models.Registros;

namespace WorketHealth.Web.Controllers
{
    public class InsertarEnBDTablaController : Controller
    {
        private readonly WorketHealthContext _contexto;
        public InsertarEnBDTablaController(WorketHealthContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        [Authorize(Roles = "Desarrollador")]
        public IActionResult Index()
        {
            return View();
        }

     //   [HttpPost]
     //   [Authorize(Roles = "Desarrollador")]
     //   public ActionResult ProcesarPorLotes()
     //   {
     //       // Obtiene los datos del TempData
     //       var excelDataList = TempData["ExcelData"] as List<EnfermedadComun>;
     //
     //       if (excelDataList != null && excelDataList.Any())
     //       {
     //           try
     //           {
     //               foreach (var data in excelDataList)
     //               {
     //                   // Agrega los datos a la base de datos utilizando el contexto inyectado
     //                   _contexto.EnfermedadesComunes.Add(data);
     //               }
     //
     //              // _contexto.SaveChanges(); // Guarda los cambios en la base de datos
     //               TempData["Success"] = "Datos insertados en la base de datos con éxito.";
     //           }
     //           catch (Exception ex)
     //           {
     //               // Manejo de excepciones, puedes registrar el error o mostrar un mensaje de error.
     //               TempData["Error"] = "Error al insertar los datos en la base de datos. (" + ex.Message + ")";
     //           }
     //       }
     //       else
     //       {
     //           TempData["Error"] = "No hay datos para insertar en la base de datos.";
     //       }+
     //
     //       return RedirectToAction("ProcesarDatos"); // Redirige de nuevo a la vista "ProcesarDatos"
     //   }

        [HttpPost]
        [Authorize(Roles = "Desarrollador")]
        public ActionResult CargarDatosEnBDEnfermedadesComunes(IEnumerable<EnfermedadComun> excelDataList)
        {
            if (excelDataList != null )
            {//&& excelDataList.Count > 0               
                //try
                //{
                //    int batchSize = 100; // Establece el tamaño del lote deseado.
                //
                //    for (int i = 0; i < excelDataList.Count; i += batchSize)
                //    {
                //        var batch = excelDataList.Skip(i).Take(batchSize).ToList();
                //
                //        var codigoRepetidos = 0;
                //
                //        foreach (var data in batch)
                //        {
                //            var codigoExistente = _contexto.EnfermedadesComunes.FirstOrDefault(p => p.COD == data.COD.Trim());
                //
                //            if (codigoExistente == null)
                //            {
                //                var nuevaEnfermerdadComun = new EnfermedadComun
                //                {
                //                    COD = data.COD.Trim(),
                //                    DESCRIPCION = data.DESCRIPCION.Trim()
                //                };
                //
                //                _contexto.EnfermedadesComunes.Add(nuevaEnfermerdadComun);
                //            }
                //            else
                //            {
                //                codigoRepetidos++;
                //                TempData["Error"] = "Se encontraron " + codigoRepetidos + " Codigos repetidos.";
                //            }
                //        }
                //
                //        _contexto.SaveChanges();
                //    }
                //
                //    TempData["Correcto"] = "Datos insertados en la base de datos correctamente.";
                //    return RedirectToAction("Index", "ImportarDataTablaController");
                //}
                //catch (Exception ex)
                //{
                //    TempData["Error"] = "Error al insertar en la base de datos: " + ex.Message;
                //}
            }
            else
            {
                TempData["Error"] = "No hay datos para insertar en la base de datos.";
            }

            // Redirige a la vista principal o a donde desees.
            return RedirectToAction("EnfermedadComun", "ImportarDataTabla");
        }

        //[HttpPost]
        //[Authorize(Roles = "Desarrollador")]
        //public IActionResult CargarDatosEnBDEnfermedadesComunes(List<EnfermedadComun> excelDataList)
        //{
        //    if (excelDataList != null && excelDataList.Count > 0)
        //    {
        //        try
        //        {
        //            var codigoRepetidos = 0;
        //            foreach (var data in excelDataList)
        //            {
        //                var codigoExistente = _contexto.EnfermedadesComunes.FirstOrDefault(p => p.COD == data.COD.Trim());

        //                if (codigoExistente == null)
        //                {
        //                    var nuevaEnfermerdadComun = new EnfermedadComun
        //                    {
        //                        COD = data.COD.Trim(),
        //                        DESCRIPCION = data.DESCRIPCION.Trim()
        //                    };

        //                    _contexto.EnfermedadesComunes.Add(nuevaEnfermerdadComun);
        //                }
        //                else
        //                {
        //                    codigoRepetidos++;
        //                    TempData["Error"] = "Se encontraron " + codigoRepetidos + " Codigos repetidos.";
        //                }

        //            }

        //            _contexto.SaveChanges();

        //            TempData["Correcto"] = "Datos insertados en la base de datos correctamente.";
        //            return RedirectToAction("Index", "ImportarDataTablaController");
        //        }
        //        catch (Exception ex)
        //        {
        //            TempData["Error"] = "Error al insertar en la base de datos: " + ex.Message;
        //        }
        //    }
        //    else
        //    {
        //        TempData["Error"] = "No hay datos para insertar en la base de datos.";
        //    }

        //    // Redirige a la vista principal o a donde desees.
        //    return RedirectToAction("EnfermedadComun", "ImportarDataTabla");
        //  }
    }
}
