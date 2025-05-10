using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Exercise3.Pages.Data;
using Exercise3.Pages.Models;

namespace Exercise3.Pages.Items
{
    public class DeleteModel : PageModel
    {
        private readonly Exercise3.Pages.Data.AppDbContext _context;

        public DeleteModel(Exercise3.Pages.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Item Item { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item.FirstOrDefaultAsync(m => m.ItemID == id);

            if (item == null)
            {
                return NotFound();
            }
            else
            {
                Item = item;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Item
                .Include(i => i.OrderDetails) 
                .FirstOrDefaultAsync(m => m.ItemID == id);

            if (item == null)
            {
                return NotFound();
            }

            if (item.OrderDetails.Any())
            {
                ModelState.AddModelError(string.Empty, "Cannot delete this item because it is used in order details.");
                Item = item; 
                return Page();
            }

            _context.Item.Remove(item);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
