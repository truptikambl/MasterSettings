using System.Threading.Tasks;
using WebAPIProject.Core.DTOs;

namespace WebAPIProject.Contract.Service
{
    public interface IEmailService
    {
        Task<SendEmailResponse> SendEmail(int userId);
    }
}
