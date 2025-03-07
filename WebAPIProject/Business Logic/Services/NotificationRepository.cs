using Microsoft.EntityFrameworkCore;
using WebAPIProject.Contract.Repositories;
using WebAPIProject.Core.Models;
using WebAPIProject.Infrastructure.Data;

namespace WebAPIProject.Business_Logic.Services
{
    public class NotificationRepository : INotificationRepository
    {

        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Notification notification)
        {
            await _context.Notification.AddAsync(notification);
            await SaveAsync();
        }

        public async Task<IEnumerable<Notification>> GetAllAsync()
        {
            return await _context.Notification.ToListAsync();
        }

        public async Task<Notification> GetByIdAsync(int id)
        {
            return await _context.Notification.FindAsync(id);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

