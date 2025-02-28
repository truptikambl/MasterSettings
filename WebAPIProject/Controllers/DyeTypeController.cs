using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIProject.Contract.Service;
using WebAPIProject.Core.DTOs;

namespace WebAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DyeTypeController : ControllerBase
    {

        private readonly IDyeTypeService _dyeTypeService;


        public DyeTypeController(IDyeTypeService dyeTypeService)
        {
            _dyeTypeService = dyeTypeService;

        }

        [HttpPost("GetAllDyeType")]
        public async Task<DyeTypePaginatedResponse> GetAllDyeType([FromBody] DyeTypePaginatedRequest request)
        {
            return await _dyeTypeService.GetAllDyeType(request);
        }

        [HttpPost("DyeTypeCode")]
         
        public async Task<IActionResult> GetNextDyeTypeCode()
        {
            var nextCode = await _dyeTypeService.GetNextDyeTypeCodeAsync();
            return Ok(new { nextDyeTypeCode = nextCode });
        }



        [HttpPost("SaveDyeType")]
        public async Task<DyeTypesaveResponse> SaveDyeType([FromBody] DyeTypesaveRequest Request)
        {
            return await _dyeTypeService.SaveOrUpdateDyeType(Request);
        }



        [HttpPost("deleteDyeType")]
        public async Task<DyeTypeDeleteRequest> DeleteDyeType([FromBody] DyeTypeDeleteRequest request)
        {
            return await _dyeTypeService.DeleteDyeType(request);
        }
    }
}
