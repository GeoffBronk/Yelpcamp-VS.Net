using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yelpcamp.Models
{
    public class CampgroundImage
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public string Filename { get; set; }
        [ForeignKey("Campground")]
        public int CampgroundId { get; set; }
    }
}
