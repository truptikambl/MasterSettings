using WebAPIProject.Core.Models;

namespace WebAPIProject.Contract.Repositories
{
    public interface IFabricRepository
    {

        IQueryable<Fabricc> GetAllActive();
       Task<int> Count(IQueryable<Fabricc> query);
        Task<Fabricc> GetById(int FabricId);
        Task<int> Add(Fabricc fabric);
        Task<int> UpdateFabric(Fabricc updatedFabric);

        Task<bool> IsExistingName(string FabricType);


        //Construction 


        Task<int> Count(IQueryable<Construction> query);
        Task<Construction> GetByIdConstruction(int ConStructionId);
        Task<int> Add(Construction construction);
        Task<int> UpdateConstruction(Construction Construction);
        Task<bool> IsExistingType(string ConstructionType);
       // IQueryable<Construction> GetAllConstruction();
        IQueryable<Construction> GetAllActiveConstruction();
        Task SaveChangesAsync();
        

    }
}
