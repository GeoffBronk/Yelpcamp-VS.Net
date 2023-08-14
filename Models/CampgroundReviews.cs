using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yelpcamp.Areas.Identity.Data;

namespace Yelpcamp.Models
{
    public class CampgroundReviews
    {
        public int Id { get; set; }
        [ForeignKey("Campground")]
        public int CampgroundsId { get; set; }
        public string Body { get; set; }
        public int Rating { get; set; }
        public virtual ApplicationUser Author { get; set; }
        public string AuthorUserName { get; set; }
    }
}
