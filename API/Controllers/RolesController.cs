using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.BaseController;
using API.Models;
using API.Repository.Interface;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : GeneralController<IRolesRepository, Roles, int>
    {
        public RolesController(IRolesRepository rolesRepository) : base(rolesRepository) { }
    }
}
