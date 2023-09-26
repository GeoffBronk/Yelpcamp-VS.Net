using System.ComponentModel.DataAnnotations;

namespace Yelpcamp.ViewModels
{
    public class CampgroundReviewViewModel
    {
        public int Id { get; set; }
        public string Body { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; }
        //public virtual ApplicationUser Author { get; set; }
        [MaxLength(450)]//string @ 450 matches Identity User ID DB definition
        public string AuthorUserId { get; set; }
        [MaxLength(256)]
        public string AuthorUserName { get; set; }
    }
}
