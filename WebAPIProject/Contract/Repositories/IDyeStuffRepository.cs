using WebAPIProject.Core.DTOs;
using WebAPIProject.Core.Models;

namespace WebAPIProject.Contract.Repositories
{
    public interface IDyeStuffRepository
    {
        object response { get; }

        IQueryable<DyeStuff> GetAllActiveDyeStuff();
        Task<int> CountDyeStuff(IQueryable<DyeStuff> query);
        Task<DyeStuff> GetByIdDyeStuff(int Id);
        Task<int> AddDyeStuff(DyeStuff DyeStuf);
        Task<int> UpdateDyeStuff(DyeStuff DyeStuff);
        Task<bool> IsExistingNameDyeStuff(string DyeStuffsNmame);
        Task<int> SaveChangesAsync();

    }
}
