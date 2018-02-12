using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HR.DateLayer.DbLayer;

namespace HR.WebUI.Controllers
{
    public class JobsController : Controller
    {
        HumanResourcesContext context;
        public JobsController(HumanResourcesContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var model = context.JobTitle;
            return View(model);
        }
    }
}