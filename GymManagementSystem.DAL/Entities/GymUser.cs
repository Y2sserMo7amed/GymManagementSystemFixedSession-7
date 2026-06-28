using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GymManagementSystem.DAL.Entities
{
    public class GymUser : IdentityUser
    {
        [Required, MaxLength(50)]
        public string Name { get; set; } = null!;

        [MaxLength(11)]
        public override string? PhoneNumber { get; set; }
    }
}
