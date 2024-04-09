using System.ComponentModel.DataAnnotations;

namespace TamerProject.Models
{
    public class Profile
    {
        public int Id { get; set; }
        [StringLength(255)]
        public required string Name { get; set; }
        [StringLength(255)]
        public string? Address { get; set; }
        public DateTime? Date_of_birth { get; set; }
    }
}
