using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.BaseController;
using API.Models;
using API.Repository.Interface;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountRolesController : GeneralController<IAccountRolesRepository, AccountRoles, int>
    {
        public AccountRolesController(IAccountRolesRepository repository) : base(repository) { }
    }
}
