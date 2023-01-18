using ImageUploaderAPI.Data;
using ImageUploaderAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text.Json.Serialization;

namespace ImageUploaderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly string _imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "images");
        private readonly ApplicationDbContext _context;
        public ImageController(ApplicationDbContext context) { _context = context; }

        [HttpPost]
        public async Task<IActionResult> UploadImage()
        {
            try
            {
                string fileName = null;
                var image = Request.Form.Files["Image"];
                var name = Request.Form["ImageCaption"];

                if (image.Length > 0)
                {

                    fileName = new string(Path.GetFileNameWithoutExtension(image.FileName).Take(10).ToArray()).Replace(" ", "-");
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(image.FileName);

                    if (!Directory.Exists(_imagesFolder))
                    {
                        Directory.CreateDirectory(_imagesFolder);
                    }

                    var filePath = Path.Combine(_imagesFolder, name + Path.GetExtension(image.FileName));
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }

                    ImageHeader toSave = new ImageHeader
                    {
                        ImageCaption = name,
                        ImageUrl = filePath
                    };
                    using (var memoryStream = new MemoryStream())
                    {
                        await image.OpenReadStream().CopyToAsync(memoryStream);
                        toSave.ImageData = memoryStream.ToArray();
                    }
                    await _context.Images.AddAsync(toSave);
                    await _context.SaveChangesAsync();

                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            try
            {
                //var images = Directory.GetFiles(_imagesFolder)
                //    .Select(x => new { Name = Path.GetFileNameWithoutExtension(x), ImagePath = x });

                var images = await _context.Images.ToListAsync();

                return Ok(images);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
