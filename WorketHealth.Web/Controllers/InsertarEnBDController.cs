using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WorketHealth.DataAccess;
using WorketHealth.DataAccess.Models.Registros;
using WorketHealth.DataAccess.Models.Personal;


using WorketHealth.Web.Models;
using WorketHealth.DataAccess.Models.ViewModels;// PONER EN NOTA

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
        public IActionResult CargarDatosEnBD(ViewF_SEG_19 excelDataList1)
        {
            List<F_SEG_19> excelDataList = excelDataList1.F_SEG_19;

            if (excelDataList != null && excelDataList.Count > 0)
            {
                try
                {
                   // // // Listas para almacenar datos
                   // // List<SeguimientoMedico> SeguimientoMedicoDataList = new List<SeguimientoMedico>();
                   // // List<SeguimientoEnfermedad> seguimiento_ecDataList = new List<SeguimientoEnfermedad>();


                    var dniRepetidos = 0;
                    foreach (var data in excelDataList)
                    {

                        // Verifica si la persona ya existe en la tabla "Personal" por su DNI
                        var personaExistente = _contexto.Personal.FirstOrDefault(p => p.Dni == data.DNI.Trim());

                        if (personaExistente == null) //(!DniExists(data.DNI))
                        {
                            // La persona no existe, crea un nuevo registro en "Personal"
                            var nuevaPersona = new Personal
                            {
                                Dni = data.DNI.Trim(),
                                Primer_Nombre = data.PrimerNombre,
                                Segundo_Nombre = data.SegundoNombre,
                                Primer_Apellido = data.PrimerApellido,
                                Segundo_Apellido = data.SegundoApellido,
                                Fec_Nacimiento = (DateTime)data.FechaNacimiento,
                                Sexo = data.Sexo
                            };

                            _contexto.Personal.Add(nuevaPersona);
                        }
                        else
                        {
                            // La persona ya existe en "Personal".
                            dniRepetidos++;
                            TempData["Error"] = "Se encontraron " + dniRepetidos + " DNI repetidos.";
                        }


                        // Busca el registro en la tabla TipoExamen donde el COD coincida con el código del Excel
                        var tipoExamen = _contexto.TipoExamenes.FirstOrDefault(te => te.COD == data.TipoExamen.Trim());

                        if (tipoExamen == null)
                        {
                            // Si no se encontró un registro con el código proporcionado, intenta buscar con "OTRO"
                            tipoExamen = _contexto.TipoExamenes.FirstOrDefault(te => te.COD == "OTRO");
                        }
                        int idTipoExamen = tipoExamen.ID_TIPO;

                        // Intenta buscar el registro en la tabla Aptitud donde la descripción coincida con el valor del Excel
                        var aptitud = _contexto.Aptitudes.FirstOrDefault(a => a.DESCRIPCION == data.Aptitud.Trim());

                        if (aptitud == null)
                        {
                            // Si no se encontró una aptitud con la descripción proporcionada, intenta buscar con "OTRO"
                            aptitud = _contexto.Aptitudes.FirstOrDefault(a => a.DESCRIPCION == "OTRO");
                        }

                        // Se encontró una Aptitud, obtén su ID_APTITUD
                        int idAptitud = aptitud.ID_APTITUD;

                        // Ahora puedes agregar el registro de "SeguimientoMedico"
                        var seguimientoMedico = new SeguimientoMedico
                        {
                            DNI = data.DNI,
                            ID_TIPO_EXAMEN = idTipoExamen,
                            FECHA_EXAM = (DateTime)data.FechaExamen,
                            AREA = data.Area,
                            PUESTO_DE_TRABAJO = data.PuestoDeTrabajo,
                            ID_SEG_APT = idAptitud,
                            RESTRICIONES = data.Restricciones,
                            RUC = data.RUC,
                            MES = data.Mes,
                            ANHO = data.Anho
                        };
                        //------------------------------------------------------------------------
                        seguimientoMedico.Enfermedades = new List<SeguimientoEnfermedad>();

                        // Dividir el campo ID_EC en códigos separados por comas
                        var codigosEC = data.ID_EC?.Split(',').Select(code => code.Trim());

                        if (codigosEC != null)
                        {
                            foreach (var codigoEC in codigosEC)
                            {
                                // Busca el código en la tabla "EnfermedadComun"
                                var ec = _contexto.EnfermedadesComunes.FirstOrDefault(ec => ec.COD == codigoEC);

                                if (ec != null)
                                {
                                    var seguimientoEnfermedad = new SeguimientoEnfermedad
                                    {
                                        EnfermedadComun = ec
                                    };

                                    seguimientoMedico.Enfermedades.Add(seguimientoEnfermedad);
                                }
                            }
                        }

                        //_contexto.SeguimientoMedicos.Add(seguimientoMedico);

                        //--------------------------------------------------------------------------

                        seguimientoMedico.EnfermedadesTrabajo = new List<SeguimientoEnfermedadTrabajo>();

                        // Dividir el campo ID_ERT en códigos separados por comas
                        var codigosERT = (data.ID_ERT ?? "").Split(',').Select(code => code.Trim());

                        //if (codigosERT != null)
                        //{
                        foreach (var codigoERT in codigosERT)
                        {
                            // Busca el código en la tabla "EnfermedadComun"
                            var ert = _contexto.EnfermedadesRelacionadasTrabajo.FirstOrDefault(ert => ert.COD == codigoERT);

                            if (ert != null)
                            {
                                var seguimientoEnfermedadTrabajo = new SeguimientoEnfermedadTrabajo
                                {
                                    EnfermedadRelacionadaTrabajo = ert
                                };

                                seguimientoMedico.EnfermedadesTrabajo.Add(seguimientoEnfermedadTrabajo);
                            }
                        }
                        //}

                        //--------------------------------------------------------------------------

                        seguimientoMedico.EnfermedadesProfesionales = new List<SeguimientoEnfermedadProfesional>();

                        // Dividir el campo ID_EP en códigos separados por comas
                        var codigosEP = (data.ID_EP ?? "").Split(',').Select(code => code.Trim());
                        //if (codigosEP != null)
                        //{
                        foreach (var codigoEP in codigosEP)
                        {
                            // Busca el código en la tabla "EnfermedadComun"
                            var ep = _contexto.EnfermedadesProfesionales.FirstOrDefault(ep => ep.COD == codigoEP);

                            if (ep != null)
                            {
                                var seguimientoEnfermedadProfesional = new SeguimientoEnfermedadProfesional
                                {
                                    EnfermedadProfesional = ep
                                };

                                seguimientoMedico.EnfermedadesProfesionales.Add(seguimientoEnfermedadProfesional);
                            }
                        }
                        //}

                        _contexto.SeguimientoMedicos.Add(seguimientoMedico);

                    }


                        _contexto.SaveChanges();

                    TempData["Correcto"] = "Datos insertados en la base de datos correctamente.";
                    return RedirectToAction("Index", "ImportarData");
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
        // private bool DniExists(string dni)
        // {
        //     return _contexto.Personal.Any(p => p.Dni == dni);
        // }

        [HttpPost]
        public IActionResult CargarDatosEnBDTEST(List<TEST> excelDataList)
        {
            if (excelDataList != null && excelDataList.Count > 0)
            {
                try
                {
                    foreach (var data in excelDataList)
                    {
                        var nuevoTest = new TEST
                        {
                            DNI = data.DNI.Trim(),
                            NOMBRE = data.NOMBRE.Trim(),
                            AÑO = data.AÑO
                        };
                    }

                    TempData["Correcto"] = "Datos insertados en la base de datos correctamente.";
                    return RedirectToAction("Index", "ImportarData");
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

            return RedirectToAction("TEST", "Proyecto");
        }
    }

}
