using Microsoft.AspNetCore.Mvc;
using WebAPIProject.Contract.Service;
using WebAPIProject.Core.DTOs;

namespace WebAPIProject.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class DyeingMethodController : ControllerBase
    {

        private readonly IDyeingMathodsService dyeingMathodsService;


        public DyeingMethodController(IDyeingMathodsService dyeingMathodsService)
        {
            this.dyeingMathodsService = dyeingMathodsService ?? throw new ArgumentNullException(nameof(dyeingMathodsService));
        }



        [HttpPost("GetAllDyeingMathod")]
        public async Task<DyeMethodPaginatedResponse> GetAllDyeingMethod([FromBody] DyeMethodPaginatedRequest request)
        {
            return await dyeingMathodsService.GetAllDyeingMethod(request);
        }

        [HttpPost("DyeDyeingMathod")]

        public async Task<IActionResult> GetNextDyeTypeCode()
        {
            var nextCode = await dyeingMathodsService.GetNextDyeTypeCode();
            return Ok(new { nextDyeTypeCode = nextCode });
        }



        [HttpPost("SaveDyeingMathod")]
        public async Task<DyeMethodsaveResponse> SaveDyeingMethod([FromBody] DyeMethodSaveRequest Request)
        {
            return await dyeingMathodsService.SaveOrUpdateDyeingMethod(Request);
        }



        [HttpPost("deleteDyeingMathod")]
        public async Task<DyeMethodDeleteRequest> DeleteDyeingMethod([FromBody] DyeMethodDeleteRequest request)
        {
            return await dyeingMathodsService.DeleteDyeingMethod(request);
        }
    }

}

