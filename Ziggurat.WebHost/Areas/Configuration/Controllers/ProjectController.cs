using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ziggurat.Infrastructure;
using Ziggurat.Web.Areas.Configuration.Models;

namespace Ziggurat.Web.Areas.Configuration.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IViewModelReader _modelReader;
        private readonly ICommandSender _commandSender;

        public ProjectController(IViewModelReader modelReader, ICommandSender commandSender)
        {
            _modelReader = modelReader;
            _commandSender = commandSender;
        }

        public ProjectController()
            : this(Client.ViewModelReader, Client.CommandSender) { }

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
