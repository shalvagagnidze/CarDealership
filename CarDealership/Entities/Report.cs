using System.ComponentModel.DataAnnotations;

namespace CarDealership.Entities
{
    public class Report
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Title { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public CarModel? CarModel { get; set; }    
        public User? User { get; set; }
    }
}
