using Microsoft.EntityFrameworkCore;

namespace TamerProject.Models
{
    public interface IProfileRepository
    {
        Task<List<Profile>> RepoGetAll();
        Task<Profile?> RepoGetById(int id);
        Task<int> RepoAdd(Profile profile);
        Task<int> RepoUpdate(Profile updatedProfile);
        Task RepoDelete(Profile profile);
        Task<List<Profile>> RepoSearch(string searchTerm);
    }

    public class ProfileRepository: IProfileRepository
    {
        private readonly ProfileContext _context;
        public ProfileRepository(ProfileContext context) { _context = context; }

        public async Task<List<Profile>> RepoGetAll()
        {
            return await _context.Profiles.ToListAsync();
        }

        public async Task<Profile?> RepoGetById(int id) 
        {
            return await _context.Profiles.FindAsync(id);
        }

        public async Task<int> RepoAdd(Profile profile)
        {
            _context.Profiles.Add(profile);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> RepoUpdate(Profile updatedProfile)
        {
            var entry = await _context.Profiles.FindAsync(updatedProfile.Id);
            if (entry != null)
            {
                entry.Name = updatedProfile.Name;
                entry.Address = updatedProfile.Address;
                entry.Date_of_birth = updatedProfile.Date_of_birth;
            }
            return await _context.SaveChangesAsync();
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
