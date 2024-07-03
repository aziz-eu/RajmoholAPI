using System.ComponentModel.DataAnnotations;

namespace Rajmohol.Models.DTOs.Villa
{
    public class VillaCreateDTO
    {
        [Required]
        public string Name { get; set; }
        public string Details { get; set; }
        [Required]
        public double Rate { get; set; }
        [Required]
        public int Sqft { get; set; }
        public int Occupancy { get; set; }
        public string ImageUrl { get; set; }
        public string Amenity { get; set; }
    }
}
