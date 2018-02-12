using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HR.DateLayer.DbLayer;
using HR.DateLayer.Repositories;
using HR.WebUI.Models;

namespace HR.WebUI.Controllers
{
    public class EmployeeRController : Controller
    {
        IGenericRepository<Employee> repository;
        public EmployeeRController(IGenericRepository<Employee> repository)
        {
            this.repository = repository;
        }
        public IActionResult Index()
        {
            var model = repository.GetAll();
            return View(model);
        }
        public IActionResult Filter()
        {
            var model = new EmployeeFilter(); ;
            return View(model);
        }
        //[HttpPost]
        public IActionResult GetData(EmployeeFilter filter)
        {
            var model = repository.FindBy(filter.predicate);
            return PartialView(model);
        }
        public IActionResult Edit(int id)
        {
            var model = repository.Get(id);
            return View(model);
        }
        public ActionResult Delete(int id)
        {
            Employee employee = repository.Get(id);
            repository.Delete(employee);
            return RedirectToAction("Index");
        }
        public IActionResult Details(int id)
        {
            return View(repository.Get(id));
        }
    }
}