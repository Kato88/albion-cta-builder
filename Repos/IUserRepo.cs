using System.Threading.Tasks;
using zergtool;
using Zorn.Models;

namespace Zorn.Repos
{
    public interface IUserRepo
    {
        Task<string> RegisterAsync(RegisterModel model);
        Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model);

        Task<ApplicationUser> GetUser(System.Security.Claims.ClaimsPrincipal user);
    }
}