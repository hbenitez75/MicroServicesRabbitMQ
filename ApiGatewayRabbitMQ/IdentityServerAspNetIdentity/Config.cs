using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityServerAspNetIdentity;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            
            
            
        };
    public static IEnumerable<ApiResource> ApiIdentityResources =>
        new List<ApiResource>
        {                        
            new ApiResource
            {
                Name ="Transaccion",
                DisplayName ="Transaccion",
                Scopes ={"leerescribir"},
                UserClaims = new List<string>
                {
                    "arquitecto"
                }
            }

        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("api1"),
            new ApiScope("My API"),            
            new ApiScope{ Name ="leerescribir",UserClaims = new List<string> { "arquitecto" } }
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            // m2m client credentials flow client
            new Client
            {
                ClientId = "client",                

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("secret".Sha256()) },
                
                AllowedScopes = { "api1" }
            },

            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "web",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "https://localhost:5002/signin-oidc" },
                //FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes = new List<string> { IdentityServerConstants.StandardScopes.OpenId, 
                                                   IdentityServerConstants.StandardScopes.Profile,
                                                   IdentityServerConstants.StandardScopes.Email,                                                                            
                                                   "leerescribir"
                },
                 AlwaysIncludeUserClaimsInIdToken = true,
                 

            },
        };
}
