using GymManagementSystem.BLL.Interfaces;
using GymManagementSystem.BLL.Mapping;
using GymManagementSystem.BLL.ViewModels;
using GymManagementSystem.DAL.Entities;
using GymManagementSystem.DAL.Repositories;

namespace GymManagementSystem.BLL.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnalyticsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public AnalyticsViewModel GetAnalytics()
        {
            List<Member>     allMembers     = new();
            List<Trainer>    allTrainers    = new();
            List<Session>    allSessions    = new();
            List<Plan>       allPlans       = new();
            List<Membership> allMemberships = new();
            List<Category>   allCategories  = new();

            try { allMembers     = _unitOfWork.Members.GetAll().ToList(); }     catch { }
            try { allTrainers    = _unitOfWork.Trainers.GetAll().ToList(); }    catch { }
            try { allSessions    = _unitOfWork.Sessions.GetAll().ToList(); }    catch { }
            try { allPlans       = _unitOfWork.Plans.GetAll().ToList(); }       catch { }
            try { allMemberships = _unitOfWork.Memberships.GetAll().ToList(); } catch { }
            try { allCategories  = _unitOfWork.Categories.GetAll().ToList(); }  catch { }

            int totalMembers  = allMembers.Count;
            int totalTrainers = allTrainers.Count;
            int totalSessions = allSessions.Count;
            int activePlans   = allPlans.Count(p => p.IsActive);

            int maleCount   = allMembers.Count(m => m.Gender == "Male");
            int femaleCount = allMembers.Count(m => m.Gender == "Female");

            var sessionsByCategory = allCategories
                .Select(cat => new CategorySessionCount
                {
                    CategoryName = cat.Name,
                    Count        = allSessions.Count(s => s.CategoryId == cat.Id)
                })
                .ToList();

            var planPopularity = allPlans
                .Select(plan => new PlanMembershipCount
                {
                    PlanName    = plan.Name,
                    MemberCount = allMemberships.Count(ms => ms.PlanId == plan.Id)
                })
                .OrderByDescending(x => x.MemberCount)
                .ToList();

            var recentMembers = allMembers
                .TakeLast(5)
                .Select(m => MappingProfile.ToMemberViewModel(m))
                .ToList();

            return new AnalyticsViewModel
            {
                TotalMembers       = totalMembers,
                TotalTrainers      = totalTrainers,
                TotalSessions      = totalSessions,
                ActivePlans        = activePlans,
                MaleMembersCount   = maleCount,
                FemaleMembersCount = femaleCount,
                SessionsByCategory = sessionsByCategory,
                PlanPopularity     = planPopularity,
                RecentMembers      = recentMembers
            };
        }
    }
}
