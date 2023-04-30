using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeFirstDemo.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace CodeFirstDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveysController : ControllerBase
    {
        private readonly BlogDbContext _context;

        public SurveysController(BlogDbContext context)
        {
            _context = context;
        }

        // GET: api/Surveys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Survey>>> GetSurveys()
        {
            var options = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                WriteIndented = true // indent JSON for readability
            };

            var surveys = await _context.Surveys
                .Include(q => q.Questions)
                .ThenInclude(a => a.Answers)
                .ToListAsync();

            // Remove circular references to simplify the JSON output
            foreach (var survey in surveys)
            {
                foreach (var question in survey.Questions)
                {
                    question.Survey = null;

                    foreach (var answer in question.Answers)
                    {
                        answer.Question = null;
                    }
                }
            }

            return Ok(JsonSerializer.Serialize(surveys, options));
        }

        // GET: api/Surveys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Survey>> GetSurvey(int id)
        {
            var survey = await _context.Surveys
                .Include(q => q.Questions)
                    .ThenInclude(a => a.Answers)
                .FirstOrDefaultAsync(s => s.SurveyId == id);

            if (survey == null)
            {
                return NotFound();
            }

            // Remove circular references to simplify the JSON output
            foreach (var question in survey.Questions)
            {
                question.Survey = null;

                foreach (var answer in question.Answers)
                {
                    answer.Question = null;
                }
            }

            var options = new JsonSerializerOptions
            {
                IgnoreNullValues = true,
                WriteIndented = true // indent JSON for readability
            };

            return Ok(JsonSerializer.Serialize(survey, options));
        }


        // PUT: api/Surveys/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSurvey(int id, Survey survey)
        {
            if (id != survey.SurveyId)
            {
                return BadRequest();
            }

            _context.Entry(survey).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurveyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Surveys
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Survey>> PostSurvey(Survey survey)
        {
            if (_context.Surveys == null)
            {
                return Problem("Entity set 'BlogDbContext.Surveys' is null.");
            }

            _context.Surveys.Add(survey);

            foreach (var question in survey.Questions)
            {
                _context.Questions.Add(question);

                foreach (var answer in question.Answers)
                {
                    _context.Answers.Add(answer);
                }
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSurvey", new { id = survey.SurveyId }, survey);
        }

        // DELETE: api/Surveys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSurvey(int id)
        {
            if (_context.Surveys == null)
            {
                return NotFound();
            }
            var survey = await _context.Surveys.FindAsync(id);
            if (survey == null)
            {
                return NotFound();
            }

            _context.Surveys.Remove(survey);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SurveyExists(int id)
        {
            return (_context.Surveys?.Any(e => e.SurveyId == id)).GetValueOrDefault();
        }
    }
}
