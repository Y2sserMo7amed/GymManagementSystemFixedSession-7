using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.DAL.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; } = null!;

        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
