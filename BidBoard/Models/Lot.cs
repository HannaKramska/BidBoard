using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace BidBoard.Models;

public class Lot
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string Description { get; set; } = string.Empty;

    // Фото — храним путь к файлу, не сам файл в БД
    [MaxLength(500)]
    public string? ImagePath { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal StartingPrice { get; set; }

    // Денормализация: не делаем MAX(Bid.Amount) при каждом запросе.
    // Обновляется атомарно при каждой новой ставке.
    [Column(TypeName = "decimal(18,2)")]
    public decimal CurrentPrice { get; set; }

    // Минимальный шаг — бизнес-правило, которое хорошо обсуждается на интервью
    [Column(TypeName = "decimal(18,2)")]
    public decimal MinBidStep { get; set; } = 1.00m;

    // Строковый статус + константы — легко добавить новый статус без миграции enum
    [Required, MaxLength(20)]
    public string Status { get; set; } = LotStatus.Draft;

    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // FK и навигация к продавцу
    [Required]
    public string SellerId { get; set; } = string.Empty;
    public ApplicationUser Seller { get; set; } = null!;

    // FK и навигация к категории
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public ICollection<Bid> Bids { get; set; } = new List<Bid>();

    // Вычисляемое свойство — не хранится в БД
    public bool IsActive => Status == LotStatus.Active && DateTime.UtcNow < EndsAt;
}

public static class LotStatus
{
    public const string Draft = "Draft";     // черновик, не виден другим
    public const string Active = "Active";    // идут торги
    public const string Ended = "Ended";     // завершён (есть победитель или нет)
    public const string Cancelled = "Cancelled"; // отменён продавцом
}