using System.ComponentModel.DataAnnotations;
namespace ProjectNet.Models;

public class BookingModel {
    public int Id { get; set; }

    [Required(ErrorMessage = "Ange datum för ditt besök")]
    [Display(Name = "Datum")]
    public string? Date { get; set; }

    [Required(ErrorMessage = "Vänligen ange en giltig tid mellan 10:00 och 16:00.")]
    [DataType(DataType.Time)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
    public DateTime? Time { get; set; }


    [Required(ErrorMessage = "Välj tjänst")]
    public int ServiceId { get; set; }

    [Display(Name = "Tjänst")]
    public ServicesModel? Service { get; set; }

    [Required]
    public string? BookedBy {get; set;}

    [Required(ErrorMessage = "Ange telefonnnummer")]
    [Display(Name = "Telefonnummer")]
    public int? Telephonenumber {get; set;}
}