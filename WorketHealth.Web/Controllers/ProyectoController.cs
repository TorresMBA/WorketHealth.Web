using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using WorketHealth.Web.Models;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using WorketHealth.DataAccess.Models.Registros;

namespace WorketHealth.Web.Controllers
{
    public class ProyectoController : Controller
    {
        [HttpGet]
        public IActionResult F_SIG_19()
        {
            return View();
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
                                F_SEG_19 data = new F_SEG_19
                                {
                                    Id = row-1,
                                    DNI = worksheet.Cells[row, 1].Text,
                                    PrimerNombre = worksheet.Cells[row, 2].Text,
                                    SegundoNombre = worksheet.Cells[row, 3].Text,
                                    PrimerApellido = worksheet.Cells[row, 4].Text,
                                    SegundoApellido = worksheet.Cells[row, 5].Text,
                                    FechaNacimiento = DateTime.Parse(worksheet.Cells[row, 6].Text),
                                    Sexo = worksheet.Cells[row, 7].Text,
                                    TipoExamen = worksheet.Cells[row, 8].Text,
                                    FechaExamen = DateTime.Parse(worksheet.Cells[row, 9].Text),
                                    Area = worksheet.Cells[row, 10].Text,
                                    PuestoDeTrabajo = worksheet.Cells[row, 11].Text,
                                    Aptitud = worksheet.Cells[row, 12].Text,
                                    Restricciones = worksheet.Cells[row, 13].Text,
                                    ID_EC = worksheet.Cells[row, 14].Text,
                                    ID_ERT = worksheet.Cells[row, 15].Text,
                                    ID_EP = worksheet.Cells[row, 16].Text,
                                    RUC = worksheet.Cells[row, 17].Text,
                                    Mes = worksheet.Cells[row, 18].Text,
                                    Anho = worksheet.Cells[row, 19].Text
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

                            ViewBag.ExcelData = excelDataList;

                            // Puedes guardar excelDataList en una variable de sesión o en una base de datos temporal si es necesario.

                            return View("F_SIG_19", excelDataList);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones, puedes registrar el error o mostrar un mensaje de error.
                    TempData["Error"] = "Error al importar el archivo Excel: " + ex.Message;

                    return View("F_SIG_19");
                }
            }
            TempData["Error"] = "No se ha seleccionado un archivo Excel.";
            return View("F_SIG_19");

        }

    }
}
