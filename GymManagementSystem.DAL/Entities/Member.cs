using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.DAL.Entities
{
    public class Member
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Email { get; set; } = null!;

        [MaxLength(11)]
        public string Phone { get; set; } = null!;

        public string? Gender { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? Photo { get; set; }

        public string? BuildingNumber { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }

        public ICollection<Membership> Memberships { get; set; } = new List<Membership>();
        public HealthRecord? HealthRecord { get; set; }
    }
}
