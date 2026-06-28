using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.DAL.Entities
{
    public class Session
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;

        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public int Capacity { get; set; }

        public int TrainerId { get; set; }
        public int CategoryId { get; set; }

        public Trainer? Trainer { get; set; }
        public Category? Category { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
