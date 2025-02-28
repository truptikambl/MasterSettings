using System.Threading.Tasks;
using WebAPIProject.Core.Models;

namespace WebAPIProject.Contract.Repositories
{
    public interface IEmailRepository
    {
        Task<Users> GetUserById(int userId);
    }
}
