using DTO.Employees;
using DTO.Repository;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace Web.Areas.Api.Controllers
{
    //[Authorize]
    public class EmployeesController : ApiController
    {
        private IEmployeeRepository _context;
        public EmployeesController(IEmployeeRepository employeeService)
        {
            this._context = employeeService;
        }

        public IQueryable<Employee> GetEmployees()
        {
            return this._context.GetEmployees();
        }

        public Employee GetEmployee(long id)
        {
            return this._context.GetEmployeeById(id);
        }

        public async Task<IHttpActionResult> Post(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }


            var result = await this.StoreAsync(employee);
            return this.Ok<Employee>(result);
        }

        public async Task<IHttpActionResult> Delete(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (employee.Id == -1)
                return this.Ok<int>(await Clear());

            var result = await this.DeleteAsync(employee);
            return this.Ok<bool>(result);
        }

        public async Task<IHttpActionResult> Patch(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var result = await this._context.Update(employee);
            return this.Ok<Employee>(result);
        }

        private async Task<int> Clear()
        {
            return (await this._context.ClearAll());
        }

        private async Task<Employee> StoreAsync(Employee employee)
        {
            var result = await this._context.Add(employee);

            return result;
        }

        private async Task<bool> DeleteAsync(Employee employee)
        {
            var result = await this._context.Delete(employee);

            return result;
        }
    }
}
