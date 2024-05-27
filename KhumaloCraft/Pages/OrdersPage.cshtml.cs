using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KhumaloCraft.Data;
using KhumaloCraft.Models;

namespace KhumaloCraft.Pages
{
    public class OrdersPageModel : PageModel
    {
        private readonly KhumaloCraftContext _context;

        public OrdersPageModel(KhumaloCraftContext context)
        {
            _context = context;
        }

        public IList<OrderViewModel> Orders { get; set; }

        public async Task OnGetAsync()
        {
            // Get the user ID from session
            int? userId = HttpContext.Session.GetInt32("UserID");

            Orders = await _context.Transactions
                .Where(t => t.UserID == userId)
                .Join(
                    _context.Product,
                    transactions => transactions.ProductID,
                    product => product.ProductID,
                    (transactions, product) => new OrderViewModel
                    {
                        Quantity = transactions.Quantity,
                        SalePrice = transactions.SalePrice,
                        ProductName = product.Name,
                        Category = product.Category,
                        Processed = transactions.Processed
                    }
                )
                .ToListAsync();
        }

        public class OrderViewModel
        {
            public int Quantity { get; set; }
            public double SalePrice { get; set; }
            public string ProductName { get; set; }
            public string Category { get; set; }
            public bool Processed { get; set; }
            public string OrderStatus => Processed ? "Processed" : "Still processing";
        }
    }

}


