using Exercise3.Pages.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
namespace Exercise3.Pages
{
    public class ReportFormModel : PageModel
    {
        private readonly AppDbContext _context;

        public ReportFormModel(AppDbContext context)
        {
            _context = context;
        }

        public List<ItemReport> TopItems { get; set; }
        public List<CustomerPurchase> CustomerPurchases { get; set; }

        public async Task OnGetAsync()
        {
            // Top 5 best-selling items
            TopItems = await _context.OrderDetail
                .Include(od => od.Item)
                .GroupBy(od => od.Item.ItemName)
                .Select(g => new ItemReport
                {
                    ItemName = g.Key,
                    TotalQuantity = g.Sum(x => x.Quantity)
                })
                .OrderByDescending(x => x.TotalQuantity)
                .Take(5)
                .ToListAsync();

            // Customer purchase history by Agent (you may adjust if Customer is added)
            CustomerPurchases = await _context.Order
                .Include(o => o.Agent)
                .Include(o => o.OrderDetails).ThenInclude(od => od.Item)
                .Select(o => new CustomerPurchase
                {
                    OrderID = o.OrderID,
                    AgentName = o.Agent.AgentName,
                    Items = o.OrderDetails.Select(d => d.Item.ItemName).ToList(),
                    Total = o.OrderDetails.Sum(d => d.Quantity * d.UnitAmount)
                })
                .ToListAsync();
        }

        public class ItemReport
        {
            public string ItemName { get; set; }
            public int TotalQuantity { get; set; }
        }

        public class CustomerPurchase
        {
            public int OrderID { get; set; }
            public string AgentName { get; set; }
            public List<string> Items { get; set; }
            public decimal Total { get; set; }
        }
    }
}
