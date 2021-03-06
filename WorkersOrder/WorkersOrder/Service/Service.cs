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
using WorkersOrder.Repository.WorkPlacesTable;
using WorkersOrder.Repository.DevicesTable;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace WorkersOrder.Service
{
    public class Service
    {
        private Context db;
        private Register register ;
        private Reservation reservation;
        private WorkPlacesRepo WorkPlacesRepo;
        private DevicesRepo DevicesRepo;
        public Service(Context context)
        {
            this.db = context;
            this.register = new Register();
            this.reservation = new Reservation();
            this.WorkPlacesRepo = new WorkPlacesRepo();
            this.DevicesRepo = new DevicesRepo();
        }
        public async Task<Employee> FindAccountModel(RegisterModel Rmodel, LoginModel Lmodel)
        {
            if (Rmodel == null)
            {
                Employee u = await db.employee.FirstOrDefaultAsync(u => u.Login == Lmodel.Login && u.Password==Lmodel.Password);
                if (u == null) return null;
                int indexSpace = u.Login.IndexOf(" ");
                string Login;
                if (indexSpace != -1)
                    Login = u.Login.Remove(indexSpace);
                else Login = u.Login;
                string Password = u.Password;
                if (Equals(Login, Lmodel.Login) && Equals(Password, Lmodel.Password))
                {
                    u.Login = Login;
                    return u;
                }
                    
            }
            
            else if (Lmodel == null)
            {
                Employee u = await db.employee.FirstOrDefaultAsync(u => u.Login == Rmodel.Login );
                if (u == null) return null;
                else return u;
            }
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
        public bool TrueRoles(LoginModel model)
        {
            Employee u = db.employee.FirstOrDefault(u => u.Login == model.Login);
            int indexSpace = u.Login.IndexOf(" ");
            string Login;
            if (indexSpace != -1)
                Login = u.Login.Remove(indexSpace);
            else Login = u.Login;
            if (Login == "admin") return true;
            return false;
        } 
        public bool Latin(string str)
        {
            bool check = true;
            foreach (char item in str)
            {
                if ((int)item < 97 || (int)item > 122)
                    return false;
            }
            return true;
        }
        public void UpdateReservation(Reservations reservations)
        {
            reservation.Update(reservations);
            reservation.Save();
        }

        public void UpdateWorkPlaces(WorkPlaces workPlaces)
        {
            WorkPlacesRepo.Update(workPlaces);
            WorkPlacesRepo.Save();
        }
        public IEnumerable<WorkPlaces> GetTableWorkPlaces()
        {
            return WorkPlacesRepo.GetInList();
        }
        public void UpdateDevices(Devices devices)
        {
            DevicesRepo.Update(devices);
            DevicesRepo.Save();
        }
        public IEnumerable<Devices> GetTableDevices()
        {
            return DevicesRepo.GetInList();
        }
        public void DeleteReservation(int ID)
        {
            reservation.Delete(ID);
            reservation.Save();
        }
        public void AddWorkplaceDevices(string type, int DevID, int? IDs)
        {
            IEnumerable<WorkPlaces> workPlaces = WorkPlacesRepo.GetInList();
            WorkPlacesRepo.Create(new WorkPlaces{DevicesID=DevID,Discription=type,OrderID=(int)IDs });
            WorkPlacesRepo.Save();
        }
        public void DeleteWorkplacesDevices(int index, int? ID)
        {
            IEnumerable<WorkPlaces> workPlaces = WorkPlacesRepo.GetInList();
            foreach (var item in workPlaces)
            {
                if (item.DevicesID == index && item.OrderID==(int)ID)
                {
                    int i = item.ID;
                    WorkPlacesRepo.Delete(i);
                }
            }
            WorkPlacesRepo.Save();
        }
        public void AddWorkplace(int ReserID, ReservationModel model, DateTime dateNow, out bool trues)
        {
            reservation.Create(new Reservations{IDWorker=null, StartDate=model.StartDate, EndDate=model.EndDate, Status=1});
            if (ValidDate(model.StartDate, model.EndDate, dateNow))
            {
                reservation.Save();
                trues = true;
            }
                
            else trues = false;
            
        }
        public IEnumerable<Employee> GetEmployee()
        {
            return register.GetInList();
        }
        public string RemoveSpace(string s)
        {
            int indexSpace = s.IndexOf(" ");
            string Login;
            if (indexSpace != -1)
                Login = s.Remove(indexSpace);
            else Login = s;
            return Login;
        }
        public bool ValidDate(DateTime Start, DateTime End, DateTime dateNow)
        {
            if (Start <= End && (Start>=dateNow.AddDays(-1)))
                return true;
            else return false;
        }
        


    }
}
