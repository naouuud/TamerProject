using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TamerProject.Models;

namespace TamerProject.Controllers
{
    [Route("api/Profile")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly ProfileContext _context;

        public ProfileController(ProfileContext context)
        {
            _context = context;
        }

        // GET: api/Profile
        [HttpGet]
        public IActionResult Get()
        {
            var query = from p in _context.Profiles
                        select p;

            var result = query.ToList();

            return Ok(result);
        }

        // GET: api/Profile/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var query = from p in _context.Profiles
                        where p.Id == id
                        select p;

            var profiles = query.ToList();

            return Ok(profiles);
        }
        //public async Task<ActionResult<Profile>> GetProfileItem(long id)
        //{
        //    var profileItem = await _context.Profiles.FindAsync(id);

        //    if (profileItem == null)
        //    {
        //        return NotFound();
        //    }

        //    return profileItem;
        //}

        // PUT: api/Profile/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfileItem(long id, Profile profileItem)
        {
            if (id != profileItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(profileItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileItemExists(id))
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

        // POST: api/Profile
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Profile>> PostProfileItem(Profile profileItem)
        {
            _context.Profiles.Add(profileItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfileItem", new { id = profileItem.Id }, profileItem);
        }

        // DELETE: api/Profile/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfileItem(long id)
        {
            var profileItem = await _context.Profiles.FindAsync(id);
            if (profileItem == null)
            {
                return NotFound();
            }

            _context.Profiles.Remove(profileItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfileItemExists(long id)
        {
            return _context.Profiles.Any(e => e.Id == id);
        }
    }
}
