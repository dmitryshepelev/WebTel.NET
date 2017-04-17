using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;

namespace WebTelNET.Auth
{
    public class IdentityServerConfig
    {
        // scopes define the resources in your system
        public static IEnumerable<Scope> GetScopes()
        {
            return new List<Scope>
            {
                StandardScopes.OpenId,
                StandardScopes.Profile,
                StandardScopes.OfflineAccess,

                new Scope
                {
                    Name = "api",
                    DisplayName = "API",
                    Description = "api",
                    Type = ScopeType.Resource,
                    IncludeAllClaimsForUser = true,
                    Claims = new List<ScopeClaim>
                    {
                        new ScopeClaim(ClaimTypes.Name),
                        new ScopeClaim(ClaimTypes.Role)
                    }
                }
            };
        }

        // client want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients(bool isProduction)
        {
            var pbxHost = isProduction ? "http://pbx.leadder.ru" : "http://localhost:5001";
            var officeHost = isProduction ? "http://office.leadder.ru" : "http://localhost:5002";

            return new List<Client>
            {
                new Client
                {
                    ClientId = "pbx",
                    ClientName = "PBX",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    RequireConsent = false,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris = { String.Format("{0}/signin-oidc", pbxHost), pbxHost },
                    PostLogoutRedirectUris = { pbxHost },

                    AllowedScopes =
                    {
                        StandardScopes.OpenId.Name,
                        StandardScopes.Profile.Name,
                        StandardScopes.OfflineAccess.Name,
                        "api"
                    }
                },

                new Client
                {
                    ClientId = "office",
                    ClientName = "Office",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,

                    RequireConsent = false,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    RedirectUris = { String.Format("{0}/signin-oidc", officeHost), officeHost },
                    PostLogoutRedirectUris = { officeHost },
                    AllowedCorsOrigins = { officeHost, pbxHost },

                    AllowedScopes =
                    {
                        StandardScopes.OpenId.Name,
                        StandardScopes.Profile.Name,
                        StandardScopes.OfflineAccess.Name,
                        "api"
                    }
                }
            };
        }
    }
}
