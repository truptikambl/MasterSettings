using System.Security.Claims;
using WebAPIProject.Contract.Repositories;

namespace WebAPIProject.Infrastructure.Repositories
{
        public class APIUserContext : IAPIUserContext
        {
            private readonly IHttpContextAccessor _httpContextAccessor;

            public APIUserContext(IHttpContextAccessor httpContextAccessor)
            {
                _httpContextAccessor = httpContextAccessor;
            }

            public int UserId
            {
                get
                {
                    var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
                    return int.TryParse(userIdClaim?.Value, out var userId) ? userId : 0;
                }
            }
        }

    

}
