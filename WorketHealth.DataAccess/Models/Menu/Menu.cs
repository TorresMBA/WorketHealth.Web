
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorketHealth.DataAccess.Models;
using WorketHealth.Domain.Entities.Genericos;

namespace WorketHealth.Domain.Entities
{
    public class Menu: BaseEntity
    {
                    
        public string Nombre { get; set; }        
        public int Nivel { get; set; }
        public int Id_Padre { get; set; }

        public int Controlador { get; set; }
        public int Accion { get; set; }

        
       public IEnumerable<UserMenuAccess> UserMenuAccess { get; set; }
    }
}
