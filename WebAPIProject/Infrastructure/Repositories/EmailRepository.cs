using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPIProject.Core.Models;
using WebAPIProject.Contract.Repositories;
using WebAPIProject.Infrastructure.Data;

public class EmailRepository : IEmailRepository
{
    private readonly ApplicationDbContext _context;

    public EmailRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Users> GetUserById(int userId)
    {
        return await _context.Users
            .Where(u => u.Id == userId)
            .FirstOrDefaultAsync();
    }
}
