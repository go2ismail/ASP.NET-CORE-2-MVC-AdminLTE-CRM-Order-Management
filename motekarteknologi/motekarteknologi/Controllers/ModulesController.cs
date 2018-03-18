using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using motekarteknologi.Motekar;
using motekarteknologi.ViewModels;

namespace motekarteknologi.Controllers
{
    [Authorize]
    public class ModulesController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            List<Module> modules = new List<Module>();
            foreach (var item in Modules.All)
            {
                modules.Add(new Module {
                    Name = item,
                    Description = item,
                    Controller = "Dashboards",
                    Action = "Index",
                    Role = item,
                    Area = item.Replace(" ", "")
                });
            }
            return View(modules);
        }

        [Authorize(Roles = Motekar.Modules.Security)]
        public IActionResult Security()
        {
            return View();
        }

        [Authorize(Roles = Motekar.Modules.CRM)]
        public IActionResult CRM()
        {
            return View();
        }

        [Authorize(Roles = Motekar.Modules.Inventory)]
        public IActionResult Inventory()
        {
            return View();
        }

        [Authorize(Roles = Motekar.Modules.SO)]
        public IActionResult SalesOrder()
        {
            return View();
        }

        [Authorize(Roles = Motekar.Modules.PO)]
        public IActionResult PurchaseOrder()
        {
            return View();
        }

    }
}