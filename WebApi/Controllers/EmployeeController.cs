using Entities.Employees;
using System;
using System.Linq;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController, IDisposable
    {
        // GET api/<controller>
        Entities.Employees.EmployeeDBContext context;
        private bool isDisposed = false;
        public EmployeeController()
        {
            context = new EmployeeDBContext();
            //context.Employees.ToList().ForEach(x => { x.LastName = "Last Name"; });
            //context.ChangeTracker.DetectChanges();
            //context.SaveChanges();
            //context.Employees.RemoveRange(context.Employees);
            //context.Employees.Add(
            //    new Employee()
            //    {
            //        Email = "test@test.com",
            //        FirstName = "T1",
            //        LastName = "T2",
            //        Phone = "333-333-3333"
            //    });
            //context.SaveChanges();
        }

        public IQueryable<Employee> Get()
        {
            return this.context.Employees;
        }

        // GET api/<controller>/5
        public Employee Get(long id)
        {
            return this.context.Employees.FirstOrDefault(x => x.Id == id);
        }

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}

        void IDisposable.Dispose()
        {
            if (!this.isDisposed)
            {
                this.isDisposed = true;
                this.context.Dispose();
            }
        }
    }
}