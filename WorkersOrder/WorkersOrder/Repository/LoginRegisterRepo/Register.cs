using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkersOrder.Models;
using WorkersOrder.Repository;

namespace WorkersOrder.Repository.LoginRegisterRepo
{
    public class Register: IRepository<Employee>
    { 
        private Context db;
        public Register()
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

        public async Task Save()
        {
           await db.SaveChangesAsync();
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

        void IRepository<Employee>.Save()
        {
            throw new NotImplementedException();
        }
    }
    
}
