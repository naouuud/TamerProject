using Microsoft.AspNetCore.Mvc;
using TamerProject.Models;

namespace TamerProject.Controllers
{
    [Route("api/Profiles")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly IProfileRepository _profileRepository;
        public ProfilesController(IProfileRepository profileRepository) { _profileRepository = profileRepository; }

        [HttpGet]
        public async Task<ActionResult<List<Profile>>> GetProfiles()
        {
            var result = await _profileRepository.RepoGetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Profile>> GetProfile(int id)
        {
            var result = await _profileRepository.RepoGetById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostProfile([FromBody] Profile profile)
        {
            if (profile.Name.Length == 0) return BadRequest("The Name property must contain at least one character.");
            await _profileRepository.RepoAdd(profile);
            return CreatedAtAction("GetProfile", new { id = profile.Id }, profile);
        }

        [HttpPut]
        public async Task<IActionResult> PutProfile([FromBody] Profile profile)
        {
            if (profile.Name.Length == 0) return BadRequest("The Name property must contain at least one character.");
            await _profileRepository.RepoUpdate(profile);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            var result = await _profileRepository.RepoGetById(id);
            if (result == null) return NotFound();
            await _profileRepository.RepoDelete(result);
            return NoContent();
        }

        [HttpPost("Search")]
        public async Task<ActionResult<List<Profile>>> GetProfilesBySearch([FromBody] string searchTerm)
        {
            var result = await _profileRepository.RepoSearch(searchTerm);
            return Ok(result);
        }
    }
}
