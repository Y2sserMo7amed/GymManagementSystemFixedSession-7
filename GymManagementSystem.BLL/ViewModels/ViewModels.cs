using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace GymManagementSystem.BLL.ViewModels
{
   

    public class MemberViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Gender { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string? Photo { get; set; }
        public string? PlanName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public DateOnly? MembershipStartDate { get; set; }
        public DateOnly? MembershipEndDate { get; set; }
        public string? Address => $"{BuildingNumber} {Street}, {City}".Trim(' ', ',');
        public string? BuildingNumber { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
    }

    public class CreateMemberViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone must be Egyptian format (010|011|012|015)XXXXXXXX")]
        public string Phone { get; set; } = string.Empty;

        public string? Gender { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public IFormFile? Photo { get; set; }

        public string? BuildingNumber { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }

        public double? Height { get; set; }
        public double? Weight { get; set; }
        public string? BloodType { get; set; }
        public string? Note { get; set; }
    }

    public class EditMemberViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;   
        public string? Photo { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone must be Egyptian format")]
        public string Phone { get; set; } = string.Empty;

        public string? BuildingNumber { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
    }

   

    public class PlanViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int DurationDays { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }

    public class PlanDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int DurationDays { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }

    public class PlanEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;   

        [Required(ErrorMessage = "Duration is required")]
        [Range(1, 3650, ErrorMessage = "Duration must be between 1 and 3650 days")]
        public int DurationDays { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 100000, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;
    }

    

    public class TrainerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string? Photo { get; set; }
    }

    public class TrainerDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;
        public string? Photo { get; set; }
    }

    public class TrainerCreateViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone must be Egyptian format (010|011|012|015)XXXXXXXX")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Specialization is required")]
        [MaxLength(50)]
        public string Specialization { get; set; } = string.Empty;

        public IFormFile? Photo { get; set; }
    }

    public class TrainerEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;   
        public string? ExistingPhoto { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone must be Egyptian format")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Specialization is required")]
        [MaxLength(50)]
        public string Specialization { get; set; } = string.Empty;

        public IFormFile? Photo { get; set; }
    }

    public class TrainerDeleteViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

   

    public class AnalyticsViewModel
    {
        public int TotalMembers { get; set; }
        public int TotalTrainers { get; set; }
        public int TotalSessions { get; set; }
        public int ActivePlans { get; set; }

        public int MaleMembersCount { get; set; }
        public int FemaleMembersCount { get; set; }

        public List<CategorySessionCount> SessionsByCategory { get; set; } = new();

        public List<PlanMembershipCount> PlanPopularity { get; set; } = new();

        public List<MemberViewModel> RecentMembers { get; set; } = new();
    }

    public class CategorySessionCount
    {
        public string CategoryName { get; set; } = string.Empty;
        public int Count { get; set; }
    }

    public class PlanMembershipCount
    {
        public string PlanName { get; set; } = string.Empty;
        public int MemberCount { get; set; }
    }

   

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}
