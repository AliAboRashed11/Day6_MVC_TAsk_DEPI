using Day2.Models;
using Day2.viewsModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Day2.Controllers
{
    public class GetDataController : Controller
    {
		//GetData/GetallInstId
		DBContextCourse course = new DBContextCourse();
        public IActionResult Index()
        {
            return View();
        }
		[HttpGet]
		public IActionResult Create()
        {
			var Departments = course.Departments.Select(a => new DepartmentVm {id= a.Id, name=a.name }).ToList();

			return View("create", Departments);
        }
        [HttpPost]
        public IActionResult create(Instructor instructors)
        {
            if (instructors != null)
            {
                course.Add(instructors);
                course.SaveChanges();
                return RedirectToAction("GetallInstructor");

            }
            return View();
        }
        public IActionResult GetallInstructor()
        {
            ViewData["Name"] = "Instructor";

			var Instructors = course.Instructors.Include(a => a.Course).Include(y => y.Department).ToList();
            return View("GetallInstructor", Instructors);
        }

        public IActionResult Edit(int id)
        {
            InstractorDeplistVM instractorDeplistVM = new InstractorDeplistVM();

            var instrctor = course.Instructors.FirstOrDefault(a=>a.Id == id);
            instrctor.Imageurl = string.Empty;
            instractorDeplistVM.Name = instrctor.Name;
            instractorDeplistVM.Id = instrctor.Id;
            instractorDeplistVM.Address = instrctor.Address;
            instractorDeplistVM.DepartmentID = instrctor.DepartmentID;
            instractorDeplistVM.CourseID = instrctor.CourseID ;
            instractorDeplistVM.Imageurl= instrctor.Imageurl;
            instractorDeplistVM.DepartmentVm = course.Departments.Select(a=> new DepartmentVm {id= a.Id ,name=a.name}).ToList();

            return View(instractorDeplistVM);
        }
        [HttpPost]
        public IActionResult Edit(int id ,Instructor instructor )
        {
            if(instructor!=null && id == instructor.Id) {course.Update(instructor);

                course.SaveChanges();
                return RedirectToAction("GetallInstructor");
            }
            return View();
        }

        public IActionResult Delete(int id) { 
        
            var inst = course.Instructors.FirstOrDefault(a=> a.Id ==id);
            if(inst!=null && id == inst.Id)
            {
                course.Remove(inst);
                course.SaveChanges();
                return RedirectToAction("GetallInstructor");
            }
            return View("GetallInstructor");
        }
        public IActionResult GetallTran()
        {
            ViewData["Name"] = "Ali";
            var Trainees = course.Trainees.Include(y => y.Department).ToList();
            return View("GetallTran", Trainees);
        }




        public IActionResult GetallInstId(int id)
        {
            ViewData["Name"] = "Instructor";
            var Instractor = course.Instructors.Include(a => a.Course).Include(y => y.Department).FirstOrDefault(a => a.Id == id);
            return View("GetallInstId", Instractor);
        }


        public IActionResult deleteId(int id)
        {


            var Instractor = course.Instructors.FirstOrDefault(a => a.Id==id);
            course.Instructors.Remove(Instractor);
            course.SaveChanges();

            return Content(" Instructor has deleted ");
        }

        public IActionResult AddInstructor(Instructor instructor)
        {


            course.Instructors.Add(instructor);

            course.SaveChanges();


            return Content(" Hellow worled ");
        }
        public IActionResult UpdateInstructor(int id)
        {

            var instructor = course.Instructors.Include(x => x.Course).Include(x => x.Department).FirstOrDefault(x => x.Id == id);

            course.SaveChanges();


            return Content(" Instructor has updated ");
        }

    }
}
