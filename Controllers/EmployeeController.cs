using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Employee.Controllers
{
    public class EmployeeController : Controller
    {

        public ActionResult create()
        {
            return View();
        }

       
        [HttpPost]
        public ActionResult create(Employee model)
        {

            
            using (var context = new tempdbEntities())
            {
                
                context.Employees.Add(model);

                
                context.SaveChanges();
            }
            string message = "Created the record successfully";

           
            ViewBag.Message = message;

            
            return View();
        }

        [HttpGet] // Set the attribute to Read
        public ActionResult Read()
        {
            using (var context = new tempdbEntities())
            {
                var data = context.Employees.ToList(); // Return the list of data from the database
                return View(data);
            }

        }

        public ActionResult Update(int employee_id) // To fill data in the form to enable easy editing
        {
            using (var context = new tempdbEntities())
            {
                var data = context.Employees.Where(x => x.employee_id == employee_id).SingleOrDefault();
                return View(data);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken] // To specify that this will be invoked when post method is called
        public ActionResult Update(int employee_id, Employee model)
        {
            using (var context = new tempdbEntities())
            {
                var data = context.Employees.FirstOrDefault(x => x.employee_id == employee_id); // Use of lambda expression to access particular record from a database
                if (data != null) // Checking if any such record exist 
                {
                    data.Name = model.Name;
                    data.Name = model.Name;
                    data.Department = model.Department;
                    data.Salary = model.Salary;
                    context.SaveChanges();
                    return RedirectToAction("Read"); // It will redirect to the Read method
                }
                else
                    return View();
            }
        }


        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int employee_id)
        {
            using (var context = new tempdbEntities())
            {
                var data = context.Employees.FirstOrDefault(x => x.employee_id == employee_id);
                if (data != null)
                {
                    context.Employees.Remove(data);
                    context.SaveChanges();
                    return RedirectToAction("Read");
                }
                else
                    return View();
            }
        }

    }
}

