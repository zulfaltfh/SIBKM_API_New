using API.Models;
using API.Repository.Interface;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using API.BaseController;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : GeneralController<IEmployeeRepository, Employee, string>
    {
        public EmployeeController(IEmployeeRepository repository) : base(repository) { }

    }
}
