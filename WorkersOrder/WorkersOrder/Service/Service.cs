using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkersOrder.Models;
using WorkersOrder.Models.ViewModels;
using WorkersOrder.Repository.LoginRegisterRepo;
using WorkersOrder.Repository.LoginRepo;

namespace WorkersOrder.Service
{
    public class Service
    {
        private Context db;
        public Register register ;
        public Login login;
        public Service(Context context)
        {
            this.db = context;
            this.register = new Register();
            this.login = new Login();
        }
        public async Task<Employee> Find(RegisterModel Rmodel, LoginModel Lmodel)
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
        public async Task Save()
        {
            await register.Save();
        }
    }
}
