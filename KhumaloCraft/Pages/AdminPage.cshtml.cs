using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using KhumaloCraft.Models;
using KhumaloCraft.Data;
using Microsoft.Extensions.Logging;

namespace KhumaloCraft.Pages
{
    public class AdminPageModel : PageModel
    {
        private readonly KhumaloCraftContext _context;
        private readonly ILogger<AdminPageModel> _logger;

        public AdminPageModel(KhumaloCraftContext context, ILogger<AdminPageModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IList<Transactions> Orders { get; set; }
        public IList<User> users { get; set; }
        public IList<Product> Products { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Orders = await _context.Transactions.ToListAsync();
            users = await _context.User.ToListAsync();
            Products = await _context.Product.ToListAsync();
            return Page();
        }

        public IActionResult OnPostProcessOrder()
        {
            var id = Request.Form["id"];

            _logger.LogInformation("ID : " + id);

            var orderId = int.Parse(id);

            var order = _context.Transactions.Find(orderId);

            if (order != null)
            {
                order.Processed = true;
                _context.SaveChanges();
            }
            else
            {
                _logger.LogWarning($"Order with ID {orderId} not found.");
            }

            return RedirectToPage("/AdminPage");
        }

        public IActionResult OnPostAdminStatus()
        {
            var userid = Request.Form["userid"];

            var tempid = int.Parse(userid);

            var admin = _context.User.Find(tempid);

            if (admin != null)
            {
                admin.Admin = true;
                _context.SaveChanges();
            }
            else
            {
                _logger.LogWarning($"User with ID {tempid} not found.");
            }


            return RedirectToPage("/AdminPage");
        }

        public IActionResult OnPostDeleteProduct()
        {

            var tempid = Request.Form["productid"];
            var productid = int.Parse(tempid);
            var product = _context.Product.Find(productid);

            if (product != null)
            {
                _context.Product.Remove(product);
                _context.SaveChanges();
            }

            return RedirectToPage("/AdminPage");
        }

        public IActionResult OnPostAddItem()
        {

            var name = Request.Form["name"];
            var description = Request.Form["description"];
            var quantity = int.Parse(Request.Form["quantity"]);
            var price = Math.Round(double.Parse(Request.Form["price"]), 2);
            var category = Request.Form["category"]; 

            var newProduct = new Product
            {
                Name = name,
                Description = description,
                Quantity = quantity,
                Price = price,
                Category = category
            };

            _context.Product.Add(newProduct);
            _context.SaveChanges();
            
            return RedirectToPage("/AdminPage");
        }

    }
}

