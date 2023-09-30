using System.Linq.Expressions;
using WorketHealth.Domain.Entities.Genericos;

namespace WorketHealth.Domain.Interfaces.Genericos {
    public interface IGenericsRepository<T> where T : BaseEntity {

        /// <summary>
        /// Se encarga de obtener un elemento en base al Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// Se encarga de obtener todos los elementos
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Se encarga de hacer una busqueda en base a una expresión 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Hace un listado paginado
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        Task<(int totalRegistros, IEnumerable<T> registros)> GetAllAsync(int pageIndex, int pageSize, string search);

        /// <summary>
        /// Añade un nuevo elemento
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// Añade varios elementos a la vez
        /// </summary>
        /// <param name="entities"></param>
        void AddRange(IEnumerable<T> entities);

        /// <summary>
        /// Eliminar en base a un elemento recibido
        /// </summary>
        /// <param name="entity"></param>
        void Remove(T entity);

        /// <summary>
        /// Elimina en base a una lista de elementos recibidos
        /// </summary>
        /// <param name="entities"></param>
        void RemoveRange(IEnumerable<T> entities);

        /// <summary>
        /// actualiza el elemento recibido
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);
    }
}
