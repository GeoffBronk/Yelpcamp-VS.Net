using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yelpcamp.Areas.Identity.Data;

namespace Yelpcamp.Models
{
    public class Campgrounds
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public float Price { get; set; }
        public string Geometry { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public IEnumerable<CampgroundImages> CampgroundImages { get; set; }
        public IEnumerable<CampgroundReviews> CampgroundReviews { get; set; }
    }
}
