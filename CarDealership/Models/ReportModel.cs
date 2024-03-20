using System.ComponentModel.DataAnnotations;

namespace CarDealership.Models
{
    public class ReportModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Title { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string? Description { get; set; }

    }
}
