using WebAPIProject.Core.DTOs;

namespace WebAPIProject.Contract.Service
{
    public interface IDyeStuffService
    {
       
        Task<DyeStuffSaveResponse> saveOrUpdatesDyeStuff(DyeStuffRequest request);
        Task<DyeStuffDeleteRequest> Delete(DyeStuffDeleteRequest request);
        Task<DyeStuffPaginatedResponse> GetAllDyeStuff(DyeStuffPaginatedRequest request);

    }
}
