using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CarsManagement.Areas.AdminPanel.Controllers
{
    public class AdminPanelController : Controller
    {
        public IActionResult AdminPage()
        {
            return View();
        }
    }
}