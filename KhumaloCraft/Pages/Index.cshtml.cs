using KhumaloCraft.Data;
using KhumaloCraft.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KhumaloCraft.Pages
{
    public class IndexModel : PageModel
    {
        private readonly KhumaloCraftContext _context;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, KhumaloCraftContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPostLogin()
        {
            var email = Request.Form["email"];
            var password = Request.Form["password"];

            List<User> users = _context.User.ToList();

            foreach(var x in users)
            {
                if(x.Email == email && x.Password == password)
                {
                    // Set session variable for user ID
                    HttpContext.Session.SetInt32("UserID", x.UserID);

                    if(x.Admin)
                    {
                        _logger.LogInformation("Admin Logged In");
                        return RedirectToPage("/AdminPage");
                    }

                    _logger.LogInformation("Logged In");
                    return RedirectToPage("/HomePage");
                }              
                else if (x.Email == email && x.Password != password)
                {
                    _logger.LogInformation("Incorrect Password!");
                }
                else
                {
                    _logger.LogInformation("Incorrect email & Password!");
                }
            }

            return Page();
        }

        public IActionResult OnPostRegister()
        {
            var email = Request.Form["email"];
            var password = Request.Form["password"];

            List<User> users = _context.User.ToList();

            foreach (var x in users)
            {
                if (x.Email == email && x.Password == password)
                {
                    // Set session variable for user ID
                    HttpContext.Session.SetInt32("UserID", x.UserID);

                    if (x.Admin)
                    {
                        _logger.LogInformation("Admin Logged In");
                        return RedirectToPage("/AdminPage");
                    }

                    _logger.LogInformation("Logged In");
                    return RedirectToPage("/HomePage");
                }
                else if (x.Email == email && x.Password != password)
                {
                    _logger.LogInformation("Incorrect Password!");
                }
                else
                {
                    _logger.LogInformation("Incorrect email & Password!");
                }
            }

            return Page();
        }
    }
}