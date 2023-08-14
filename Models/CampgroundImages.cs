using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yelpcamp.Models
{
    public class CampgroundImages
    {
        public int Id { get; set; }
        [ForeignKey("Campground")]
        public int CampgroundsId { get; set; }
        public string URL { get; set; }
        public string Filename { get; set; }
    }
}
