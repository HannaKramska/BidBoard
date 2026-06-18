using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace BidBoard.Models;

public class ApplicationUser : IdentityUser
{
    // Роль хранится строкой — проще объяснять на интервью,
    // чем отдельная таблица ролей для такого проекта
    [MaxLength(20)]
    public string Role { get; set; } = UserRoles.Bidder;

    // Навигационные свойства
    public ICollection<Lot> Lots { get; set; } = new List<Lot>();
    public ICollection<Bid> Bids { get; set; } = new List<Bid>();
}

// Константы вместо enum — не нужна миграция при добавлении роли
public static class UserRoles
{
    public const string Admin = "Admin";
    public const string Seller = "Seller";
    public const string Bidder = "Bidder";
}