using Microsoft.EntityFrameworkCore;
using WebAPIProject.Contract.Repositories;
using WebAPIProject.Core.DTOs;
using WebAPIProject.Core.Models;
using WebAPIProject.Infrastructure.Data;

namespace WebAPIProject.Infrastructure.Repositories
{
    public class DyeStuffRepository : IDyeStuffRepository
    {
        private readonly ApplicationDbContext _context;

        public DyeStuffRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public string DyeStuffName { get; private set; }

        public object response => throw new NotImplementedException();

        public  async Task<int> AddDyeStuff(DyeStuff DyeStuf)
        {
            await _context.DyeStuff.AddAsync(DyeStuf);
            await _context.SaveChangesAsync();
            return DyeStuf.Id;
        }

        public Task<int> CountDyeStuff(IQueryable<DyeStuff> query)
        {
            return query.CountAsync();
        }

        public IQueryable<DyeStuff> GetAllActiveDyeStuff()
        {
            return _context.DyeStuff;
        }

        public  async Task<DyeStuff> GetByIdDyeStuff(int Id)
        {
            return await _context.DyeStuff.FirstOrDefaultAsync(c => c.Id == Id);
        }

        public  async Task<bool> IsExistingNameDyeStuff(string DyeStuff)
        {
            return await _context.DyeStuff.Where(x => x.IsActive).AnyAsync(s => s.DyeStuffName.Trim() == DyeStuffName);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async  Task<int> UpdateDyeStuff(DyeStuff UpdateDyeStuff)
        {
            _context.DyeStuff.Update(UpdateDyeStuff);
            await _context.SaveChangesAsync();
            return UpdateDyeStuff.Id;
        }

       

    }
}
