using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using Yelpcamp.Areas.Identity.Data;
using Yelpcamp.Models;

namespace Yelpcamp.Controllers
{
    public class CampgroundReviewsController : Controller
    {
        private ApplicationDbContext _context;
        public CampgroundReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int Id, CampgroundReview campgroundReview)
        {
            campgroundReview.AuthorUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            campgroundReview.AuthorUserName = User.FindFirst(ClaimTypes.Name).Value;

            Campground campground = await _context.Campgrounds.SingleAsync(m => m.Id == Id);
            if (campground == null)
                return NotFound();
            
            campground.CampgroundReviews = new List<CampgroundReview> { campgroundReview };

            await _context.SaveChangesAsync();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            CampgroundReview campgroundReview = await _context.CampgroundReviews.SingleAsync(c => c.Id == id);
            if (campgroundReview == null)
                return NotFound();

            _context.CampgroundReviews.Remove(campgroundReview);

            await _context.SaveChangesAsync();

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}