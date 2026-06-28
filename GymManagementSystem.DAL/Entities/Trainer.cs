using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.DAL.Entities
{
    public class Trainer
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Email { get; set; } = null!;

        [MaxLength(11)]
        public string Phone { get; set; } = null!;

        [Required, MaxLength(50)]
        public string Specialization { get; set; } = null!;

        public string? Photo { get; set; }

        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
