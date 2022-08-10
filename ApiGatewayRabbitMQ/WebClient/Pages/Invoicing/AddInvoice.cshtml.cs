using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebClient.Pages.Invoicing
{
    public class AddInvoiceModel : PageModel
    {
        [BindProperty]
        public InvoiceModel invoice { get; set; }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var json = JsonSerializer.Serialize(invoice);
            var content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = await client.PostAsync("https://localhost:44385/api/Invoicing/Invoicing", content);
            var response = result.Content.ReadAsStringAsync();
            return Redirect("../Index");
        }
    }
}
