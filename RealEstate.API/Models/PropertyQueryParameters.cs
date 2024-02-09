namespace RealEstate.API.Models
{
    public class PropertyQueryParameters: QueryParameters
    {
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? Category { get; set; }
        public string State { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
    }
}