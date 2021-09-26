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
        private static int? id;
        private static int countdev;
        public PanelController(Context context)
        {
            this.db = context;
            this.service = new Service.Service(context);
        }
        [HttpGet]
        public IActionResult Admin()
        {
            return View(service.GetTableReservations());
        }
        [HttpPost]
        public IActionResult Admin(string Delete, string ID)
        {
            if (Delete != null)
            {
                service.DeleteReservation(Convert.ToInt32(ID));
            }
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

        [HttpGet]
        public IActionResult DetailsAdmin(string ID, string Back)
        {
            if (Back != null)
                return RedirectToAction("Admin");
            if(ID!=null)
                id = Convert.ToInt32(ID);
            ViewBag.Id = id;
            return View(service.GetTableWorkPlaces());
        }
        [HttpPost]
        public IActionResult DetailsAdmin(string Back, string ids, string Delete)
        {
            if (Delete != null)
            {
                service.DeleteWorkplacesDevices(Convert.ToInt32(ids),id);
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
                return RedirectToAction("DetailsAdmin");
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
                return RedirectToAction("DetailsEpmloyee");
            if (id == null)
            {
                id = Convert.ToInt32(ID);
            }
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
        public IActionResult AddDetailsAdmin(WorkPlaceModel model,string Cancel)
        {
            if (Cancel != null)
                return RedirectToAction("DetailsAdmin");
            if (ModelState.IsValid)
            {
                countdev = countdev + 1;
                service.AddWorkplaceDevices(model.Discription,countdev,id);
                return RedirectToAction("DetailsAdmin");
            }
            return View();
        }

    }
}
