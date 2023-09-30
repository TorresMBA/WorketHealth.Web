using WorketHealth.Domain.Entities.Genericos;

namespace WorketHealth.Domain.Entities.Empresa 
{
    public class Company : BaseEntity{

        public string RazonSocial { get; set; }

        public string Ruc { get; set; }
    }
}
