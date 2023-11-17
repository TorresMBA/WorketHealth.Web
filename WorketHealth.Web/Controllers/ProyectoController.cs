using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WorketHealth.Web.Models;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using WorketHealth.DataAccess.Models.Registros;
using WorketHealth.DataAccess;
using WorketHealth.DataAccess.Models.ViewModels;
using WorketHealth.DataAccess.Models.Tablas;

namespace WorketHealth.Web.Controllers
{
    public class ProyectoController : Controller
    {
        private readonly WorketHealthContext _contexto;
        public ProyectoController(WorketHealthContext contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public IActionResult F_SIG_19()
        {
            return RedirectToAction();
        }

        public List<Ruc> ObtenerDatosRuc()
        {
            // Supongamos que Rucs es la DbSet en tu DbContext que representa la tabla de Rucs
            return _contexto.Ruc.Select(p => new Ruc { ID_RUC = p.ID_RUC, NOM_RUC = p.NOM_RUC }).ToList();
        }

        [HttpPost]
        public ActionResult F_SIG_19(IFormFile file)
        {
            
            if (file != null && file.Length > 0)
            {
                try
                {
                    using (var stream = new MemoryStream())
                    {
                        file.CopyTo(stream);
                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                            var excelDataList = new List<F_SEG_19>();

                            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                            {
                                //int numero;

                                if (worksheet.Cells[row, 2].Text != "")
                                {
                                    if (!int.TryParse(worksheet.Cells[row, 2].Text, out int numero))
                                    {
                                        throw new Exception("El Dni solo debe ser numeros");
                                    }

                                    F_SEG_19 data = new F_SEG_19
                                    {
                                        Id = row - 1,
                                        DNI = worksheet.Cells[row, 2].Text,
                                        PrimerNombre = worksheet.Cells[row, 3].Text,
                                        SegundoNombre = worksheet.Cells[row, 4].Text,
                                        PrimerApellido = worksheet.Cells[row, 5].Text,
                                        SegundoApellido = worksheet.Cells[row, 6].Text,
                                        FechaNacimiento = DateTime.Parse(worksheet.Cells[row, 7].Text),
                                        Sexo = worksheet.Cells[row, 8].Text,
                                        TipoExamen = worksheet.Cells[row, 9].Text,
                                        FechaExamen = DateTime.Parse(worksheet.Cells[row, 10].Text),
                                        Area = worksheet.Cells[row, 11].Text,
                                        PuestoDeTrabajo = worksheet.Cells[row, 12].Text,
                                        Aptitud = worksheet.Cells[row, 13].Text,
                                        Restricciones = worksheet.Cells[row, 14].Text,
                                        ID_EC = worksheet.Cells[row, 15].Text,
                                        ID_ERT = worksheet.Cells[row, 16].Text,
                                        ID_EP = worksheet.Cells[row, 17].Text,
                                        RUC = worksheet.Cells[row, 18].Text,
                                        Mes = worksheet.Cells[row, 19].Text,
                                        Anho = worksheet.Cells[row, 20].Text
                                    };
                                    // Validar y convertir las fechas
                                    //DateTime fechaNacimiento, fechaExamen;
                                    //if (DateTime.TryParse(worksheet.Cells[row, 6].Text, out fechaNacimiento))
                                    //{
                                    //    data.FechaNacimiento = fechaNacimiento;
                                    //}
                                    //
                                    //if (DateTime.TryParse(worksheet.Cells[row, 9].Text, out fechaExamen))
                                    //{
                                    //    data.FechaExamen = fechaExamen;
                                    //}
                                    excelDataList.Add(data);
                                }

                                
                            }

                            ViewBag.ExcelData = excelDataList;

                            // Puedes guardar excelDataList en una variable de sesión o en una base de datos temporal si es necesario.

                            return View("F_SIG_19", excelDataList);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones, puedes registrar el error o mostrar un mensaje de error.
                    TempData["Error"] = "Error al importar el archivo Excel. (" + ex.Message +")";

                    return View("F_SIG_19");
                }
            }
            TempData["Error"] = "No se ha seleccionado un archivo Excel.";
            return View("F_SIG_19");

        }
        [HttpGet]
        public FileResult DescargarExcelF_SEG_19()
        {
            // Ruta relativa al archivo Excel en el directorio wwwroot
            var filePath = "~/FormatoExcel/F_SIG_019.xlsx"; // Asegúrate de proporcionar la ruta correcta

            // Obtiene el nombre del archivo sin la ruta
            var fileName = Path.GetFileName(filePath);

            // Devuelve el archivo para su descarga
            return File(filePath, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        // ---------------------------- START TEST -----------------------------------

        [HttpGet]
        public IActionResult TEST()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TEST(IFormFile file)
        {

            if (file != null && file.Length > 0)
            {
                try
                {
                    using (var stream = new MemoryStream())
                    {
                        file.CopyTo(stream);
                        using (var package = new ExcelPackage(stream))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];

                            var excelDataList = new List<TEST>();

                            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                            {
                                TEST data = new TEST
                                {
                                    Id = row - 1,
                                    DNI = worksheet.Cells[row, 1].Text,
                                    NOMBRE = worksheet.Cells[row, 2].Text,
                                    AÑO = worksheet.Cells[row, 3].Text
                                    
                                };

                                excelDataList.Add(data);
                            }

                            ViewBag.ExcelData = excelDataList;

                            return View("TEST", excelDataList);
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "Error al importar el archivo Excel: " + ex.Message;

                    return View("TEST");
                }
            }
            TempData["Error"] = "No se ha seleccionado un archivo Excel.";
            return View("TEST");

        }
        // ---------------------------- END TEST -----------------------------------
    }
}
