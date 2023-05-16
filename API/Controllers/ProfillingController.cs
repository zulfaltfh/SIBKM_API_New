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
    public class ProfillingController : GeneralController<IProfilingRepository, Profilings, string>
    {
        public ProfillingController(IProfilingRepository profilingRepository) : base(profilingRepository) { }
    }
}
