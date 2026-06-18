using System.ComponentModel.DataAnnotations;

namespace BidBoard.Models;

public class Category
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    // URL-friendly идентификатор: "vintage-clocks", "art-prints"
    // Удобно для фильтрации и читаемых URL
    [Required, MaxLength(100)]
    public string Slug { get; set; } = string.Empty;

    public ICollection<Lot> Lots { get; set; } = new List<Lot>();
}
