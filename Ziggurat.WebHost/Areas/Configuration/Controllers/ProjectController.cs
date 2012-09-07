using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ziggurat.Web.Areas.Configuration.Models;

namespace Ziggurat.Web.Areas.Configuration.Controllers
{
    public class ProjectController : Controller
    {
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(CreateProjectModel model)
        {
            if (ModelState.IsValid)
            {
                return View("ProjectIsBeingCreated");
            }

            return View();
        }

        public ActionResult Configure(Guid id)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
