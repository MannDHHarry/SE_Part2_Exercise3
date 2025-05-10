using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Exercise3;
using System.Linq;
using Exercise3.Pages.Data;
using Microsoft.AspNetCore.Http;

namespace Exercise3.Pages
{
    public class LoginFormModel : PageModel
    {
       private readonly AppDbContext _context;

        public LoginFormModel(AppDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == Username && u.Password == Password && u.Lock == false);


            if (user != null)
            {
                HttpContext.Session.SetString("UserID", user.UserID.ToString());
                HttpContext.Session.SetString("UserName", user.UserName);

                return RedirectToPage("/MainForm"); 
            }
            else
            {
                ErrorMessage = "Invalid username or password.";
                return Page();
            }
        }
    }
}
