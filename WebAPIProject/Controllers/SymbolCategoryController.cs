using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIProject.Contract.Repositories;
using WebAPIProject.Contract.Service;
using WebAPIProject.Core.DTOs;

namespace WebAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SymbolCategoryController : ControllerBase
    {

        private readonly ISymbolCategoryService _symbolCategoryService;
    

        public SymbolCategoryController(ISymbolCategoryService symbolCategoryService)
        {
            _symbolCategoryService = symbolCategoryService;

        }

        [HttpPost("GetAllSymbolCategory")]
        public async Task<SymbolCategoryPaginatedResponse> GetAllFabric([FromBody] SymbolCategoryPaginatedRequest request)
        {
            return await _symbolCategoryService.GetAllSymbolCategory(request);
        }

        [HttpPost("SymbolCategory")]
        public async Task<SymbolCategorySaveResponse> SymbolCategory([FromBody] SymbolCategoryRequest Request)
        {
            return await _symbolCategoryService.saveOrUpdate(Request);
        }

       

        [HttpPost("delete")]
        public async Task<SymbolCategoryDeleteRequest> DeleteFabric([FromBody] SymbolCategoryDeleteRequest request)
        {
            return await _symbolCategoryService.Delete(request);
        }

        // IndividualCareSymbol

        [Authorize]
        [HttpPost("GetAllIndividualCareSymbol")]
        public async Task<IndividualCareSymbolPaginatedResponse> GetAllIndividualCareSymbol([FromBody] IndividualCareSymbolPaginationRequest request)
        {
            return await _symbolCategoryService.GetAllIndividualCareSymbol(request);
        }

        
        [HttpPost("SaveIndividualCareSymbol")]
        public async Task<IndividualCareSymbolSaveResponse> SaveIndividualCareSymbol([FromBody] IndividualCareSymbolRequest Request)
        {
           
            return await _symbolCategoryService.saveOrUpdates(Request);
        }



        [HttpPost("deleteIndividualCareSymbol")]
        public async Task<IndividualCareSymbolDeleteRequest> DeleteIndividualCareSymbol([FromBody] IndividualCareSymbolDeleteRequest request)
        {
            return await _symbolCategoryService.Delete(request);
        }


    }
}
