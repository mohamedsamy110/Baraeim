using Graduation_Project.Dtos;
using Graduation_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Graduation_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevelopmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DevelopmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateDevelopment([FromBody] CreateDevelopmentDto request)
        {
            var development = new Development
            {
                Category = request.Category,
                Videos = request.Videos.Select(v => new Video
                {
                    Title = v.Title,
                    Description = v.Description,
                    Url = v.Url
                }).ToList()
            };

            _context.Developments.Add(development);
            await _context.SaveChangesAsync();

            return Ok("Development category created successfully.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDevelopment(int id)
        {
            var development = await _context.Developments
                .Include(d => d.Videos)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (development == null)
            {
                return NotFound("Development category not found.");
            }

            return Ok(development);
        }

        [HttpPost("add-video")]
        public async Task<IActionResult> AddVideo([FromBody] CreateVideoDto request, int developmentId)
        {
            var development = await _context.Developments.FindAsync(developmentId);
            if (development == null)
            {
                return NotFound("Development category not found.");
            }

            var video = new Video
            {
                DevelopmentId = developmentId,
                Title = request.Title,
                Description = request.Description,
                Url = request.Url
            };

            _context.Videos.Add(video);
            await _context.SaveChangesAsync();

            return Ok("Video added successfully.");
        }

        [HttpPut("update-video/{id}")]
        public async Task<IActionResult> UpdateVideo(int id, [FromBody] UpdateVideoDto request)
        {
            var video = await _context.Videos.FindAsync(id);
            if (video == null)
            {
                return NotFound("Video not found.");
            }

            video.Title = request.Title;
            video.Description = request.Description;
            video.Url = request.Url;

            await _context.SaveChangesAsync();

            return Ok("Video updated successfully.");
        }

        [HttpDelete("delete-video/{id}")]
        public async Task<IActionResult> DeleteVideo(int id)
        {
            var video = await _context.Videos.FindAsync(id);
            if (video == null)
            {
                return NotFound("Video not found.");
            }

            _context.Videos.Remove(video);
            await _context.SaveChangesAsync();

            return Ok("Video deleted successfully.");
        }


    }
}
