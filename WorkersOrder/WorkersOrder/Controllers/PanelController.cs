using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkersOrder.Models.ViewModels;
using WorkersOrder.Models;
using System.ComponentModel.DataAnnotations;

namespace WorkersOrder.Controllers
{
    public class PanelController : Controller
    {
        private Context db;
        private Service.Service service;
        private Reservations reservations;
        private WorkPlaces WorkPlaces;
        private static int? id;
        private static int? iddevice;
        private static int countdev;
        

        public PanelController(Context context)
        {
            this.db = context;
            this.service = new Service.Service(context);
            
        }
        [HttpGet]
        public IActionResult Admin(LoginModel model)
        {
            string login = HttpContext.User.Identity.Name;
            if (login != "admin")
                return RedirectToAction("EmployeePanel");
            Employee employee = service.GetEmployee().FirstOrDefault(u=> service.RemoveSpace(u.Login) == login);
            ViewData["UserName"] = employee.Name;
            ViewData["UserSurname"] = employee.Surname;
            ViewData["UserIdWorker"] = employee.IDWorker;
            return View(service.GetTableReservations());
        }
        [HttpPost]
        public IActionResult Admin(string Delete, string ID, string DeleteAdminWork)
        {
            
            string login = HttpContext.User.Identity.Name;
            Employee employee = service.GetEmployee().FirstOrDefault(u => service.RemoveSpace(u.Login) == login);
            if (DeleteAdminWork != null)
            {
                id = Convert.ToInt32(ID);
                IEnumerable<Reservations> table = service.GetTableReservations();
                reservations = table.FirstOrDefault(u => u.ReservationID == Convert.ToInt32(id));
                reservations.Status = 1;
                reservations.IDWorker = null;
                service.UpdateReservation(reservations);
                ViewData["UserName"] = employee.Name;
                ViewData["UserSurname"] = employee.Surname;
                ViewData["UserIdWorker"] = employee.IDWorker;
                return View(service.GetTableReservations());
            }
            if (Delete != null)
            {
                service.DeleteReservation(Convert.ToInt32(ID));
                ViewData["UserName"] = employee.Name;
                ViewData["UserSurname"] = employee.Surname;
            }
            return View(service.GetTableReservations());
        }
        [HttpGet]
        public IActionResult EmployeePanel()
        {
            string login = HttpContext.User.Identity.Name;
            if (login == "admin")
                return RedirectToAction("Admin");
            Employee employee = service.GetEmployee().FirstOrDefault(u => service.RemoveSpace(u.Login) == login);
            ViewData["UserName"] = employee.Name;
            ViewData["UserSurname"] = employee.Surname;
            ViewData["UserIdWorker"] = employee.IDWorker;
            return View(service.GetTableReservations());
        }
        [HttpPost]
        public IActionResult EmployeePanel(string Delete, string ID)
        {
            if (Delete != null)
            {
                id = Convert.ToInt32(ID);
                string login = HttpContext.User.Identity.Name;
                Employee employee = service.GetEmployee().FirstOrDefault(u => service.RemoveSpace(u.Login) == login);
                IEnumerable<Reservations> table = service.GetTableReservations();
                reservations = table.FirstOrDefault(u => u.ReservationID == Convert.ToInt32(id));
                reservations.Status=1;
                reservations.IDWorker = null;
                service.UpdateReservation(reservations);
                ViewData["UserName"] = employee.Name;
                ViewData["UserSurname"] = employee.Surname;
                ViewData["UserIdWorker"] = employee.IDWorker;
            }
            return View(service.GetTableReservations());
        }


