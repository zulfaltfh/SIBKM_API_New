using API.Models;
using Client.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly EmployeeRepository repository;

        public EmployeeController(EmployeeRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var Results = await repository.Get();
            var employees = new List<Employee>();

            if (Results != null)
            {
                employees = Results.Data.ToList();
            }

            return View(employees);
        }

        /*
         -- create
         -- untuk httpget alias untuk menampilkan tampilan form
         */
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /*
         -- create
         -- HttpPost untuk mengirimkan data yang diinputkan di form ke dalam database
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            var result = await repository.Post(employee);
            if (result.Code == 200)
            {
                TempData["Success"] = "Data berhasil masuk";
                return RedirectToAction(nameof(Index));
            }
            else if (result.Code == 409)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }

            return View();
        }

        /*
         -- details - get by id
         */
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            //localhost/university/
            var Results = await repository.Get(id);
            var employee = Results.Data;

            //if (Results != null)
            //{
            //    employee = Results.Data;
            //}

            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var Results = await repository.Get(id);
            var employee = new Employee();

            if (Results.Data?.NIK is null)
            {
                return View(employee);
            }
            else
            {
                employee.NIK = Results.Data.NIK;
                employee.FirstName = Results.Data.FirstName;
                employee.LastName = Results.Data.LastName;
                employee.BirthDate = Results.Data.BirthDate;
                employee.Gender = Results.Data.Gender;
                employee.HiringDate = Results.Data.HiringDate;
                employee.Email = Results.Data.Email;
                employee.PhoneNumber = Results.Data.PhoneNumber;
            }
            return View(employee);
        }


        /*
         -- edit
         -- HttpPost 
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                var result = await repository.Put(employee.NIK, employee);
                if (result.Code == 200)
                {
                    return RedirectToAction(nameof(Index));
                }
                else if (result.Code == 409)
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View();
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await repository.Get(id);
            var university = result?.Data;

            return View(university);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(string id)
        {
            var result = await repository.Delete(id);
            if (result.Code == 200)
            {
                TempData["Success"] = "Data berhasil dihapus";
                return RedirectToAction(nameof(Index));
            }

            var employee = await repository.Get(id);
            return View("Delete", employee?.Data);
        }
    }
}
