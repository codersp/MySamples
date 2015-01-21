using Entities.Employees;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Repository
{
    public class EmployeeRepository : IEmployeeRepository, IDisposable
    {
        Entities.Employees.EmployeeDBContext dbContext;
        public EmployeeRepository()
        {
            this.dbContext = new Entities.Employees.EmployeeDBContext();
            //if (this.dbContext.Employees.Count() <= 10)
            //{
            //    this.dbContext.Employees.Add(
            //           new Employee()
            //           {
            //               Email = "test@test.com",
            //               FirstName = "T1",
            //               LastName = "T2",
            //               Phone = "333-333-3333"
            //           });
            //}

            //if (this.dbContext.ChangeTracker.HasChanges())
            //    this.dbContext.SaveChanges();
        }

        public IQueryable<Entities.Employees.Employee> GetEmployees()
        {
            return this.dbContext.Employees;
        }

        public Entities.Employees.Employee GetEmployeeById(long Id)
        {
            return this.dbContext.Employees.SingleOrDefault(x => x.Id == Id);
        }

        public async Task<Employee> Add(Employee employee)
        {
            this.dbContext.Employees.Add(employee);
            await dbContext.SaveChangesAsync();

            return this.dbContext.Employees.FirstOrDefault(x => x.FirstName == employee.FirstName && x.Email == employee.Email && employee.LastName == employee.LastName);

        }

        public async Task<bool> Delete(Employee employee)
        {
            var deleteItem = this.dbContext.Employees.FirstOrDefault(x => x.Email == employee.Email);
            if (deleteItem != null)
            {
                this.dbContext.Employees.Remove(deleteItem);
                await dbContext.SaveChangesAsync();

                return this.dbContext.Employees.FirstOrDefault(x => x.FirstName == employee.FirstName && x.Email == employee.Email && employee.LastName == employee.LastName) == null;
            }


            throw new System.Data.Entity.Validation.DbEntityValidationException(string.Format("employee with email {0} could not be found", employee.Email));

        }

        public async Task<Employee> Update(Employee employee)
        {
            var updateItem = this.dbContext.Employees.FirstOrDefault(x => x.Email == employee.Email);

            if (updateItem != null)
            {
                updateItem.FirstName = employee.FirstName;
                updateItem.LastName = employee.LastName;

                await dbContext.SaveChangesAsync();

                return this.dbContext.Employees.FirstOrDefault(x => x.FirstName == employee.FirstName && x.Email == employee.Email && employee.LastName == employee.LastName);
            }

            throw new System.Data.Entity.Validation.DbEntityValidationException(string.Format("employee with email {0} could not be found", employee.Email));
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.dbContext != null)
                {
                    this.dbContext.Dispose();
                    this.dbContext = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}