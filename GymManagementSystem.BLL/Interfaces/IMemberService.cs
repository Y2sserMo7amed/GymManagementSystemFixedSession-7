using GymManagementSystem.BLL.ViewModels;

namespace GymManagementSystem.BLL.Interfaces
{
    public interface IMemberService
    {
        IEnumerable<MemberViewModel> GetAllMembers();
        MemberViewModel? GetMemberDetails(int id);
        bool CreateMember(CreateMemberViewModel model);
        EditMemberViewModel? GetMemberForEdit(int id);
        bool EditMember(EditMemberViewModel model);
        bool DeleteMember(int id);
    }
}