        [HttpGet]
        public IActionResult ChangesAdmin(string ID)
        {
            id = Convert.ToInt32(ID);
            return View();
        }
        [HttpPost]
        public IActionResult ChangesAdmin(ReservationModel model, string Cancel)
        {
            if (Cancel != null)
                return RedirectToAction("Admin");
            if (ModelState.IsValid)
            {
                string login = HttpContext.User.Identity.Name;
                Employee employee = service.GetEmployee().FirstOrDefault(u => service.RemoveSpace(u.Login) == login);
                
                IEnumerable<Reservations> table = service.GetTableReservations();
                reservations = table.FirstOrDefault(u => u.ReservationID == Convert.ToInt32(id));
                if(service.ValidDate(model.StartDate,model.EndDate, DateTime.Now))
                {
                    reservations.Status = model.Status;
                    reservations.StartDate = model.StartDate;
                    reservations.EndDate = model.EndDate;
                    if (reservations.Status == 2)
                        reservations.IDWorker = employee.IDWorker;
                    

                    service.UpdateReservation(reservations);
                    id = null;
                    return RedirectToAction("Admin");
                }
                else ModelState.AddModelError("","Wrong format date");
            }
            return View();
        }
        [HttpGet]
        public IActionResult ChangesEmployee(string ID)
        {
            id = Convert.ToInt32(ID);
            return View();
        }
        [HttpPost]
        public IActionResult ChangesEmployee(ReservationModel model, string Cancel)
        {
            if (Cancel != null)
                return RedirectToAction("EmployeePanel");
            if (ModelState.IsValid)
            {
                string login = HttpContext.User.Identity.Name;
                Employee employee = service.GetEmployee().FirstOrDefault(u => service.RemoveSpace(u.Login) == login);
                IEnumerable<Reservations> table = service.GetTableReservations();
                reservations = table.FirstOrDefault(u => u.ReservationID == Convert.ToInt32(id));
                reservations.Status = model.Status;
                if (reservations.Status == 2)
                    reservations.IDWorker = employee.IDWorker;
                service.UpdateReservation(reservations);
                id = null;
                return RedirectToAction("EmployeePanel");
            }
            return View();
        }

        [HttpGet]
        public IActionResult DetailsAdmin(string ID, string Back)
        {
            if (Back != null)
                return RedirectToAction("Admin");
            if (ID != null)
                id = Convert.ToInt32(ID);
            ViewBag.Id = id;
            return View(service.GetTableWorkPlaces());
        }
        [HttpPost]
        public IActionResult DetailsAdmin(string Back, string ids, string Delete)
        {
            if (Delete != null)
            {
                service.DeleteWorkplacesDevices(Convert.ToInt32(ids), id);
            }

            ViewBag.Id = id;
            return View(service.GetTableWorkPlaces());

        }

        [HttpGet]
        public IActionResult ChangeDevicesAdmin(string ID)
        {
            iddevice = Convert.ToInt32(ID);
            return View();
        }

        [HttpPost]
        public IActionResult ChangeDevicesAdmin(WorkPlaceModel model, string Cancel)
        {
            if (Cancel != null)
                return RedirectToAction("DetailsAdmin");
            if (ModelState.IsValid)
            {
                IEnumerable<WorkPlaces> table = service.GetTableWorkPlaces();
                WorkPlaces = table.FirstOrDefault(u => u.DevicesID == Convert.ToInt32(iddevice) && u.OrderID==id);
                WorkPlaces.Discription = model.Discription;
                service.UpdateWorkPlaces(WorkPlaces);
                return RedirectToAction("DetailsAdmin");
            }
            return View();
        }

        
        public IActionResult DetailsEmployee(string ID, string Back)
        {
            if (Back != null)
                return RedirectToAction("EmployeePanel");
            
            id = Convert.ToInt32(ID);
            
            ViewBag.Id = id;
            return View(service.GetTableWorkPlaces());
        }
        [HttpGet]
        public IActionResult AddDetailsAdmin(string CountDevices, string ID)
        {
            id = Convert.ToInt32(ID);
            countdev = Convert.ToInt32(CountDevices);
            return View();
        }
        [HttpPost]
        public IActionResult AddDetailsAdmin(WorkPlaceModel model, string Cancel)
        {
            if (Cancel != null)
                return RedirectToAction("DetailsAdmin");
            if (ModelState.IsValid)
            {
                countdev = countdev + 1;
                service.AddWorkplaceDevices(model.Discription, countdev, id);
                return RedirectToAction("DetailsAdmin");
            }
            return View();
        }
        [HttpGet]
        public IActionResult AddWorkplace(string count)
        {
            id = Convert.ToInt32(count);
            
            return View();
        }
        [HttpPost]
        public IActionResult AddWorkplace(ReservationModel model, string Cancel)
        {
            bool trues;
            if (Cancel != null)
                return RedirectToAction("Admin");
            if (ModelState.IsValid)
            {
                
                int ReserID = (int)id + 1;
                service.AddWorkplace(ReserID, model, DateTime.Now, out trues);
                if (trues)
                    return RedirectToAction("Admin");
                else
                {
                    ModelState.AddModelError("", "Wrong format date");
                    return View();
                }
            }
            return View();
        }

    }
}
