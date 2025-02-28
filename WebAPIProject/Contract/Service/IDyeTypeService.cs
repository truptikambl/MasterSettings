using WebAPIProject.Core.DTOs;

namespace WebAPIProject.Contract.Service
{
    public interface IDyeTypeService
    {

        Task<DyeTypePaginatedResponse> GetAllDyeType(DyeTypePaginatedRequest request);
        Task<DyeTypesaveResponse> SaveOrUpdateDyeType(DyeTypesaveRequest request);
        Task<DyeTypeDeleteRequest> DeleteDyeType(DyeTypeDeleteRequest request);

        Task<string> GetNextDyeTypeCodeAsync();

    }
}
