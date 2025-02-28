using Microsoft.EntityFrameworkCore;
using WebAPIProject.Contract.Repositories;
using WebAPIProject.Core.Models;
using WebAPIProject.Infrastructure.Data;

namespace WebAPIProject.Infrastructure.Repositories
{
    public class FabricRepository : IFabricRepository
    {
        private readonly ApplicationDbContext _context;

        public FabricRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IQueryable<Fabricc> GetAllActive()
        {
            return _context.Fabric.Where(c => c.IsActive);
        }


        public async Task<int> Count(IQueryable<Fabricc> query)
        {
            return query.Count();
        }

        public async Task<Fabricc> GetById(int id)
        {
            return await _context.Fabric.FirstOrDefaultAsync(c => c.FabricId == id);
        }

        public async Task<int> Add(Fabricc fabric)
        {
            await _context.Fabric.AddAsync(fabric);
            await _context.SaveChangesAsync();
            return fabric.FabricId;
        }

        
        public async Task<int> UpdateFabric(Fabricc fabric)
        {
            _context.Fabric.Update(fabric);
            await _context.SaveChangesAsync();
            return fabric.FabricId;
        }

        public async Task<bool> IsExistingName (String FabricType)
        {
            return await _context.Fabric.Where(x=>x.IsActive).AnyAsync(f => f.FabricType.Trim() == FabricType);
        }

        public IQueryable<Fabricc> GetAllActiveFabric()
        {
            return _context.Fabric.Where(c => c.IsActive);
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task<bool> IsExistingType(String ConstructionType)
        {
            return await _context.constructions.Where(x => x.IsActive).AnyAsync(f => f.ConstructionType.Trim() == ConstructionType);
        }


        public async Task<int> UpdateConstruction(Construction construction)
        {
            var existingConstruction = await _context.constructions.FindAsync(construction.ConstructionId);
            if (existingConstruction != null)
            {
                _context.Entry(existingConstruction).CurrentValues.SetValues(construction);
                return await _context.SaveChangesAsync(); 
            }

            return 0; 
        }



        public IQueryable<Construction> GetAllActiveConstruction()
        {
            return (IQueryable<Construction>)_context.constructions.Where(d => d.IsActive);
        }

        public async Task<int> Count(IQueryable<Construction> query)
        {
            return query.Count();
        }

        public async Task<Construction> GetByIdConstruction(int ConStructionId)
        {
            return await _context.constructions.FirstOrDefaultAsync(c=>c.ConstructionId==ConStructionId);
        }

        public async Task<int> Add(Construction construction)
        {
            
            await _context.constructions.AddAsync(construction);
            await _context.SaveChangesAsync();
            return construction.ConstructionId;
        }

  
        public IQueryable<Construction> GetAllConstruction()
        {
            return _context.constructions.Where(d => d.IsActive);
        }

     
    }

}

