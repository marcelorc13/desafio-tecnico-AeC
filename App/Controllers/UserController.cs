using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using App.Data;
using App.Models;
using App.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace App.Controllers;

[Route("auth")]
public class AuthController : Controller
{
    private readonly AppDbContext _db;

    public AuthController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet("signup")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Register(UserCreateDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        bool usernameExists = await _db.Users.AnyAsync(u => u.Username == dto.Username);
        if (usernameExists)
        {
            ModelState.AddModelError("Username", "Username already taken");
            return View(dto);
        }

        var user = new UserModel
        {
            Name = dto.Name,
            Username = dto.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        return RedirectToAction("Login");
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);

        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
        {
            ModelState.AddModelError(string.Empty, "Invalid username or password");
            return View(dto);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        return RedirectToAction("Index", "Home");
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
}
