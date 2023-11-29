using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yelpcamp.Models
{
    public class CampgroundImage
    {
        public int Id { get; set; }
        [Required]
        public string? URL { get; set; }
        [Required]
        public string? AppPublicId { get; set; }
        [ForeignKey("Campground")]
        public int CampgroundId { get; set; }
    }
}
