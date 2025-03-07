using WebAPIProject.Core.DTOs;

namespace WebAPIProject.Contract.Service
{
    public interface IDyeingMathodsService
    {


        Task<DyeMethodPaginatedResponse> GetAllDyeingMethod(DyeMethodPaginatedRequest request);
        Task<DyeMethodsaveResponse> SaveOrUpdateDyeingMethod(DyeMethodSaveRequest request);
        Task<DyeMethodDeleteRequest> DeleteDyeingMethod(DyeMethodDeleteRequest request);
        Task<string> GetNextDyeTypeCode();


    }
}
