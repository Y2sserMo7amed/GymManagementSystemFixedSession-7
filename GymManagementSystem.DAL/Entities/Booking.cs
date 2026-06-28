namespace GymManagementSystem.DAL.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int SessionId { get; set; }
        public Member? Member { get; set; }
        public Session? Session { get; set; }
    }
}
