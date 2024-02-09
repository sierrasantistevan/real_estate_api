

namespace RealEstate.API.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public virtual List<Property> Properties { get; set; }
    }
}