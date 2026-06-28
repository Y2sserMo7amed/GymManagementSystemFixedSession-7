using GymManagementSystem.BLL.Interfaces;
using GymManagementSystem.BLL.Mapping;
using GymManagementSystem.DAL.Entities;
using GymManagementSystem.DAL.Repositories;
using GymManagementSystem.BLL.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace GymManagementSystem.BLL.Services
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public MemberService(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env        = env;
        }

        public IEnumerable<MemberViewModel> GetAllMembers()
            => _unitOfWork.Members.GetAll()
                .Select(m => MappingProfile.ToMemberViewModel(m));

        public MemberViewModel? GetMemberDetails(int id)
        {
            var m = _unitOfWork.Members.GetById(id);
            if (m is null) return null;

            var ms = _unitOfWork.Memberships.GetAll()
                                .Where(x => x.MemberId == id)
                                .OrderByDescending(x => x.EndDate)
                                .FirstOrDefault();

            var model = MappingProfile.ToMemberViewModel(m);
            model.MembershipStartDate = ms?.StartDate;
            model.MembershipEndDate   = ms?.EndDate;

            return model;
        }

        public bool CreateMember(CreateMemberViewModel model)
        {
            var member = MappingProfile.ToMember(model);
            member.Photo = SavePhoto(model.Photo);

            _unitOfWork.Members.Add(member);
            _unitOfWork.Complete();

            if (model.Height.HasValue || model.Weight.HasValue || model.BloodType != null)
            {
                var hr = new HealthRecord
                {
                    MemberId  = member.Id,
                    Height    = model.Height,
                    Weight    = model.Weight,
                    BloodType = model.BloodType,
                    Note      = model.Note
                };
                _unitOfWork.HealthRecords.Add(hr);
                _unitOfWork.Complete();
            }

            return true;
        }

        public EditMemberViewModel? GetMemberForEdit(int id)
        {
            var m = _unitOfWork.Members.GetById(id);
            if (m is null) return null;
            return MappingProfile.ToEditMemberViewModel(m);
        }

        public bool EditMember(EditMemberViewModel model)
        {
            var member = _unitOfWork.Members.GetById(model.Id);
            if (member is null) return false;

            member.Email          = model.Email;
            member.Phone          = model.Phone;
            member.BuildingNumber = model.BuildingNumber;
            member.Street         = model.Street;
            member.City           = model.City;

            _unitOfWork.Members.Update(member);
            _unitOfWork.Complete();
            return true;
        }

        public bool DeleteMember(int id)
        {
            var member = _unitOfWork.Members.GetById(id);
            if (member is null) return false;
            _unitOfWork.Members.Delete(member);
            _unitOfWork.Complete();
            return true;
        }

        private string? SavePhoto(IFormFile? photo)
        {
            if (photo is null || photo.Length == 0) return null;
            var folder = Path.Combine(_env.WebRootPath, "images", "members");
            Directory.CreateDirectory(folder);
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(photo.FileName)}";
            using var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create);
            photo.CopyTo(stream);
            return $"members/{fileName}";
        }
    }
}
