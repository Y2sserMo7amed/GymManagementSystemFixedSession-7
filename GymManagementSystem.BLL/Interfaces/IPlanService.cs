using GymManagementSystem.BLL.ViewModels;

namespace GymManagementSystem.BLL.Interfaces
{
    public interface IPlanService
    {
        IEnumerable<PlanViewModel> GetAllPlans();
        PlanDetailsViewModel? GetPlanDetails(int id);
        PlanEditViewModel? GetPlanForEdit(int id);
        bool EditPlan(PlanEditViewModel model);
        bool ToggleActivation(int id);
        bool HasActiveMembers(int id);
    }
}
