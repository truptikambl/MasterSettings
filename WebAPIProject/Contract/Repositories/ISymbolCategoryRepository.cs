using WebAPIProject.Core.Models;

namespace WebAPIProject.Contract.Repositories
{
    public interface ISymbolCategoryRepository
    {
        IQueryable<SymbolCategory> GetAllActive();
        Task<int> Count(IQueryable<SymbolCategory> query);
        Task<SymbolCategory> GetById(int SymbolCategoryId);
        Task<int> AddsymbolCategory(SymbolCategory symbolCategory);
        Task<int> UpdateSymbolCategory(SymbolCategory UpdateSymbolCategory);
        Task<bool> IsExistingName(string SymbolCategoryName);
        Task SaveChangesAsync();


        // IndividualCareSymbol

        IQueryable<IndividualCareSymbol> GetAllActiveIndividual();
        Task<int> CountIndividual(IQueryable<IndividualCareSymbol> query);
        Task<IndividualCareSymbol> GetByIdIndividual(int SymbolCode);
        Task<int> AddIndividual(IndividualCareSymbol IndividualCareSymbol);
        Task<int> UpdateIndividual(IndividualCareSymbol UpdateIndividualCareSymbol);
        Task<bool> IsExistingNameIndividual(string name);


    }
}
