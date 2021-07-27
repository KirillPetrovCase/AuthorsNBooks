using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthorsNBooks.Data.Contracts
{
    public interface IController<TEntity> where TEntity : class, IEntity
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAllAsync();

        Task<ActionResult<TEntity>> GetByIdAsync(int id);

        Task<ActionResult> DeleteByIdAsync(int id);

        Task<ActionResult> AddAsync(TEntity entity);

        Task<ActionResult> UpdateAsync(int id, int secondId);
    }
}