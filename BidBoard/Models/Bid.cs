using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BidBoard.Models;

public class Bid
{
    public int Id { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    // UTC везде — иначе проблемы с таймзонами при сравнении с EndsAt
    public DateTime PlacedAt { get; set; } = DateTime.UtcNow;

    public int LotId { get; set; }
    public Lot Lot { get; set; } = null!;

    [Required]
    public string BidderId { get; set; } = string.Empty;
    public ApplicationUser Bidder { get; set; } = null!;
}
