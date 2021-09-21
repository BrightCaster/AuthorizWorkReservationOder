using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkersOrder.Models;
using WorkersOrder.Models.ViewModels;
using WorkersOrder.Repository.LoginRegisterRepo;
using WorkersOrder.Repository;
using WorkersOrder.Repository.ReservationTable;

namespace WorkersOrder.Service
{
    public class Service
    {
        private Context db;
        private Register register ;
        private Reservation reservation;
        public Service(Context context)
        {
            this.db = context;
            this.register = new Register();
            this.reservation = new Reservation();
        }
        public async Task<Employee> FindAccountModel(RegisterModel Rmodel, LoginModel Lmodel)
        {
            if (Rmodel == null)
            {
                
                return await db.employee.FirstOrDefaultAsync(u => u.Login == Lmodel.Login && u.Password == Lmodel.Password);
            }
            
            else if (Lmodel == null)
                return await db.employee.FirstOrDefaultAsync(employee => employee.Login == Rmodel.Login);
            return null;
        }
        public void AddToDBEmployee(string Surname, string Name, string Login, string Password, int Role)
        {
            register.Create(new Employee { IDWorker = db.employee.Max(x => x.IDWorker)+1, Surname = Surname, Name = Name, Login = Login, Password = Password, Role = Role });
        }

        public IEnumerable<Reservations> GetTableReservations()
        {
            return reservation.GetInList();
        }
        public async Task Save()
        {
            await register.Save();
        }
        // создать булеву функцию на проверку роли входа для accountController
        
        

    }
}
