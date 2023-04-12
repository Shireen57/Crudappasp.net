using CRUDAPP2.Data;
using CRUDAPP2.Models;
using CRUDAPP2.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPP2.Controllers
{
    public class EmployeesController : Controller
    {
        public EmployeesController(MVCDemoDBContext mVCDemoDBContext)
        {
            MVCDemoDBContext = mVCDemoDBContext;
        }
        
        public MVCDemoDBContext MVCDemoDBContext { get; }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
        var employee=    await MVCDemoDBContext.Employees.ToListAsync();
            return View(employee);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        //toinseert into database
        public async Task<IActionResult> Add(AddEmployee addemployee) {
            //to assign domain into addEmployeeModel
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addemployee.Name,
                Email = addemployee.Email,
                Salery = addemployee.Salery,
                Department = addemployee.Department,
                DOB = addemployee.DOB,
            };
            //Now save into Database
          await   MVCDemoDBContext.Employees.AddAsync(employee);
         await   MVCDemoDBContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]

        public async Task<IActionResult> View(Guid id)
        {
            var employee =await  MVCDemoDBContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if(employee!=null)
            {
                var viewmodel = new update()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salery = employee.Salery,
                    Department = employee.Department,
                    DOB = employee.DOB,
                };
                return await Task.Run(() => View("View", viewmodel));
            }
            return RedirectToAction("Index");
            
        }
        [HttpPost]
        public async Task<IActionResult> View(update model)
        {
            var employee = await MVCDemoDBContext.Employees.ForEachAsync(model.Id);
            if(employee!=null)
            {
                employee.Name = model.Name; 
                employee.Email=model.Email;
                employee.Salery = model.Salery;
                employee.dB = model.DOB;
                employee.Department = model.Department;
              await  MVCDemoDBContext.SaveChangesAsync();
                return RedirectToAction("Index");

            }
        }
    }
}
