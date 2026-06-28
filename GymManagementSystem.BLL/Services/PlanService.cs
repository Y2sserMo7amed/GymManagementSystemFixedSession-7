using GymManagementSystem.BLL.Interfaces;
using GymManagementSystem.BLL.Mapping;
using GymManagementSystem.DAL.Repositories;
using GymManagementSystem.BLL.ViewModels;

namespace GymManagementSystem.BLL.Services
{
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PlanViewModel> GetAllPlans()
            => _unitOfWork.Plans.GetAll()
                .Select(p => MappingProfile.ToPlanViewModel(p));

        public PlanDetailsViewModel? GetPlanDetails(int id)
        {
            var p = _unitOfWork.Plans.GetById(id);
            if (p is null) return null;
            return MappingProfile.ToPlanDetailsViewModel(p);
        }

        public PlanEditViewModel? GetPlanForEdit(int id)
        {
            var p = _unitOfWork.Plans.GetById(id);
            if (p is null) return null;
            return MappingProfile.ToPlanEditViewModel(p);
        }

        public bool EditPlan(PlanEditViewModel model)
        {
            var plan = _unitOfWork.Plans.GetById(model.Id);
            if (plan is null) return false;

            plan.DurationDays = model.DurationDays;
            plan.Price        = model.Price;
            plan.Description  = model.Description;

            _unitOfWork.Plans.Update(plan);
            _unitOfWork.Complete();
            return true;
        }

        public bool ToggleActivation(int id)
        {
            var plan = _unitOfWork.Plans.GetById(id);
            if (plan is null) return false;

            if (plan.IsActive && HasActiveMembers(id))
                return false;

            plan.IsActive = !plan.IsActive;
            _unitOfWork.Plans.Update(plan);
            _unitOfWork.Complete();
            return true;
        }

        public bool HasActiveMembers(int id)
            => _unitOfWork.Memberships.GetAll()
                              .Any(m => m.PlanId == id &&
                                        m.EndDate >= DateOnly.FromDateTime(DateTime.Today));
    }
}
