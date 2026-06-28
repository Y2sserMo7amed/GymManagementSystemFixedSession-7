using GymManagementSystem.DAL.Entities;

namespace GymManagementSystem.DAL.Repositories
{
    // The Unit of Work groups all the repositories together and makes sure
    // that all changes are saved to the database in ONE place (one SaveChanges call).
    public interface IUnitOfWork
    {
        IGenericRepository<Member>       Members       { get; }
        IGenericRepository<Trainer>      Trainers      { get; }
        IGenericRepository<Plan>         Plans         { get; }
        IGenericRepository<Membership>   Memberships   { get; }
        IGenericRepository<HealthRecord> HealthRecords { get; }
        IGenericRepository<Session>      Sessions      { get; }
        IGenericRepository<Category>     Categories    { get; }
        IGenericRepository<Booking>      Bookings      { get; }

        // Saves every change made through any repository above in a single transaction.
        int Complete();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymDbContext _context;

        public IGenericRepository<Member>       Members       { get; }
        public IGenericRepository<Trainer>      Trainers      { get; }
        public IGenericRepository<Plan>         Plans         { get; }
        public IGenericRepository<Membership>   Memberships   { get; }
        public IGenericRepository<HealthRecord> HealthRecords { get; }
        public IGenericRepository<Session>      Sessions      { get; }
        public IGenericRepository<Category>     Categories    { get; }
        public IGenericRepository<Booking>      Bookings      { get; }

        public UnitOfWork(GymDbContext context)
        {
            _context = context;

            Members       = new GenericRepository<Member>(context);
            Trainers      = new GenericRepository<Trainer>(context);
            Plans         = new GenericRepository<Plan>(context);
            Memberships   = new GenericRepository<Membership>(context);
            HealthRecords = new GenericRepository<HealthRecord>(context);
            Sessions      = new GenericRepository<Session>(context);
            Categories    = new GenericRepository<Category>(context);
            Bookings      = new GenericRepository<Booking>(context);
        }

        public int Complete() => _context.SaveChanges();
    }
}
