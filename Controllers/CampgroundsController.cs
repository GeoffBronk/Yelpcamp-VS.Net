using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yelpcamp.Areas.Identity.Data;
using Yelpcamp.Models;
using Yelpcamp.ViewModels;
using System.Security.Claims;
// Import packages for Cloudinary (via Cloudinary .Net Quick start guide)
//==============================
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using dotenv.net;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;

namespace Yelpcamp.Controllers
{
    public class CampgroundsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Cloudinary cloudinary;
        private readonly String mapboxApiKey;
        public CampgroundsController(ApplicationDbContext context)
        {
            _context = context;

            // Create Cloudinary object and set credentials
            //=================================
            DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));
            cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
            cloudinary.Api.Secure = true;

            mapboxApiKey = Environment.GetEnvironmentVariable("MAPBOX_TOKEN");
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
        public async Task<IActionResult> Create(CampgroundViewModel campground, IFormFile mainImage)
        {
            if (!ModelState.IsValid || campground == null)
            {
                return BadRequest();
            }

            CampgroundImage campgroundImage = new CampgroundImage();

            if (mainImage != null && mainImage.Length > 0)
            {
                var fileTime = DateTime.UtcNow.ToString("yyMMddHHmmss");
                var fileName = fileTime + Path.GetFileName(mainImage.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory() + @"\wwwroot\img\upload", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await mainImage.CopyToAsync(fileStream);
                }

                ImageUploadParams uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(filePath),
                    Folder = "YelpCampAsp"
                };

                ImageUploadResult result = await cloudinary.UploadAsync(uploadParams);

                campgroundImage = new CampgroundImage {
                    URL = result.SecureUrl.ToString(),
                    AppPublicId = result.PublicId.ToString()
                };

                System.IO.File.Delete(filePath);
            }

            //use MapBox API to get GeoCoded Coordinates for the location
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("https://api.mapbox.com/geocoding/v5/mapbox.places/"
                                                                    + campground.Location.Replace(" ", "%20") 
                                                                    + ".json?access_token=" + mapboxApiKey);
            JObject? jsonObject = JObject.Parse(await response.Content.ReadAsStringAsync());

            Campground newCampground = new Campground{
                Id = campground.Id,
                Title = campground.Title,
                Description = campground.Description,
                Location = campground.Location,
                Price = campground.Price,
                Geometry = (string)jsonObject["Geometry"],
                AuthorUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                AuthorUserName = User.FindFirst(ClaimTypes.Name).Value
            };

            if (campgroundImage.AppPublicId != null)
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
                        AppPublicId = image.AppPublicId,
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
            
            Campground campground = await _context.Campgrounds.AsNoTracking().Include(c => c.CampgroundImages).SingleAsync(m => m.Id == id);

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

            List<CampgroundImageViewModel> campgroundImages = new List<CampgroundImageViewModel>();
            
            if(campground.CampgroundImages != null && campground.CampgroundImages.Any())
            {
                foreach (var image in campground.CampgroundImages)
                {
                    campgroundImages.Add(new CampgroundImageViewModel() 
                        { URL = image.URL, AppPublicId = image.AppPublicId, Id = image.Id });
                }
            }

            campgroundViewData.CampgroundImages = campgroundImages;

            return View(campgroundViewData);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CampgroundViewModel campEditData, IEnumerable<IFormFile> newImages, List<int> deleteImages)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", campEditData);
            }

            Campground campInDb = await _context.Campgrounds.Include(c => c.CampgroundImages).SingleAsync(c => c.Id == campEditData.Id);

            if (campInDb == null)
            {
                return NotFound();
            }

            campInDb.Title = campEditData.Title;
            campInDb.Description = campEditData.Description;
            campInDb.Price = campEditData.Price;

            //See if new location needs to be goecoded for mapping
            if (campInDb.Location != campEditData.Location)
            {
                //use MapBox API to get GeoCoded Coordinates for the location
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("https://api.mapbox.com/geocoding/v5/mapbox.places/"
                                                                        + campEditData.Location.Replace(" ", "%20") 
                                                                        + ".json?access_token=" + mapboxApiKey);
                //response.EnsureSuccessStatusCode();
                JObject? jsonObject = JObject.Parse(await response.Content.ReadAsStringAsync());

                campInDb.Geometry = (string)jsonObject["Geometry"];
                campInDb.Location = campEditData.Location;
            }
            campInDb.Price = campEditData.Price;

            //Process new images
            if (newImages != null && newImages.Count() > 0)
            {
                List<CampgroundImage> campgroundImages = campInDb.CampgroundImages.ToList();
                foreach (var image in newImages)
                {
                    var fileTime = DateTime.UtcNow.ToString("yyMMddHHmmss");
                    var fileName = fileTime + Path.GetFileName(image.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory() + @"\wwwroot\img\upload", fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }

                    ImageUploadParams uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(filePath),
                        Folder = "YelpCampAsp"
                    };

                    ImageUploadResult result = await cloudinary.UploadAsync(uploadParams);

                    var campgroundImage = new CampgroundImage
                    {
                        URL = result.SecureUrl.ToString(),
                        AppPublicId = result.PublicId.ToString()
                    };

                    campgroundImages.Add(campgroundImage);

                    System.IO.File.Delete(filePath);
                }
                campInDb.CampgroundImages = campgroundImages;
            }

            //Delete images marked for deletion
            if (deleteImages.Count > 0) {
                List<CampgroundImage> campgroundImages = campInDb.CampgroundImages.ToList();
                foreach (int imageId in deleteImages) {
                    CampgroundImage campgroundImage = await _context.CampgroundImages.SingleAsync(c => c.Id == imageId);

                    DeletionResult deletionResult = await cloudinary.DestroyAsync(new DeletionParams(campgroundImage.AppPublicId));

                    campgroundImages.Remove(campgroundImage);
                }
                campInDb.CampgroundImages = campgroundImages;
            }

            await _context.SaveChangesAsync();

            return Redirect("/Campgrounds/Details/" + campEditData.Id);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Campground campground = await _context.Campgrounds.Include(c => c.CampgroundImages).SingleAsync(c => c.Id == id);

            if (campground == null) return NotFound();

            if (campground.CampgroundImages != null) {
                foreach (CampgroundImage image in campground.CampgroundImages) {
                    DeletionResult deletionResult = await cloudinary.DestroyAsync(new DeletionParams(image.AppPublicId));
                }
            }

            _context.Campgrounds.Remove(campground);

            await _context.SaveChangesAsync();

            return Redirect("/Campgrounds/Index");
        }
    }
}