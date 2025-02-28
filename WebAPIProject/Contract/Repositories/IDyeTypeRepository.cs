using WebAPIProject.Core.Models;

namespace WebAPIProject.Contract.Repositories
{
    public interface IDyeTypeRepository
    {
        IQueryable<DyeType> GetAllActive();
        Task<int> Count(IQueryable<DyeType> query);
        Task<DyeType> GetById(int id);
        Task<int> Add(DyeType dyeType );
        Task<bool> IsExistingName(string DyeTypeName);
        Task<int> UpdateDyeType(DyeType dyeType );

        Task<DyeType> GetMaxDyeTypeAsync(); 
        Task SaveChangesAsync();

    }
}
