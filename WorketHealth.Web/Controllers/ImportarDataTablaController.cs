using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Data;
using WorketHealth.DataAccess.Models.Registros;
using System;
using System.IO;
using System.Xml.Serialization;

namespace WorketHealth.Web.Controllers
{
    public class ImportarDataTablaController : Controller
    {
        [HttpGet]
        [Authorize(Roles = "Desarrollador")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Desarrollador")]
        public IActionResult EnfermedadComun()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Desarrollador")]
        public ActionResult EnfermedadComun(IFormFile file)
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

                            var excelDataList = new List<EnfermedadComun>();

                            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                            {
                                //int numero;

                                if (worksheet.Cells[row, 2].Text != "")
                                {
                                    EnfermedadComun data = new EnfermedadComun
                                    {
                                        COD = worksheet.Cells[row, 1].Text.Trim(),
                                        DESCRIPCION = worksheet.Cells[row, 2].Text.Trim()
                                    };
                                    excelDataList.Add(data);
                                }
                            }

                            ViewBag.ExcelData = excelDataList;


                            return View("EnfermedadComun", excelDataList);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de excepciones, puedes registrar el error o mostrar un mensaje de error.
                    TempData["Error"] = "Error al importar el archivo Excel. (" + ex.Message + ")";

                    return View("EnfermedadComun");
                }
            }
            TempData["Error"] = "No se ha seleccionado un archivo Excel.";
            return View("EnfermedadComun");
        }


        // //    [HttpPost]
        // //    [Authorize(Roles = "Desarrollador")]
        // //    public ActionResult EnfermedadComun(IFormFile file)
        // //    {
        // //        if (file != null && file.Length > 0)
        // //        {
        // //            try
        // //            {
        // //                using (var stream = new MemoryStream())
        // //                {
        // //                    file.CopyTo(stream);
        // //                    using (var package = new ExcelPackage(stream))
        // //                    {
        // //                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
        // //
        // //                        DataTable dt = new DataTable();
        // //                        //dt.Columns.Add("ID", typeof(int));
        // //                        dt.Columns.Add("COD", typeof(string));
        // //                        dt.Columns.Add("DESCIPCION", typeof(string));
        // //
        // //                        for(int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
        // //                        {
        // //                            DataRow row = dt.NewRow();
        // //                            row["COD"] = worksheet.Cells[i, 1].Value.ToString().Trim();
        // //                            row["DESCIPCION"] = worksheet.Cells[i, 2].Value.ToString().Trim();
        // //                            dt.Rows.Add(row);
        // //                        }
        // //                        Session["ExcelData"] = dt;
        // //                        //TempData["ExcelData"] = dt;
        // //                        //
        // //                        //var excelDataList = new List<EnfermedadComun>();
        // //                        //
        // //                        //for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
        // //                        //{
        // //                        //    if (worksheet.Cells[row, 2].Text != "")
        // //                        //    {
        // //                        //        EnfermedadComun data = new EnfermedadComun
        // //                        //        {
        // //                        //            COD = worksheet.Cells[row, 1].Text.Trim(),
        // //                        //            DESCRIPCION = worksheet.Cells[row, 2].Text.Trim()
        // //                        //        };
        // //                        //        excelDataList.Add(data);
        // //                        //    }
        // //                        //}
        // //                        //
        // //                        //TempData["TotalItemCount"] = excelDataList.Count;
        // //                        //
        // //                        //int batchSize = 200; // Tamaño del lote
        // //                        //var batchedData = excelDataList.Take(batchSize).ToList();
        // //                        //TempData["ExcelDataBatch"] = JsonConvert.SerializeObject(batchedData);
        // //                        //
        // //                        //TempData["RemainingData"] = JsonConvert.SerializeObject(excelDataList.Skip(batchSize).ToList());
        // //
        // //                        return RedirectToAction("EnfermedadComun");
        // //                    }
        // //                }
        // //            }
        // //            catch (Exception ex)
        // //            {
        // //                TempData["Error"] = "Error al importar el archivo Excel. (" + ex.Message + ")";
        // //            }
        // //        }
        // //        else
        // //        {
        // //            TempData["Error"] = "No se ha seleccionado un archivo Excel.";
        // //        }
        // //
        // //        return RedirectToAction("EnfermedadComun");
        // //    }

    //    [HttpPost]
    //    [Authorize(Roles = "Desarrollador")]
    //    public IActionResult EnfermedadComun(IFormFile file)
    //    {
    //        if (file != null && file.Length > 0)
    //        {
    //            try
    //            {
    //                using (var stream = new MemoryStream())
    //                {
    //                    file.CopyTo(stream);
    //                    using (var package = new ExcelPackage(stream))
    //                    {
    //                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
    //
    //                        DataTable dt = new DataTable();
    //                        dt.Columns.Add("COD", typeof(string));
    //                        dt.Columns.Add("DESCRIPCION", typeof(string));
    //
    //                        for (int i = worksheet.Dimension.Start.Row + 1; i <= worksheet.Dimension.End.Row; i++)
    //                        {
    //                            DataRow row = dt.NewRow();
    //                            row["COD"] = worksheet.Cells[i, 1].Value.ToString().Trim();
    //                            row["DESCRIPCION"] = worksheet.Cells[i, 2].Value.ToString().Trim();
    //                            dt.Rows.Add(row);
    //                        }
    //
    //                        // Serializa la DataTable a XML y almacénala en la sesión.
    //                        var serializer = new XmlSerializer(typeof(DataTable));
    //                        using (var sw = new StringWriter())
    //                        {
    //                            serializer.Serialize(sw, dt);
    //                            HttpContext.Session.SetString("ExcelData", sw.ToString());
    //                        }
    //
    //                        return RedirectToAction("EnfermedadComun");
    //                    }
    //                }
    //            }
    //            catch (Exception ex)
    //             {
    //                TempData["Error"] = "Error al importar el archivo Excel. (" + ex.Message + ")";
    //            }
    //        }
    //        else
    //        {
    //            TempData["Error"] = "No se ha seleccionado un archivo Excel.";
    //        }
    //
    //        return RedirectToAction("EnfermedadComun");
    //    }
    
}

}
