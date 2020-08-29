using System.Threading.Tasks;
using Zorn.Models;

namespace Zorn.Repos
{
    public interface IUserRepo
    {
        Task<string> RegisterAsync(RegisterModel model);
        Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model);
    }
}