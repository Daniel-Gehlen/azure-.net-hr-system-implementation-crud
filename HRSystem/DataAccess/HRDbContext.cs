// HRDbContext.cs (DbContext)
public class HRDbContext : DbContext
{
    public HRDbContext(DbContextOptions <HRDbContext> options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<EmployeeLog> EmployeeLogs { get; set; }
}