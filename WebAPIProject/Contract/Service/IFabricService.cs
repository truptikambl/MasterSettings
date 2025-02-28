using static WebAPIProject.Core.DTOs.Constructiondto;
using static WebAPIProject.Core.DTOs.FabricDetails;

namespace WebAPIProject.Contract.Service
{
    public interface IFabricService
    {
  
        Task<FabricDeleteRequest> Delete(FabricDeleteRequest request);
  
        Task<FabricPaginatedResponse> GetAllFabric(FabricPaginationRequest request);
        Task<FabricSaveResponse> SaveOrUpdate(FabricRequest fabricRequest);




        // Construction

        Task<ConstructionSaveResponse> SaveOrUpdate(ConstructionRequest request);
        Task<ConstructionDeleteRequest> DeleteConstruction(ConstructionDeleteRequest request);
        Task<ConstructionPaginatedResponse> GetAllActiveConstruction(ConstructionPaginationRequest request);
    }
}
