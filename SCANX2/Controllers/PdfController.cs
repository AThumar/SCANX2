using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting; // ✅ Required for IWebHostEnvironment

namespace SCANX2.Controllers
{
    public class PdfController : Controller
    {
        private readonly string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        private readonly IWebHostEnvironment _hostingEnvironment; // ✅ Add this

        public PdfController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            uploadPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

            // Ensure the uploads folder exists
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
        }

        //public IActionResult Index()
        //{
        //    var files = Directory.GetFiles(uploadPath);
        //    ViewBag.PdfFiles = files;
        //    return View();
        //}
        [HttpGet]
        public IActionResult Index()
        {
            var files = Directory.GetFiles(uploadPath)
                                 .Select(Path.GetFileName)
                                 .ToList();

            return Json(files); // ✅ Return file names
        }


        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.ContentType != "application/pdf")
            {
                ViewBag.Message = "Please upload a valid PDF file.";
                return RedirectToAction("Index");
            }

            try
            {
                // ✅ Ensure the uploads directory exists
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                // ✅ Save the file
                string filePath = Path.Combine(uploadPath, file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                Console.WriteLine($"✅ File Uploaded Successfully: {filePath}"); // Debugging log

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Upload Error: {ex.Message}");
                return BadRequest("Error uploading file.");
            }
        }

        [HttpGet]
        public IActionResult ViewPDF(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return NotFound("File name is missing!");
            }

            string filePath = Path.Combine(uploadPath, fileName);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound($"File not found: {filePath}");
            }

            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return File(stream, "application/pdf"); // ✅ This will open in the browser
        }
    }
}
