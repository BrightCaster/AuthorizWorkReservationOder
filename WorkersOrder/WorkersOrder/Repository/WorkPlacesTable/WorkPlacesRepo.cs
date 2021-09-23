using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkersOrder.Models;

namespace WorkersOrder.Repository.WorkPlacesTable
{
    public class WorkPlacesRepo : IRepository<WorkPlaces>
    {
        Context db;
        public WorkPlacesRepo()
        {
            this.db = new Context();
        }
        public void Create(WorkPlaces item)
        {
            db.workplaces.Add(item);
        }

        public void Delete(int id)
        {
            WorkPlaces workPlaces= db.workplaces.Find(id);
            if (workPlaces != null)
                db.workplaces.Remove(workPlaces);
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

        public WorkPlaces GetByOneFromID(int id)
        {
            return db.workplaces.Find(id);
        }

        public IEnumerable<WorkPlaces> GetInList()
        {
            return db.workplaces;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(WorkPlaces item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
