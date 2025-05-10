using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Exercise3.Pages.Data;
using Exercise3.Pages.Models;

namespace Exercise3.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly Exercise3.Pages.Data.AppDbContext _context;

        public IndexModel(Exercise3.Pages.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Order = await _context.Order
                
                .Include(o => o.Agent).ToListAsync();
        }
    }
}
