using GymManagementSystem.BLL.ViewModels;
using GymManagementSystem.DAL.Entities;

namespace GymManagementSystem.BLL.Mapping
{
    // This is the Mapping Profile. It is the ONE place in the whole project where
    // an Entity (from the database) gets turned into a ViewModel (used by the views),
    // and back. Instead of writing "new XViewModel { Field = entity.Field, ... }"
    // separately inside every service, every service calls a method from here.
    public static class MappingProfile
    {
        // ---------- Member ----------

        public static MemberViewModel ToMemberViewModel(Member m) => new MemberViewModel
        {
            Id             = m.Id,
            Name           = m.Name,
            Email          = m.Email,
            Phone          = m.Phone,
            Gender         = m.Gender,
            Photo          = m.Photo,
            DateOfBirth    = m.DateOfBirth,
            BuildingNumber = m.BuildingNumber,
            Street         = m.Street,
            City           = m.City
        };

        public static EditMemberViewModel ToEditMemberViewModel(Member m) => new EditMemberViewModel
        {
            Id             = m.Id,
            Name           = m.Name,
            Photo          = m.Photo,
            Email          = m.Email,
            Phone          = m.Phone,
            BuildingNumber = m.BuildingNumber,
            Street         = m.Street,
            City           = m.City
        };

        public static Member ToMember(CreateMemberViewModel model) => new Member
        {
            Name           = model.Name,
            Email          = model.Email,
            Phone          = model.Phone,
            Gender         = model.Gender,
            DateOfBirth    = model.DateOfBirth,
            BuildingNumber = model.BuildingNumber,
            Street         = model.Street,
            City           = model.City
            // Photo is NOT set here: it is an uploaded file, handled separately by the service.
        };

        // ---------- Trainer ----------

        public static TrainerViewModel ToTrainerViewModel(Trainer t) => new TrainerViewModel
        {
            Id             = t.Id,
            Name           = t.Name,
            Email          = t.Email,
            Phone          = t.Phone,
            Specialization = t.Specialization,
            Photo          = t.Photo
        };

        public static TrainerDetailsViewModel ToTrainerDetailsViewModel(Trainer t) => new TrainerDetailsViewModel
        {
            Id             = t.Id,
            Name           = t.Name,
            Email          = t.Email,
            Phone          = t.Phone,
            Specialization = t.Specialization,
            Photo          = t.Photo
        };

        public static TrainerDeleteViewModel ToTrainerDeleteViewModel(Trainer t) => new TrainerDeleteViewModel
        {
            Id   = t.Id,
            Name = t.Name
        };

        public static TrainerEditViewModel ToTrainerEditViewModel(Trainer t) => new TrainerEditViewModel
        {
            Id             = t.Id,
            Name           = t.Name,
            Email          = t.Email,
            Phone          = t.Phone,
            Specialization = t.Specialization,
            ExistingPhoto  = t.Photo
            // Photo (the upload field) is left null on purpose: it's for a NEW file, not the existing one.
        };

        public static Trainer ToTrainer(TrainerCreateViewModel model) => new Trainer
        {
            Name           = model.Name,
            Email          = model.Email,
            Phone          = model.Phone,
            Specialization = model.Specialization
            // Photo is NOT set here: it is an uploaded file, handled separately by the service.
        };

        // ---------- Plan ----------

        public static PlanViewModel ToPlanViewModel(Plan p) => new PlanViewModel
        {
            Id           = p.Id,
            Name         = p.Name,
            Description  = p.Description,
            DurationDays = p.DurationDays,
            Price        = p.Price,
            IsActive     = p.IsActive
        };

        public static PlanDetailsViewModel ToPlanDetailsViewModel(Plan p) => new PlanDetailsViewModel
        {
            Id           = p.Id,
            Name         = p.Name,
            Description  = p.Description,
            DurationDays = p.DurationDays,
            Price        = p.Price,
            IsActive     = p.IsActive
        };

        public static PlanEditViewModel ToPlanEditViewModel(Plan p) => new PlanEditViewModel
        {
            Id           = p.Id,
            Name         = p.Name,
            DurationDays = p.DurationDays,
            Price        = p.Price,
            Description  = p.Description
        };
    }
}
