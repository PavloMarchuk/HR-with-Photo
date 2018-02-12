using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HR.DateLayer.Repositories;
using HR.DateLayer.DbLayer;
using Newtonsoft.Json;
using HR.WebUI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.Primitives;

namespace HR.WebUI.Controllers
{
    public class PhotoController : Controller
    {
        IGenericRepository<Employee> EmployeeRep;
        IGenericRepository<Photo> PhotoRep;
        IHostingEnvironment env;
        public PhotoController(IGenericRepository<Employee> EmployeeRep,
            IGenericRepository<Photo> PhotoRep,
            IHostingEnvironment env)
        {
            this.env = env;
            this.EmployeeRep = EmployeeRep;
            this.PhotoRep = PhotoRep;
        }
        public IActionResult List(int id=1)
        {
            Employee emp = EmployeeRep.Get(id);


            ViewModelPhoto model = new ViewModelPhoto
            {
                employee = EmployeeRep.Get(id),
                JsonPhotoString = GetInitialPreview(id),
                PreviewPhotoString = GetInitialPreviewConfig(id)
            };
            ViewBag.Employee = emp.FirstName + " " + emp.LastName;
            return View(model);
        }
        public IActionResult DeleteFile()
        {
            StringValues id;
            int PhotoId;
            if (Request.Form.ContainsKey("key"))
            {
                id = Request.Form["key"];
                PhotoId = Convert.ToInt32(id);
                var photo = PhotoRep.Get(PhotoId);
                PhotoRep.Delete(photo);
                Json("Ok");
            }
            return Json("Bad");
        }
        [HttpPost]
        public IActionResult ManyFileUpload(IEnumerable<IFormFile> manyFileUpload)//
        {
            StringValues id;
            int EmployeeId = 0;
            Request.Query.TryGetValue("id", out id);
            List<Photo> listphotoid = new List<Photo>();

            if (Request.QueryString.Value != null)
                EmployeeId = Convert.ToInt32(id.FirstOrDefault());


            if (manyFileUpload != null)
            {
                foreach (var fileUpload in manyFileUpload)
                {

                    string ext = Path.GetExtension(fileUpload.FileName);
                    string filenamefull = Guid.NewGuid().ToString()  + ext;

                    Photo photo = new Photo
                    {
                        //PhotoId = 7777,
                        EmployeeId = EmployeeId,
                        PhotoPath = "..\\..\\Files\\" + filenamefull//fileUpload.FileName.Split(new char[] { '\\', '/' }).Last()

                    };
                    PhotoRep.Add(photo); //пока не сохраняю в базе данных
                    //сохраняем добавленные айдишники коллекции
                    // Чтобы потом получить их на клиенте !!!!!!!!!!!!!
                    listphotoid.Add(photo);
                    string path = "/Files/" + filenamefull;
                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(env.WebRootPath + path, FileMode.Create))
                    {
                        fileUpload.CopyTo(fileStream);
                    }
                }
            }

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
            string data = JsonConvert.SerializeObject(listphotoid, settings);
            return Json(data);
        }

        //initialPreview
        string GetInitialPreview(int id)
        {
            var photos = PhotoRep.GetAll()
                .Where(g => g.EmployeeId == id)
                .Select(p =>  p.PhotoPath);
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
            return JsonConvert.SerializeObject(photos, settings);
        }
        //initialPreviewConfig
        string GetInitialPreviewConfig(int id)
        {
            var photos = PhotoRep.GetAll()
                .Where(g => g.EmployeeId == id)
                .Select(p => new {
                    caption = p.Employee.FirstName + " " + p.Employee.LastName,
                    key = p.PhotoId
                });
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
            string result = JsonConvert.SerializeObject(photos, settings);
            return JsonConvert.SerializeObject(photos, settings);
        }




        //GetPhotoEmployee
        public IActionResult GetPhotoEmployee(int id)
        {
            var photos = PhotoRep.GetAll().Where(p => p.EmployeeId == id)
               .Select(p => p.PhotoPath).ToList();


            var path1 = photos[0];
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
            string output = JsonConvert.SerializeObject(photos, settings);
            return Json(output);
        }
    }
}