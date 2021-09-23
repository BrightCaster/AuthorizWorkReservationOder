using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkersOrder.Models.ViewModels;
using WorkersOrder.Models;

namespace WorkersOrder.Controllers
{
    public class PanelController : Controller
    {
        private Context db;
        private Service.Service service;
        private Reservations reservations;
        private WorkPlaces WorkPlaces;
        private Devices Devices;
        private static int? id;
        public PanelController(Context context)
        {
            this.db = context;
            this.service = new Service.Service(context);
        }
        
        public IActionResult Admin()
        {
            
            return View(service.GetTableReservations());
        }
        public IActionResult EmployeePanel()
        {

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
                IEnumerable<Reservations> table = service.GetTableReservations();
                reservations = table.FirstOrDefault(u => u.ReservationID == Convert.ToInt32(id));
                reservations.StartDate = model.StartDate;
                reservations.EndDate = model.EndDate;
                reservations.Status = model.Status;
                service.UpdateReservation(reservations);
                id = null;
                return RedirectToAction("Admin");
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
                IEnumerable<Reservations> table = service.GetTableReservations();
                reservations = table.FirstOrDefault(u => u.ReservationID == Convert.ToInt32(id));
                reservations.Status = model.Status;
                service.UpdateReservation(reservations);
                id = null;
                return RedirectToAction("EmployeePanel");
            }
            return View();
        }

        
        public IActionResult DetailsAdmin(string ID, string Back)
        {
            if (Back != null)
                return RedirectToAction("Admin");
            if (id == null)
            {
                id = Convert.ToInt32(ID);
                
            }
            ViewBag.Id = id;
            return View(service.GetTableWorkPlaces());
        }

        [HttpGet]
        public IActionResult ChangeDevicesAdmin(string ID)
        {
            id = Convert.ToInt32(ID);
            return View();
        }

        [HttpPost]
        public IActionResult ChangeDevicesAdmin(WorkPlaceModel model, string Cancel)
        {
            if (Cancel != null)
                return RedirectToAction("Admin");
            if (ModelState.IsValid)
            {
                IEnumerable<WorkPlaces> table = service.GetTableWorkPlaces();
                WorkPlaces = table.FirstOrDefault(u => u.DevicesID == Convert.ToInt32(id));
                WorkPlaces.Discription = model.Discription;
                service.UpdateWorkPlaces(WorkPlaces);
                return RedirectToAction("DetailsAdmin");
            }
            return View();
        }

        [HttpGet]
        public IActionResult DetailsEmployee(string ID, string Back)
        {
            if (Back != null)
                return RedirectToAction("EmployeePanel");
            if (id == null)
            {
                id = Convert.ToInt32(ID);
            }
            ViewBag.Id = id;
            return View(service.GetTableWorkPlaces());
        }

    }
}
