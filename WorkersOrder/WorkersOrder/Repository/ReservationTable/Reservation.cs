using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkersOrder.Repository.ReservationTable;
using WorkersOrder.Models;
using WorkersOrder.Repository;

namespace WorkersOrder.Repository.ReservationTable
{
    public class Reservation : IRepository<Reservations>
    {
        private Context db;
        public Reservation()
        {
            this.db = new Context();
        }

        public void Create(Reservations item)
        {
            db.reservations.Add(item);
        }

        public void Delete(int id)
        {
            Reservations reservations = db.reservations.Find(id);
            if (reservations != null)
                db.reservations.Remove(reservations);
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

        public Reservations GetByOneFromID(int id)
        {
            return db.reservations.Find(id);
        }

        public IEnumerable<Reservations> GetInList()
        {
            return db.reservations;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Reservations item)
        {
            db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
