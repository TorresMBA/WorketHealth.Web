using WorketHealth.DataAccess.Implementations.Repositories;
using WorketHealth.Domain.Entities.Empresa;
using WorketHealth.Domain.Interfaces.Empresa;

namespace WorketHealth.Services.Services.Empresa {
    public class CompanyServices {

        public async Task<List<Company>> tester()
        {
            EmpresaRepository tester = new EmpresaRepository();
            var result = (await tester.GetAllAsync()).ToList();

            return result;
        }
    }
}
