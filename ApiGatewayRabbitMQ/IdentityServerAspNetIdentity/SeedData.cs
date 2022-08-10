using System.Security.Claims;
using IdentityModel;
using IdentityServerAspNetIdentity.Data;
using IdentityServerAspNetIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IdentityServerAspNetIdentity;

public class SeedData
{
    public static void EnsureSeedData(WebApplication app)
    {
        using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
           // context.Database.Migrate();

            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            
            var roleArkusAdim = roleMgr.FindByNameAsync("ArkusAdmin").Result;
            if (roleArkusAdim == null)
            {
                roleArkusAdim = new IdentityRole
                {
                    Id = "1",
                    Name = "ArkusAdmin",
                    NormalizedName = "ArkusAdmin"
                };
                var resultRole = roleMgr.CreateAsync(roleArkusAdim).Result;
                if (!resultRole.Succeeded)
                {
                    throw new Exception(resultRole.Errors.First().Description);
                }
                Log.Debug("Role ArkusAdmin Added");
            }
            else
            {
                Log.Debug("Role ArkusAdmin Already Exists");
            }
            
            var hector =  userMgr.FindByNameAsync("hector").Result;
            if (hector == null)
            {
                hector = new ApplicationUser
                {
                    UserName = "hector",
                    Email = "hbenitez@arkusnexus.com",
                    EmailConfirmed = true
                };
                var result = userMgr.CreateAsync(hector, "Arkus1$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                result = userMgr.AddClaimsAsync(hector, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Hector"),
                            new Claim(JwtClaimTypes.GivenName, "Don"),
                            new Claim(JwtClaimTypes.FamilyName, "Benitez"),
                            new Claim(JwtClaimTypes.WebSite, "http://hector.com"),
                            new Claim("arquitecto","si"),

                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                Log.Debug("User Hector created");

                var resultRole = userMgr.AddToRoleAsync(hector, "ArkusAdmin").Result;
                if (!resultRole.Succeeded)
                {
                    Log.Debug("Hector added to role Arkusadmin Failed " + result.Errors.First().Description);
                    Log.Debug(result.Errors.First().Description);
                    throw new Exception(resultRole.Errors.First().Description);
                }
                Log.Debug("Hector added to role Arkusadmin");


            }
            else
            {
                Log.Debug("hector already exists");
            }


            var alice = userMgr.FindByNameAsync("lolo").Result;
            if (alice == null)
            {
                alice = new ApplicationUser
                {
                    UserName = "lolo",
                    Email = "AliceSmith@email.com",
                    EmailConfirmed = true,
                };
                var result = userMgr.CreateAsync(alice, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(alice, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Arturo Renteria"),
                            new Claim(JwtClaimTypes.GivenName, "lolo"),
                            new Claim(JwtClaimTypes.FamilyName, "R"),
                            new Claim(JwtClaimTypes.WebSite, "http://lolo.com"),
                            new Claim("arquitecto","no"),
                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                Log.Debug("lolo created");
            }
            else
            {
                Log.Debug("lolo already exists");
            }

            var bob = userMgr.FindByNameAsync("bob").Result;
            if (bob == null)
            {
                bob = new ApplicationUser
                {
                    UserName = "bob",
                    Email = "BobSmith@email.com",
                    EmailConfirmed = true
                };
                var result = userMgr.CreateAsync(bob, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(bob, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Bob Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Bob"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                            new Claim("location", "somewhere")
                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                Log.Debug("bob created");
            }
            else
            {
                Log.Debug("bob already exists");
            }
        }
    }
}
