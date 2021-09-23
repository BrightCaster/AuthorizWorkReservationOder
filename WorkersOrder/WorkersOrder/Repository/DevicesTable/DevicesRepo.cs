using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkersOrder.Models;
using WorkersOrder.Repository;

namespace WorkersOrder.Repository.DevicesTable
{
    public class DevicesRepo : IRepository<Devices>
    {
        Context db;
        public DevicesRepo()
        {
            this.db = new Context();
        }
        public void Create(Devices item)
        {
            db.devices.Add(item);
        }

        public void Delete(int id)
        {
            Devices devices = db.devices.Find(id);
            if (devices != null)
                db.devices.Remove(devices);
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

        public Devices GetByOneFromID(int id)
        {
            return db.devices.Find(id);
        }

        public IEnumerable<Devices> GetInList()
        {
            return db.devices;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Devices item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
