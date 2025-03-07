using Microsoft.EntityFrameworkCore;
using WebAPIProject.Contract.Repositories;
using WebAPIProject.Core.Models;
using WebAPIProject.Infrastructure.Data;

namespace WebAPIProject.Infrastructure.Repositories
{
    public class DyeingMethodRepository : IDyeingMethods
    {

          private readonly ApplicationDbContext _context;

            public DyeingMethodRepository(ApplicationDbContext context)
            {
                _context = context;

            }


        public async Task<int> Add(DyeingMethod dyeingMathod)
        {
            await _context.DyeingMethod.AddAsync(dyeingMathod);
            await _context.SaveChangesAsync();
            return dyeingMathod.Id;
        }

        public Task<int> Count(IQueryable<DyeingMethod> query)
        {
            return query.CountAsync();
        }

        public IQueryable<DyeingMethod> GetAllActive()
        {
            return _context.DyeingMethod.Where(x => x.IsActive);
        }

        public async Task<DyeingMethod> GetMaxDyeTypeAsync()
        {
            return await _context.DyeingMethod
                                 .Where(d => !string.IsNullOrEmpty(d.DyeMethodCode))
                                 .OrderByDescending(d => d.DyeMethodCode)
                                 .FirstOrDefaultAsync();
        }


        public async Task<DyeingMethod> GetById(int id)
        {
            return await _context.DyeingMethod.FirstOrDefaultAsync(d => d.Id == id);
        }


        public async Task<bool> IsExistingName(string DyeingMathod)
        {
            return await _context.DyeingMethod.Where(x => x.IsActive).AnyAsync(s => s.DyeMethodCode.Trim() == DyeingMathod);
        }



    
        public async Task<int> UpdateDyeingMathod(DyeingMethod dyeingMathod)
        {
            _context.DyeingMethod.Update(dyeingMathod);
            await _context.SaveChangesAsync();
            return dyeingMathod.Id;
        }

     

        public async  Task<DyeingMethod> GetMaxDyeType()
        {
            return await _context.DyeingMethod
                                 .Where(d => !string.IsNullOrEmpty(d.DyeMethodCode))
                                 .OrderByDescending(d => d.DyeMethodCode)
                                 .FirstOrDefaultAsync();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
    
}
