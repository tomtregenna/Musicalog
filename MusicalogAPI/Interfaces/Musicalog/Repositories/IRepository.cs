using Microsoft.AspNetCore.Mvc;

namespace MusicalogAPI.Interfaces.Musicalog.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAsync(bool includeGraph, Dictionary<string, string> filters);
        Task<ActionResult<T?>> GetByIdAsync(Guid id);

        Task<IActionResult> InsertAsync(T t);

        Task<IActionResult> UpdateAsync(T t);

        Task<IActionResult> DeleteAsync(Guid id);
    }
}
