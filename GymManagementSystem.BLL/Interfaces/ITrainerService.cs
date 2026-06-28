using GymManagementSystem.BLL.ViewModels;
using Microsoft.AspNetCore.Http;

namespace GymManagementSystem.BLL.Interfaces
{
    public interface ITrainerService
    {
        IEnumerable<TrainerViewModel> GetAllTrainers();
        TrainerDetailsViewModel? GetTrainerDetails(int id);
        bool CreateTrainer(TrainerCreateViewModel model);
        TrainerEditViewModel? GetTrainerForEdit(int id);
        bool EditTrainer(TrainerEditViewModel model);
        TrainerDeleteViewModel? GetTrainerForDelete(int id);
        bool DeleteTrainer(int id);
        bool HasScheduledSessions(int id);
    }
}
