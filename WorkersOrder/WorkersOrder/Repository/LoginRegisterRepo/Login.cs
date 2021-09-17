using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkersOrder.Repository.LoginRepo;
using WorkersOrder.Models;

namespace WorkersOrder.Repository.LoginRepo
{
    public class Login : IRepository<Employee>
    {
        private Context db;
        public Login()
        {
            this.db = new Context();
        }
        public void Create(Employee item)
        {
            db.employee.Add(item);
        }

        public void Delete(int id)
        {
            Employee employee = db.employee.Find(id);
            if (employee != null)
                db.employee.Remove(employee);
        }

        public Employee GetByOneFromID(int id)
        {
            return db.employee.Find(id);
        }

        public IEnumerable<Employee> GetInList()
        {
            return db.employee;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Employee item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing) db.Dispose();
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
        }
    }

}
