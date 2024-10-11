using IdentityServer4.Models;

namespace Library.Identity;

public class IdentityServerConfig
{
    public static IEnumerable<Client> GetClients()
    {
        return new List<Client>
        {
            new Client
            {
                ClientId = "client_id",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets =
                {
                    new Secret("client_secret".Sha256())
                },
                AllowedScopes = { "api_scope" },
                AllowOfflineAccess = true, // Позволяет использовать Refresh токены
                RequireConsent = false,
                AccessTokenLifetime = 3600, // Время жизни Access токена в секундах
                AbsoluteRefreshTokenLifetime = 2592000, // Время жизни Refresh токена в секундах
                RefreshTokenUsage = TokenUsage.ReUse // Один Refresh токен на grant
            }
        };
    }

    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new List<ApiScope>
        {
            new ApiScope("api_scope", "My API")
        };
    }
}