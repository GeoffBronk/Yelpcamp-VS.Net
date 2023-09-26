using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yelpcamp.Areas.Identity.Data;

namespace Yelpcamp.Models
{
    public class CampgroundReview
    {
        public int Id { get; set; }
        public string Body { get; set; }
        [Range(1,5)]
        public int Rating { get; set; }
        [MaxLength(450)]//string @ 450 matches Identity User ID DB definition
        public string AuthorUserId { get; set; }
        [MaxLength(256)]
        public string AuthorUserName { get; set; }
        [ForeignKey("Campground")]
        public int CampgroundId { get; set; }
    }
}
