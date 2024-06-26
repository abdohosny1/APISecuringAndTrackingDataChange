


namespace MyAPISecuringAndTrackingDataChange
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _context;
        public IBaseRepository<Employee> Employees { get; private set; }
        public IBaseRepository<Department> Departments { get; private set; }



        public UnitOfWork(ApplicationDBContext context)
        {
            _context = context;

            Employees = new BaseRepository<Employee>(_context);
            Departments = new BaseRepository<Department>(_context);


        }
        public int Complete()
        {
            return _context.SaveChanges();

        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
