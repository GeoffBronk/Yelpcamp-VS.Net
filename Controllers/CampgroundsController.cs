using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yelpcamp.Areas.Identity.Data;
using Yelpcamp.Models;
using Yelpcamp.ViewModels;
using System.Security.Claims;

namespace Yelpcamp.Controllers
{
    public class CampgroundsController : Controller
    {
        private ApplicationDbContext _context;
        public CampgroundsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Campground> campgrounds = await _context.Campgrounds.AsNoTracking().Include(c => c.CampgroundImages).ToListAsync();

            return View(campgrounds);
        }

        [HttpGet]
        [Authorize]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CampgroundViewModel campground)
        {
            if (!ModelState.IsValid || campground == null)
            {
                return BadRequest();
            }

            CampgroundImage campgroundImage = new CampgroundImage { 
                URL = campground.Image,
                Filename = Path.GetFileName(campground.Image)
            };

            Campground newCampground = new Campground{
                Id = campground.Id,
                Title = campground.Title,
                Description = campground.Description,
                Location = campground.Location,
                Price = campground.Price,
                Geometry = campground.Geometry,
                AuthorUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                AuthorUserName = User.FindFirst(ClaimTypes.Name).Value
             };

            if (campgroundImage.Filename != null)
                newCampground.CampgroundImages = new List<CampgroundImage> { campgroundImage };

            _context.Campgrounds.Add(newCampground);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Campgrounds");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (_context.Campgrounds == null)
            {
                return BadRequest();
            }

            Campground campgroundInDb = await _context.Campgrounds.AsNoTracking()
                .Include(c => c.CampgroundReviews)
                .Include(c => c.CampgroundImages)
                .Where(c => c.Id == id).SingleAsync();

            if (campgroundInDb == null)
            {
                return NotFound();
            }

            CampgroundViewModel campgroundViewData = new CampgroundViewModel
            {
                Id = campgroundInDb.Id,
                Title = campgroundInDb.Title,
                Description = campgroundInDb.Description,
                Location = campgroundInDb.Location,
                Price = campgroundInDb.Price,
                Geometry = campgroundInDb.Geometry,
                AuthorUserId = campgroundInDb.AuthorUserId,
                AuthorUserName = campgroundInDb.AuthorUserName,
            };

            if (campgroundInDb.CampgroundImages != null)
            {
                List<CampgroundImageViewModel> campgroundImages = new List<CampgroundImageViewModel>();
                foreach (var image in campgroundInDb.CampgroundImages)
                {
                    CampgroundImageViewModel imageViewModel = new CampgroundImageViewModel
                    {
                        Id = image.Id,
                        Filename = image.Filename,
                        URL = image.URL
                    };
                    campgroundImages.Add(imageViewModel);
                }
                campgroundViewData.CampgroundImages = campgroundImages;
            }

            if (campgroundInDb.CampgroundReviews != null)
            {
                List<CampgroundReviewViewModel> campgroundReviews = new List<CampgroundReviewViewModel>();
                foreach (var review in campgroundInDb.CampgroundReviews)
                {
                    CampgroundReviewViewModel reviewViewModel = new CampgroundReviewViewModel
                    {
                        Id = review.Id,
                        Body = review.Body,
                        Rating = review.Rating,
                        AuthorUserId = review.AuthorUserId,
                        AuthorUserName = review.AuthorUserName
                    };
                    campgroundReviews.Add(reviewViewModel);
                }
                campgroundViewData.CampgroundReviews = campgroundReviews;
            }

            return View(campgroundViewData);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Campgrounds == null)
            {
                return BadRequest();
            }
            
            Campground campground = await _context.Campgrounds.SingleAsync(m => m.Id == id);

            if (campground == null)
            {
                return NotFound();
            }

            if (campground.AuthorUserId != User.FindFirst(ClaimTypes.NameIdentifier).Value)
                return BadRequest();

            CampgroundViewModel campgroundViewData = new CampgroundViewModel
            {
                Id = campground.Id,
                Title = campground.Title,
                Description = campground.Description,
                Location = campground.Location,
                Price = campground.Price,
                Geometry = campground.Geometry,
                AuthorUserId = campground.AuthorUserId,
                AuthorUserName = campground.AuthorUserName
            };

            return View(campgroundViewData);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CampgroundViewModel campEditData)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", campEditData);
            }

            Campground campInDb = await _context.Campgrounds.SingleAsync(c => c.Id == campEditData.Id);

            if (campInDb == null)
            {
                return NotFound();
            }

            campInDb.Title = campEditData.Title;
            campInDb.Description = campEditData.Description;
            campInDb.Location = campEditData.Location;
            campInDb.Price = campEditData.Price;

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Campgrounds");
        }

        public async Task<IActionResult> Delete(int id)
        {
            Campground campground = await _context.Campgrounds.SingleAsync(c => c.Id == id);
            if (campground == null)
                return NotFound();

            _context.Campgrounds.Remove(campground);

            await _context.SaveChangesAsync();

            return Redirect("/Campgrounds/Index");
        }
    }
}