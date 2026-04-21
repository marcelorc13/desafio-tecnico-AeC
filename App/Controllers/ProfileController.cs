using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Data;

namespace App.Controllers;

[Authorize]
[Route("profile")]
public class ProfileController : Controller
{
    private readonly AppDbContext _db;

    public ProfileController(AppDbContext db)
    {
        _db = db;
    }

    private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
        if (user == null) return NotFound();

        return View(new ProfileResponseDto { Name = user.Name, Username = user.Username });
    }

    [HttpGet("edit")]
    public async Task<IActionResult> Edit()
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
        if (user == null) return NotFound();

        return View(new ProfileUpdateDTO { Name = user.Name, Username = user.Username });
    }

    [HttpPost("edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(ProfileUpdateDTO dto)
    {
        if (!ModelState.IsValid) return View("Edit", dto);

        var userId = GetUserId();
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId);
        if (user == null) return NotFound();

        var usernameTaken = await _db.Users.AnyAsync(u => u.Username == dto.Username && u.Id != userId);
        if (usernameTaken)
        {
            ModelState.AddModelError(nameof(dto.Username), "Nome de usuário já está em uso");
            return View("Edit", dto);
        }

        user.Name = dto.Name;
        user.Username = dto.Username;
        await _db.SaveChangesAsync();

        TempData["Success"] = "Perfil atualizado com sucesso";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("change-password")]
    public IActionResult ChangePassword() => View();

    [HttpPost("change-password")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdatePassword(ChangePasswordDTO dto)
    {
        if (!ModelState.IsValid) return View("ChangePassword", dto);

        var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
        if (user == null) return NotFound();

        if (!BCrypt.Net.BCrypt.Verify(dto.CurrentPassword, user.Password))
        {
            ModelState.AddModelError(nameof(dto.CurrentPassword), "Senha atual incorreta");
            return View("ChangePassword", dto);
        }

        user.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
        await _db.SaveChangesAsync();

        TempData["Success"] = "Senha alterada com sucesso";
        return RedirectToAction(nameof(Index));
    }
}
