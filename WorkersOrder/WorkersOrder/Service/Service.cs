﻿using Microsoft.EntityFrameworkCore;
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
                    return u;
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
            foreach (var item in str)
            {
                if (item >= 'a' && item <= 'z')
                    return true;
            }
            return false;
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


    }
}
