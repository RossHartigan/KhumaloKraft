using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KhumaloCraft.Models;
using KhumaloCraft.Data;
using Microsoft.Extensions.Configuration;

namespace KhumaloCraft.Pages
{
    public class MyWorkPageModel : PageModel
    {
        private readonly KhumaloCraftContext _context;
        private readonly ILogger<MyWorkPageModel> _logger;

        public MyWorkPageModel(KhumaloCraftContext context, ILogger<MyWorkPageModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IList<Product> ProductList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ProductList = await _context.Product.ToListAsync();
            return Page();
        }


        public IActionResult OnPostAddToCart()
        {
            var product = int.Parse(Request.Form["productID"]);
            var quantity = int.Parse(Request.Form["quantity"]);
            var price = float.Parse(Request.Form["price"]);

            var salePrice = Math.Round(price * quantity, 2);

            int? userId = HttpContext.Session.GetInt32("UserID");
            
            int defaultUserId = userId ?? 1;

            var order = new Transactions
            {
                UserID = defaultUserId,
                ProductID = product,
                Quantity = quantity,
                SalePrice = salePrice,
                Processed = false
            };

            var remove = _context.Product.ToList();
            
            foreach (var x in remove)
            {
                if (x.ProductID == product)
                {
                    x.Quantity = x.Quantity - quantity;
                    _context.SaveChanges();
                }
            }

            _logger.LogInformation("Quantity : " + quantity.ToString());
            _logger.LogInformation("Price : " + price.ToString());

            _context.Transactions.Add(order);
            _context.SaveChanges();

            return RedirectToPage("/MyWorkPage");
        }

    }
}

