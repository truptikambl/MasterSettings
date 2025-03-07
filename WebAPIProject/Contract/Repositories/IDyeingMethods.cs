using WebAPIProject.Core.Models;

namespace WebAPIProject.Contract.Repositories
{
    public interface IDyeingMethods
    {

        IQueryable<DyeingMethod> GetAllActive();
        Task<int> Count(IQueryable<DyeingMethod> query);
        Task<DyeingMethod> GetById(int id);
        Task<int> Add(DyeingMethod dyeType);
        Task<bool> IsExistingName(string DyeingMathod);
        Task<int> UpdateDyeingMathod(DyeingMethod dyeingMathod);
        Task SaveChangesAsync();
        Task<DyeingMethod> GetMaxDyeType();
        
    }
}
