namespace GymManagementSystem.DAL.Entities
{
    public class Membership
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int PlanId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public Member? Member { get; set; }
        public Plan? Plan { get; set; }
    }
}
