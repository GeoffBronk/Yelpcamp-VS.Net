﻿using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yelpcamp.Areas.Identity.Data;

namespace Yelpcamp.Models
{
    public class Campground
    {
        public int Id { get; set; }
        [MaxLength(256)]
        public string Title { get; set; }
        [MaxLength(256)]
        public string Description { get; set; }
        [MaxLength(256)]
        public string? Location { get; set; }
        public float Price { get; set; }
        [MaxLength(256)]
        public string? Geometry { get; set; }
        [MaxLength(450)]//string @ 450 matches Identity User ID DB definition
        public string AuthorUserId { get; set; }
        [MaxLength(256)]
        public string AuthorUserName { get; set; }
        public IEnumerable<CampgroundImage>? CampgroundImages { get; set; }
        public IEnumerable<CampgroundReview>? CampgroundReviews { get; set; }
    }
}
