using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkersOrder.Controllers
{
    public class PanelController : Controller
    {
        public IActionResult Admin()
        {
            return View();
        }
    }
}
