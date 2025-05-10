using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Exercise3.Pages.Data;
using Exercise3.Pages.Models;
using Microsoft.EntityFrameworkCore;

namespace Exercise3.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Order Order { get; set; }

        [BindProperty]
        public List<OrderDetail> OrderDetails { get; set; } = new();

        public SelectList AgentList { get; set; }
        public SelectList ItemList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadSelectListsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || OrderDetails == null || OrderDetails.Count == 0)
            {
                await LoadSelectListsAsync();
                return Page(); // Display the form again if validation fails
            }

            // Save the Order
            Order.OrderDate = DateTime.Now;
            _context.Order.Add(Order);
            await _context.SaveChangesAsync();

            // Add each order detail
            foreach (var detail in OrderDetails)
            {
                detail.OrderID = Order.OrderID;
                _context.OrderDetail.Add(detail);
            }

            await _context.SaveChangesAsync();

            // Redirect to the list of orders
            return RedirectToPage("/Orders/Index");
        }

        private async Task LoadSelectListsAsync()
        {
            AgentList = new SelectList(await _context.Agent.ToListAsync(), "AgentID", "AgentName");
            ItemList = new SelectList(await _context.Item.ToListAsync(), "ItemID", "ItemName");
        }
    }
}
