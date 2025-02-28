using Microsoft.EntityFrameworkCore;
using WebAPIProject.Contract.Repositories;
using WebAPIProject.Core.Models;
using WebAPIProject.Infrastructure.Data;

namespace WebAPIProject.Infrastructure.Repositories
{
    public class SymbolCategoryRepository : ISymbolCategoryRepository
    {

        private readonly ApplicationDbContext _context;

        public SymbolCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public  async Task<int> AddsymbolCategory(SymbolCategory symbolCategory)
        {
             await _context.SymbolCategory.AddAsync(symbolCategory);
             await _context.SaveChangesAsync();
            return symbolCategory.SymbolCategoryId;
        }

        public  async Task<int> Count(IQueryable<SymbolCategory> query)
        {
            return query.Count();
        }

        public IQueryable<SymbolCategory> GetAllActive()
        {
            return _context.SymbolCategory.Where(c => c.IsActive);
        }

        public async Task<SymbolCategory> GetById(int SymbolCategoryId)
        {
            return await _context.SymbolCategory.FirstOrDefaultAsync(c => c.SymbolCategoryId == SymbolCategoryId);
        }

        public async Task<bool> IsExistingName(string SymbolCategoryName)
        {
            return await _context.SymbolCategory.Where(x => x.IsActive).AnyAsync(s =>s.SymbolCategoryName.Trim()== SymbolCategoryName);
        }

        public  async Task<int> UpdateSymbolCategory(SymbolCategory UpdateSymbolCategory)
        {
            _context.SymbolCategory.Update(UpdateSymbolCategory);
            await _context.SaveChangesAsync();
            return UpdateSymbolCategory.SymbolCategoryId;
        }
        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }



        // IndividualCareSymbol
        public IQueryable<IndividualCareSymbol> GetAllActiveIndividual()
        {
           return _context.individualCareSymbols;
        }

        public async  Task<int> CountIndividual(IQueryable<IndividualCareSymbol> query)
        {
            return query.Count();
        }

        public  async Task<IndividualCareSymbol> GetByIdIndividual(int SymbolCode)
        {
            return await _context.individualCareSymbols.FirstOrDefaultAsync(c => c.SymbolCode == SymbolCode);
        }

        public  async Task<int> AddIndividual(IndividualCareSymbol IndividualCareSymbol)
        {

            await _context.individualCareSymbols.AddAsync(IndividualCareSymbol);
            await _context.SaveChangesAsync();
            return IndividualCareSymbol.SymbolCode;
        }

        public async Task<int> UpdateIndividual(IndividualCareSymbol UpdateIndividualCareSymbol)
        {
            _context.individualCareSymbols.Update(UpdateIndividualCareSymbol);
            await _context.SaveChangesAsync();
            return UpdateIndividualCareSymbol.SymbolCode;
        }

        public async Task<bool> IsExistingNameIndividual(string name)
        {
            return await _context.individualCareSymbols.Where(x => x.IsActive).AnyAsync(s => s.name.Trim() == name);

        }
    }
}
