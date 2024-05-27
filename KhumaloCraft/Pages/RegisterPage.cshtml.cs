using KhumaloCraft.Data;
using KhumaloCraft.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KhumaloCraft.Pages
{
    public class RegisterPageModel : PageModel
    {
        private readonly KhumaloCraftContext _context;
        private readonly ILogger<RegisterPageModel> _logger;

        public RegisterPageModel(KhumaloCraftContext context, ILogger<RegisterPageModel> logger)
        {
            _context = context;
            _logger = logger;
        }   

        public void OnGet()
        {

        }

        public IActionResult OnPostRegister()
        {
            var email = Request.Form["email"];
            var name = Request.Form["firstName"];
            var surname = Request.Form["lastName"];
            var password = Request.Form["password"];
            var passwordConfirm = Request.Form["confirmPassword"];

            string finalPassword = null;

            if (password != passwordConfirm)
            {
                return RedirectToPage("/RegisterPage");
            }
            else
            {
                finalPassword = password;
            }

            var user = new User
            {
                Email = email,
                Name = name,
                Surname = surname,
                Password = finalPassword,
                Admin = false

            };

            _context.User.Add(user);
            _context.SaveChanges();

            return RedirectToPage("/Index");
        }
    }
}
