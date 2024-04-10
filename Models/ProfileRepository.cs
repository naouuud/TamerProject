using Microsoft.EntityFrameworkCore;

namespace TamerProject.Models
{
    public interface IProfileRepository
    {
        Task<List<Profile>> RepoGetAll();
        Task<Profile?> RepoGetById(int id);
        Task<string> RepoAdd(Profile profile);
        Task<string> RepoUpdate(Profile updatedProfile);
        Task RepoDelete(Profile profile);
        Task<List<Profile>> RepoSearch(string searchTerm);
    }

    public class ProfileRepository: IProfileRepository
    {
        private readonly ProfileContext _context;
        public ProfileRepository(ProfileContext context) { _context = context; }

        public async Task<List<Profile>> RepoGetAll()
        {
            var result = await _context.Profiles.ToListAsync();
            return result;
        }

        public async Task<Profile?> RepoGetById(int id) 
        {
            return await _context.Profiles.FindAsync(id);
        }

        public async Task<string> RepoAdd(Profile profile)
        {
            if (profile.Name.Length == 0)
            {
                return "The Name property must contain at least one character.";
            }
            if (profile.Date_of_birth != null)
            {
                var dob = profile.Date_of_birth.GetValueOrDefault();
                profile.Date_of_birth = new DateTime(dob.Year, dob.Month, dob.Day);
            }
            _context.Profiles.Add(profile);
            await _context.SaveChangesAsync();
            return "Ok";
        }

        public async Task<string> RepoUpdate(Profile updatedProfile)
        {
            var entry = await _context.Profiles.FindAsync(updatedProfile.Id);
            if (entry != null)
            {
                if (updatedProfile.Name.Length == 0)
                {
                    return "The Name property must contain at least one character.";
                }
                entry.Name = updatedProfile.Name;
                entry.Address = updatedProfile.Address;
                if (updatedProfile.Date_of_birth != null)
                {
                    var dob = updatedProfile.Date_of_birth.GetValueOrDefault();
                    updatedProfile.Date_of_birth = new DateTime(dob.Year, dob.Month, dob.Day);
                }
                entry.Date_of_birth = updatedProfile.Date_of_birth;
            }
            await _context.SaveChangesAsync();
            return "Ok";
        }

        public async Task RepoDelete(Profile profile)
        {
            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Profile>> RepoSearch(string searchTerm)
        {
            return await _context.Profiles
                .Where(e 
                => e.Name.Contains(searchTerm) 
                || e.Address!.Contains(searchTerm) 
                || e.Date_of_birth.ToString()!.Contains(searchTerm))
                .ToListAsync();
        }
    }
}
