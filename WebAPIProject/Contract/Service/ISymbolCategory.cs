using WebAPIProject.Core.DTOs;


namespace WebAPIProject.Contract.Service
{
    public interface ISymbolCategoryService
    {

        Task<SymbolCategoryPaginatedResponse> GetAllSymbolCategory(SymbolCategoryPaginatedRequest request);

        Task<SymbolCategorySaveResponse> saveOrUpdate(SymbolCategoryRequest request);

        Task <SymbolCategoryDeleteRequest> Delete (SymbolCategoryDeleteRequest request);


        // IndividualCareSymbol


        Task<IndividualCareSymbolPaginatedResponse> GetAllIndividualCareSymbol(IndividualCareSymbolPaginationRequest request);

        Task<IndividualCareSymbolSaveResponse> saveOrUpdates(IndividualCareSymbolRequest request);

        Task<IndividualCareSymbolDeleteRequest> Delete(IndividualCareSymbolDeleteRequest request);
    }
}



