using API.Models;
using API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using API.BaseController;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class UniversityController : GeneralController<IUniversityRepository, Universities, int>
    {
        public UniversityController(IUniversityRepository repository) : base(repository) { }
    }
}
