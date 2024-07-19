using Graduation_Project.Dtos;
using Graduation_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Graduation_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyExercisesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DailyExercisesController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<DailyExerciseDto>>> GetExercises()
        {
            var exercises = await _context.DailyExercises
                .Select(e => new DailyExerciseDto
                {
                    Title = e.Title,
                    Description = e.Description,
                    VideoUrl = e.VideoUrl
                })
                .ToListAsync();

            return Ok(exercises);
        }


        [HttpPost]
        public async Task<ActionResult<DailyExerciseDto>> CreateExercise([FromBody] DailyExerciseDto exerciseDto)
        {
            var exercise = new DailyExercise
            {
                Title = exerciseDto.Title,
                Description = exerciseDto.Description,
                VideoUrl = exerciseDto.VideoUrl
            };

            _context.DailyExercises.Add(exercise);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExercises), new { id = exercise.Id }, exerciseDto);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<DailyExerciseDto>> GetExercise(int id)
        {
            var exercise = await _context.DailyExercises.FindAsync(id);

            if (exercise == null)
            {
                return NotFound();
            }

            var exerciseDto = new DailyExerciseDto
            {
                Title = exercise.Title,
                Description = exercise.Description,
                VideoUrl = exercise.VideoUrl
            };

            return Ok(exerciseDto);
        }

    }
}
