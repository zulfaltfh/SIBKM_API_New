using API.Models;
using Client.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Client.Controllers
{
    public class UniversityController : Controller
    {
        private readonly UniversityRepository repository;

        public UniversityController(UniversityRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var Results = await repository.Get();
            var universities = new List<Universities>();

            if(Results != null)
            {
                universities = Results.Data.ToList();
            }

            return View(universities);
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
        public async Task<IActionResult> Create(Universities universities)
        {
            var result = await repository.Post(universities);
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
        public async Task<IActionResult> Details(int id)
        {
            //localhost/university/
            var Results = await repository.Get(id);
            //var universities = new University();

            //if (Results != null)
            //{
            //    universities = Results.Data;
            //}

            return View(Results.Data);
        }

        /*
         -- edit
         -- HttpGet untuk mengambil id 
         */
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var Results = await repository.Get(id);
            var universities = new Universities();

            if (Results.Data?.Id is null)
            {
                return View(universities);
            }
            else
            {
                universities.Id = Results.Data.Id;
                universities.Name = Results.Data.Name;
            }
            return View(universities);
        }


        /*
         -- edit
         -- HttpPost 
         */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Universities universities)
        {
            if(ModelState.IsValid)
            {
                var result = await repository.Put(universities.Id, universities);
                if (result.Code == 200)
                {
                    return RedirectToAction(nameof(Index));
                }
                else if (result.Code == 500)
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View();
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await repository.Get(id);
            var university = result?.Data;

            return View(university);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var result = await repository.Delete(id);
            if (result.Code == 200)
            {
                TempData["Success"] = "Data berhasil dihapus";
                return RedirectToAction(nameof(Index));
            }

            var university = await repository.Get(id);
            return View("Delete", university?.Data);
        }
    }
}
