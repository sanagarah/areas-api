using Microsoft.AspNetCore.Identity;

namespace areas_api.Repositores
{
	public interface ITokenRepository
	{
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}

