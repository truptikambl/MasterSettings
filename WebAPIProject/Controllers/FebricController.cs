using Microsoft.AspNetCore.Mvc;
using WebAPIProject.Contract.Service;
using static WebAPIProject.Core.DTOs.Constructiondto;
using static WebAPIProject.Core.DTOs.FabricDetails;

namespace WebAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FabricController : ControllerBase
    {

        private readonly IFabricService _fabricService;

        public FabricController(IFabricService fabricService)
        {
            _fabricService = fabricService;

        }
        [HttpPost("SaveFabric")]
        public async Task<FabricSaveResponse> SaveFabric([FromBody] FabricRequest fabricRequest)
        {
            return await _fabricService.SaveOrUpdate(fabricRequest);
        }

        [HttpPost("GetAllFabric")]
        public async Task<FabricPaginatedResponse> GetAllFabric([FromBody] FabricPaginationRequest request)
        {
            return await _fabricService.GetAllFabric(request);
        }

        [HttpPost("delete")]
        public async Task<FabricDeleteRequest> DeleteFabric([FromBody] FabricDeleteRequest request)
        {
            return await _fabricService.Delete(request);
        }


        [HttpPost("SaveConstruction")]
        public async Task<ConstructionSaveResponse> SaveConstruction([FromBody] ConstructionRequest fabricRequest)
        {
            return await _fabricService.SaveOrUpdate(fabricRequest);
        }

        [HttpPost("GetAllConstruction")]
        public async Task<ConstructionPaginatedResponse> GetAllConstruction([FromBody] ConstructionPaginationRequest request)
        {
            return await _fabricService.GetAllActiveConstruction(request);
        }
        [HttpPost("DeleteConstruction")]
        public async Task<ConstructionDeleteRequest> Deleteconstruction([FromBody] ConstructionDeleteRequest request)
        {

            return await _fabricService.DeleteConstruction(request);
        }
    }
}
