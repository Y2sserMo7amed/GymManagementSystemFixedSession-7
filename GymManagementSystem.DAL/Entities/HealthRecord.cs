namespace GymManagementSystem.DAL.Entities
{
    public class HealthRecord
    {
        public int Id { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }
        public string? BloodType { get; set; }
        public string? Note { get; set; }
        public int MemberId { get; set; }
        public Member? Member { get; set; }
    }
}
