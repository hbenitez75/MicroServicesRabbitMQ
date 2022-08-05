using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using IdentityModel;

var builder = WebApplication.CreateBuilder(args);

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
.AddCookie("Cookies")
.AddOpenIdConnect("oidc", options =>
 {
     options.Authority = "https://localhost:5001";
     options.ClientId = "web";
     options.ClientSecret = "secret";
     options.ResponseType = "code";
     options.SaveTokens = true;

     options.Scope.Clear();
     options.Scope.Add("openid");
     options.Scope.Add("profile");
     options.Scope.Add("email");     
     options.Scope.Add("api1");
     options.Scope.Add("offline_access");
     options.Scope.Add("leerescribir");
     options.ClaimActions.MapUniqueJsonKey("arquitecto", "arquitecto");     
     options.ClaimActions.MapUniqueJsonKey("website", "website");
     options.ClaimActions.MapUniqueJsonKey("preferred_username", "preferred_username");
     options.ClaimActions.MapUniqueJsonKey(JwtClaimTypes.Role, JwtClaimTypes.Role);
     options.GetClaimsFromUserInfoEndpoint = true;
     
     options.TokenValidationParameters = new TokenValidationParameters
     {
        NameClaimType = JwtClaimTypes.Name,
        RoleClaimType = JwtClaimTypes.Role
     };

     
 });

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages().RequireAuthorization();

app.Run();
