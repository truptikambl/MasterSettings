using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIProject.Contract.Service;
using WebAPIProject.Core.DTOs;

namespace WebAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DyeStuffController : Controller
    {


        private readonly IDyeStuffService _dyeStuffService;


        public DyeStuffController(IDyeStuffService Service)
        {
            _dyeStuffService = Service;

        }

        [HttpPost("GetAllDyeStuff")]
        public async Task<DyeStuffPaginatedResponse> GetAllDyeStuff([FromBody] DyeStuffPaginatedRequest request)
        {
            return await _dyeStuffService.GetAllDyeStuff(request);
        }

        [HttpPost("SaveDyeStuff")]
        public async Task<DyeStuffSaveResponse> Saveorupdate([FromBody] DyeStuffRequest Request)
        {
            return await _dyeStuffService.saveOrUpdatesDyeStuff(Request);
        }

        [HttpPost("delete")]
        public async Task<DyeStuffDeleteRequest> DeleteDyestuff([FromBody] DyeStuffDeleteRequest request)
        {
            return await _dyeStuffService.Delete(request);
        }
    }
}
