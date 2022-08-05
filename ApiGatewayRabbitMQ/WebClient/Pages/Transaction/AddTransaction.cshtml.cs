using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebClient.Pages.Transaction
{
    public class AddTransactionModel : PageModel
    {
        [BindProperty]
        public TransactionModel Transaction { get; set; }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var client = new HttpClient();
            var json = JsonSerializer.Serialize(Transaction);
            var content = new StringContent(json);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = await client.PostAsync("https://localhost:44383/api/Transaction", content);
            var response = await result.Content.ReadAsStringAsync();
            return RedirectToPage("../Index");
        }
    }
}
