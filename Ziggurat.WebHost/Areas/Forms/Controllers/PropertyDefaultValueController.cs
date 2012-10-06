using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ziggurat.Infrastructure;
using Ziggurat.Messages.Client;

namespace Ziggurat.Web.Areas.Forms.Controllers
{
    public sealed class PropertyDefaultValueController : Controller
    {
        private readonly IViewModelReader _viewModelReader;
        private readonly ICommandSender _commandSender;

        public PropertyDefaultValueController(IViewModelReader reader, ICommandSender sender)
        {
            _viewModelReader = reader;
            _commandSender = sender;
        }

        public ActionResult ShowValue(Guid formId, Guid propertyId)
        {
            var values = _viewModelReader.Load<Guid, FormDefaultValues>(formId);
            var defaultValue = values.Values[propertyId];

            return View(defaultValue);
        }
    }
}