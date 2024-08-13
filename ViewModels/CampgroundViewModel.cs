using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Nodes;
using Yelpcamp.Models;

namespace Yelpcamp.ViewModels
{
    public class CampgroundViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(256)]
        public string? Title { get; set; }
        [Required]
        [MaxLength(256)]
        public string? Description { get; set; }
        [Required]
        [MaxLength(256)]
        public string? Location { get; set; }
        [Required]
        public float Price { get; set; }
        public double? GeometryXCoord { get; set; }
        public double? GeometryYCoord { get; set; }
        [MaxLength(256)]
        public string? Geometry { get; set; }
        [MaxLength(450)]//string @ 450 matches Identity User ID DB definition
        public string? AuthorUserId { get; set; }
        [MaxLength(256)]
        public string? AuthorUserName { get; set; }
        public IEnumerable<CampgroundImageViewModel>? CampgroundImages { get; set; }
        public IEnumerable<CampgroundReviewViewModel>? CampgroundReviews { get; set; }
    }
}