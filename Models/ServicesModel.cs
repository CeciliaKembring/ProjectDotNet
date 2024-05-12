using System.ComponentModel.DataAnnotations;
namespace ProjectNet.Models;

public class ServicesModel {
    public int Id { get; set; }

    [Required]
    public string? Service { get; set; }

    public string? Description { get; set; }

    [Required]
    public int? Price { get; set; }
    
    public ICollection<BookingModel>? Bookings { get; set; }
}