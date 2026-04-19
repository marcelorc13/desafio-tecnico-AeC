using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using App.Data;
using App.Models;
using App.Models.DTOs;

namespace App.Controllers;

[Authorize]
[Route("addresses")]
public class AddressController : Controller
{
    private readonly AppDbContext _db;

    public AddressController(AppDbContext db)
    {
        _db = db;
    }

    private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var userId = GetUserId();

        var addresses = await _db.Addresses
            .Where(a => a.UserId == userId)
            .Select(a => new AddressResponseDto
            {
                Id = a.Id,
                Name = a.Name,
                CEP = a.CEP,
                PublicPlace = a.PublicPlace,
                Complement = a.Complement,
                District = a.District,
                City = a.City,
                FederalUnit = a.FederalUnit,
                Number = a.Number,
                UserId = a.UserId
            })
            .ToListAsync();

        return View(addresses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var userId = GetUserId();
        var address = await _db.Addresses
            .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);

        if (address == null) return NotFound();

        var dto = new AddressResponseDto
        {
            Id = address.Id,
            Name = address.Name,
            CEP = address.CEP,
            PublicPlace = address.PublicPlace,
            Complement = address.Complement,
            District = address.District,
            City = address.City,
            FederalUnit = address.FederalUnit,
            Number = address.Number,
            UserId = address.UserId
        };

        return View(dto);
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(AddressCreateDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        var address = new AddressModel
        {
            Name = dto.Name,
            CEP = dto.CEP,
            PublicPlace = dto.PublicPlace,
            Complement = dto.Complement,
            District = dto.District,
            City = dto.City,
            FederalUnit = dto.FederalUnit,
            Number = dto.Number,
            UserId = GetUserId()
        };

        _db.Addresses.Add(address);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var userId = GetUserId();
        var address = await _db.Addresses
            .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);

        if (address == null) return NotFound();

        var dto = new AddressCreateDto
        {
            Name = address.Name,
            CEP = address.CEP,
            PublicPlace = address.PublicPlace,
            Complement = address.Complement,
            District = address.District,
            City = address.City,
            FederalUnit = address.FederalUnit,
            Number = address.Number
        };

        return View(dto);
    }

    [HttpPost("edit/{id}")]
    public async Task<IActionResult> Update(int id, AddressCreateDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        var userId = GetUserId();
        var address = await _db.Addresses
            .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);

        if (address == null) return NotFound();

        address.Name = dto.Name;
        address.CEP = dto.CEP;
        address.PublicPlace = dto.PublicPlace;
        address.Complement = dto.Complement;
        address.District = dto.District;
        address.City = dto.City;
        address.FederalUnit = dto.FederalUnit;
        address.Number = dto.Number;

        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = GetUserId();
        var address = await _db.Addresses
            .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);

        if (address == null) return NotFound();

        return View(new AddressResponseDto
        {
            Id = address.Id,
            Name = address.Name,
            CEP = address.CEP,
            PublicPlace = address.PublicPlace,
            Complement = address.Complement,
            District = address.District,
            City = address.City,
            FederalUnit = address.FederalUnit,
            Number = address.Number,
            UserId = address.UserId
        });
    }

    [HttpPost("delete/{id}")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var userId = GetUserId();
        var address = await _db.Addresses
            .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);

        if (address == null) return NotFound();

        _db.Addresses.Remove(address);
        await _db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
