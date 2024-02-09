using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RealEstate.API.Models
{
    public class Property
    {
        public int Id { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [JsonIgnore]
        public virtual Category? Category { get; set; }
        [Required]
        public string City { get; set; } = string.Empty;
        [Required]
        public double AcresLot { get; set; }
        [Required]
        public string Address { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        public int Bed { get; set; }
        public int Bath { get; set; }
        [Required]
        public string PostalCode { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Sqft { get; set; }
        public string Status { get; set; } = string.Empty;
        [Required]
        public string State { get; set; } = string.Empty;
        public int YearBuilt { get; set; }

    }
}