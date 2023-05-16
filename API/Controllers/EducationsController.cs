using API.Models;
using API.Repository.Data;
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
    public class EducationsController : GeneralController<IEducationRepository, Educations, int>
    {
        public EducationsController(IEducationRepository educationRepository) : base(educationRepository) { }
    }
}
