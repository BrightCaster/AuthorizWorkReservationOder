using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkersOrder.Models;

namespace WorkersOrder.Controllers
{
    public class PanelController : Controller
    {
        private Context db;
        private Service.Service service;
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
    }
}
