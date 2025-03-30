using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SCANX2.Models;  // Add this line to fix "User not found" error

public class AccountController : Controller
{
    private readonly ScanXDbContext _context;

    public AccountController(ScanXDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Login(string Email, string Password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == Email);

        if (user == null || !VerifyPassword(user.Password, Password))
        {
            ModelState.AddModelError("", "Invalid email or password");
            return View();
        }

        return RedirectToAction("Index", "Home");
    }

    private bool VerifyPassword(string storedPassword, string enteredPassword)
    {
        using (var sha256 = System.Security.Cryptography.SHA256.Create())
        {
            byte[] enteredBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(enteredPassword));
            string enteredHash = System.Convert.ToBase64String(enteredBytes);
            return storedPassword == enteredHash;
        }
    }
    //[HttpPost]
    //public IActionResult Register(string name, string email, string password)
    //{
    //    if (_context.Users.Any(u => u.Email == email))
    //    {
    //        return BadRequest("Email already exists!");
    //    }

    //    var hashedPassword = HashPassword(password);

    //    var newUser = new User { Name = name, Email = email, Password = hashedPassword };
    //    _context.Users.Add(newUser);
    //    _context.SaveChanges();

    //    return Ok("User registered successfully!");
    //}
    [HttpPost]
    public IActionResult Register(string name, string email, string password)
    {
        if (_context.Users.Any(u => u.Email == email))
        {
            return BadRequest("Email already exists!");
        }

        var hashedPassword = HashPassword(password);

        var newUser = new User { Name = name, Email = email, Password = hashedPassword };
        _context.Users.Add(newUser);
        _context.SaveChanges();

        // Redirect to Login page after successful registration
        return RedirectToAction("Login", "Account");
    }


    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }

    [HttpGet]
    public IActionResult SignUp()
    {
        return View();
    }
}
