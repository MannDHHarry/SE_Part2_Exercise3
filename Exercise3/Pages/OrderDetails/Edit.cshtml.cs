using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Exercise3.Pages.Data;
using Exercise3.Pages.Models;

namespace Exercise3.Pages.OrderDetails
{
    public class EditModel : PageModel
    {
        private readonly Exercise3.Pages.Data.AppDbContext _context;

        public EditModel(Exercise3.Pages.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OrderDetail OrderDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderdetail =  await _context.OrderDetail.FirstOrDefaultAsync(m => m.ID == id);
            if (orderdetail == null)
            {
                return NotFound();
            }
            OrderDetail = orderdetail;
           ViewData["ItemID"] = new SelectList(_context.Item, "ItemID", "ItemID");
           ViewData["OrderID"] = new SelectList(_context.Order, "OrderID", "OrderID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(OrderDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(OrderDetail.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool OrderDetailExists(int id)
        {
            return _context.OrderDetail.Any(e => e.ID == id);
        }
    }
}
