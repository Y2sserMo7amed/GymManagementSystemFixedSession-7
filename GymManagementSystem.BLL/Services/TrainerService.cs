using GymManagementSystem.BLL.Interfaces;
using GymManagementSystem.BLL.Mapping;
using GymManagementSystem.DAL.Entities;
using GymManagementSystem.DAL.Repositories;
using GymManagementSystem.BLL.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace GymManagementSystem.BLL.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public TrainerService(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env        = env;
        }

        public IEnumerable<TrainerViewModel> GetAllTrainers()
            => _unitOfWork.Trainers.GetAll()
                .Select(t => MappingProfile.ToTrainerViewModel(t));

        public TrainerDetailsViewModel? GetTrainerDetails(int id)
        {
            var t = _unitOfWork.Trainers.GetById(id);
            if (t is null) return null;
            return MappingProfile.ToTrainerDetailsViewModel(t);
        }

        public bool CreateTrainer(TrainerCreateViewModel model)
        {
            var trainer = MappingProfile.ToTrainer(model);
            trainer.Photo = SavePhoto(model.Photo);

            _unitOfWork.Trainers.Add(trainer);
            _unitOfWork.Complete();
            return true;
        }

        public TrainerEditViewModel? GetTrainerForEdit(int id)
        {
            var t = _unitOfWork.Trainers.GetById(id);
            if (t is null) return null;
            return MappingProfile.ToTrainerEditViewModel(t);
        }

        public bool EditTrainer(TrainerEditViewModel model)
        {
            var trainer = _unitOfWork.Trainers.GetById(model.Id);
            if (trainer is null) return false;

            trainer.Email          = model.Email;
            trainer.Phone          = model.Phone;
            trainer.Specialization = model.Specialization;

            if (model.Photo is not null)
                trainer.Photo = SavePhoto(model.Photo);

            _unitOfWork.Trainers.Update(trainer);
            _unitOfWork.Complete();
            return true;
        }

        public TrainerDeleteViewModel? GetTrainerForDelete(int id)
        {
            var t = _unitOfWork.Trainers.GetById(id);
            if (t is null) return null;
            return MappingProfile.ToTrainerDeleteViewModel(t);
        }

        public bool DeleteTrainer(int id)
        {
            if (HasScheduledSessions(id)) return false;

            var trainer = _unitOfWork.Trainers.GetById(id);
            if (trainer is null) return false;

            _unitOfWork.Trainers.Delete(trainer);
            _unitOfWork.Complete();
            return true;
        }

        public bool HasScheduledSessions(int id)
            => _unitOfWork.Sessions.GetAll()
                           .Any(s => s.TrainerId == id &&
                                     s.Date >= DateOnly.FromDateTime(DateTime.Today));

        private string? SavePhoto(IFormFile? photo)
        {
            if (photo is null || photo.Length == 0) return null;

            var folder = Path.Combine(_env.WebRootPath, "images", "trainers");
            Directory.CreateDirectory(folder);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(photo.FileName)}";
            using var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create);
            photo.CopyTo(stream);

            return $"trainers/{fileName}";
        }
    }
}
