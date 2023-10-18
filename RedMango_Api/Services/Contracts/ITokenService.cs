using DataAccess.Data.Identity;

namespace RedMango_Api.Services.Contracts
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);

    }
}
