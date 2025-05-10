using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Exercise3.Pages
{
    public class MainFormModel : PageModel
    {
        public IActionResult OnGet()
        {
            // Check if user is logged in
            var userID = HttpContext.Session.GetString("UserID");

            if (string.IsNullOrEmpty(userID))
            {
                // User is not logged in, redirect to Login form
                return RedirectToPage("/LoginForm");
            }

            // Logic for loading MainForm
            return Page();
        }
    }
}
