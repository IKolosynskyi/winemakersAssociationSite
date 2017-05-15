using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WinemakersAssociation.Areas.Backend.Controllers
{
    [Area("Backend")]
    public class HomeController : Controller
    {
        private IHostingEnvironment _environment;

        public HomeController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void UploadImage(IFormFile image)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "uploads");

                using (var fileStream = new FileStream(Path.Combine(uploads, image.FileName), FileMode.Create))
                {
                    image.CopyToAsync(fileStream);
                }

        }
    }
}