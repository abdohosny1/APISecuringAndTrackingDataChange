


namespace MyAPISecuringAndTrackingDataChange.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<Employee> Employees { get; }
        IBaseRepository<Department> Departments { get; }

        int Complete();
    }
}
