using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebClient.Pages
{
    public class SignoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            var claims = User.Claims;
            var claimsp = HttpContext.User.Claims;
            return SignOut("Cookies", "oidc");
        }
    }
}
