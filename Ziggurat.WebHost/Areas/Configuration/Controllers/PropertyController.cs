using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ziggurat.Web.Areas.Configuration.Models;

namespace Ziggurat.Web.Areas.Configuration.Controllers
{
    [Authorize]
    public class PropertyController : Controller
    {
        [HttpPost]
        public ActionResult AddNew(NewPropertyModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToArray();

                Response.StatusCode = 500;
                return Json(errors);
            }

            return Json("");
        }
    }
}
