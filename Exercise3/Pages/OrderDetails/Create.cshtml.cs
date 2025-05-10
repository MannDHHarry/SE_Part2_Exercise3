using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Exercise3.Pages.Data;
using Exercise3.Pages.Models;

namespace Exercise3.Pages.OrderDetails
{
    public class CreateModel : PageModel
    {
        private readonly Exercise3.Pages.Data.AppDbContext _context;

        public CreateModel(Exercise3.Pages.Data.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ItemID"] = new SelectList(_context.Item, "ItemID", "ItemID");
        ViewData["OrderID"] = new SelectList(_context.Order, "OrderID", "OrderID");
            return Page();
        }

        [BindProperty]
        public OrderDetail OrderDetail { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.OrderDetail.Add(OrderDetail);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
