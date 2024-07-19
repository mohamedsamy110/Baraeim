using Graduation_Project.Dtos;
using Graduation_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Graduation_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevelopmentnewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DevelopmentnewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{category}")]
        public async Task<ActionResult<DevelopmentDto>> GetDevelopment(string category)
        {
            var development = await _context.Developments
                .Include(d => d.Videos)
                .Where(d => d.Category == category)
                .FirstOrDefaultAsync();

            if (development == null)
            {
                return NotFound();
            }

            var developmentDto = new DevelopmentDto
            {
                Id = development.Id,
                Category = development.Category,
                Videos = development.Videos.Select(v => new VideoDto
                {
                    Id = v.Id,
                    Title = v.Title,
                    Url = v.Url
                }).ToList()
            };

            return Ok(developmentDto);
        }

        [HttpPost]
        public async Task<ActionResult<DevelopmentDto>> CreateDevelopment([FromBody] DevelopmentDto developmentDto)
        {
            var development = new Development
            {
                Category = developmentDto.Category,
                Videos = developmentDto.Videos.Select(v => new Video
                {
                    Title = v.Title,
                    Url = v.Url
                }).ToList()
            };

            _context.Developments.Add(development);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDevelopment), new { category = development.Category }, developmentDto);
        }

        [HttpPost("{id}/videos")]
        public async Task<ActionResult<VideoDto>> AddVideo(int id, [FromBody] VideoDto videoDto)
        {
            var development = await _context.Developments.FindAsync(id);

            if (development == null)
            {
                return NotFound();
            }

            var video = new Video
            {
                Title = videoDto.Title,
                Url = videoDto.Url
            };

            development.Videos.Add(video);
            await _context.SaveChangesAsync();

            videoDto.Id = video.Id;

            return Ok(videoDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDevelopment(int id)
        {
            var development = await _context.Developments.FindAsync(id);

            if (development == null)
            {
                return NotFound();
            }

            _context.Developments.Remove(development);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
