using Microsoft.EntityFrameworkCore;
using WebAPIProject.Contract.Repositories;
using WebAPIProject.Core.Models;
using WebAPIProject.Infrastructure.Data;

namespace WebAPIProject.Infrastructure.Repositories
{
    public class DyeTypeRepository : IDyeTypeRepository
    {
       
        private readonly ApplicationDbContext _context;

        public DyeTypeRepository(ApplicationDbContext context)
        {
            _context = context;
          
        }

      


        public async Task<int> Add(DyeType dyeType)
        {
            await _context.DyeType.AddAsync(dyeType);
            await _context.SaveChangesAsync();
            return dyeType.Id;
        }

        public Task<int> Count(IQueryable<DyeType> query)
        {
            return query.CountAsync();
        }

        public IQueryable<DyeType> GetAllActive()
        {
            return _context.DyeType.Where(x => x.IsActive);
        }

        public async Task<DyeType> GetMaxDyeTypeAsync()
        {
            return await _context.DyeType
                                 .Where(d => !string.IsNullOrEmpty(d.DyeTypeCode))
                                 .OrderByDescending(d => d.DyeTypeCode)
                                 .FirstOrDefaultAsync();
        }


        public async Task<DyeType> GetById(int id)
        {
            return await _context.DyeType.FirstOrDefaultAsync(d => d.Id == id);
        }

       
        public async Task<bool> IsExistingName(string DyeTypeName)
        {
           return await _context.DyeType.Where(x =>  x.IsActive).AnyAsync(s => s.DyeTypeName.Trim() == DyeTypeName);
        }


        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task<int> UpdateDyeType(DyeType dyeType)
        {
            _context.DyeType.Update(dyeType);   
            await _context.SaveChangesAsync();
            return dyeType.Id;
        }

        
    }
}