using System.Linq.Expressions;
using WorketHealth.Domain.Entities.Empresa;
using WorketHealth.Domain.Interfaces.Empresa;

namespace WorketHealth.DataAccess.Implementations.Repositories 
{
    public class EmpresaRepository : IEmpresaRepository {

        public void Add(Company entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<Company> entities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Company> Find(Expression<Func<Company, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Company>> GetAllAsync()
        {
            IEnumerable<Company> companies = new List<Company>() { 
                new Company() { Id = 1, RazonSocial = "a", Ruc = "12312312" },
                new Company() { Id = 2, RazonSocial = "b", Ruc = "31231232" },
                new Company() { Id = 3, RazonSocial = "c", Ruc = "43545465" },
            };

            return Task.FromResult(companies);
        }

        public Task<(int totalRegistros, IEnumerable<Company> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
        {
            throw new NotImplementedException();
        }

        public Task<Company> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Company entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Company> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(Company entity)
        {
            throw new NotImplementedException();
        }
    }
}
