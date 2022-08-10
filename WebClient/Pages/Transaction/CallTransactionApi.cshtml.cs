using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebClient.Pages.Transaction;

    public class CallApiModel : PageModel
    {
        public string Json = string.Empty;
        public async Task OnGet()
        {
           var accessToken = await HttpContext.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var content = await client.GetStringAsync("https://localhost:44383/api/Transaction");
            var parsed = JsonDocument.Parse(content);
            var formatted = JsonSerializer.Serialize(parsed,new JsonSerializerOptions { WriteIndented = true});
            Json = formatted;       
        }

    }
