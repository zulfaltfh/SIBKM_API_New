using API.Models;
using API.Repository.Interface;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using API.BaseController;
using API.Handlers;
using API.Repository.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "admin")] //digunakan untuk pembatasan interaksi pada tabel (dalam kasus ini, bergantung pada role user)
    public class AccountsController : GeneralController<IAccountsRepository, Accounts, string>
    {
        private readonly ITokenService _tokenService;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAccountRolesRepository _accountRolesRepository;


        public AccountsController(
            IAccountsRepository accountsRepository, 
            ITokenService tokenService,
            IEmployeeRepository employeeRepository,
            IAccountRolesRepository accountRolesRepository) : base(accountsRepository)
        {
            _tokenService = tokenService;
            _employeeRepository = employeeRepository;
            _accountRolesRepository = accountRolesRepository;

        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult Login(LoginVM loginVM)
        {
            var login = _repository.Login(loginVM);
            if (!login)
            {
                return NotFound(new ResponseErrorsVM<string>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Errors = "Login Failed, Account or Password Not Found!"
                });
            }

            var claims = new List<Claim>()
            {
                new Claim("Email", loginVM.Email),
                new Claim("FullName", _employeeRepository.GetFullNameByEmail(loginVM.Email))
            };

            var getRoles = _accountRolesRepository.GetRolesByEmail(loginVM.Email);
            foreach (var role in getRoles) 
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = _tokenService.GenerateToken(claims);

            return Ok(new ResponseDataVM<string>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Login Success",
                Data = token
            });

        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public ActionResult Register(RegisterVM registerVM)
        {
            var register = _repository.Register(registerVM);
            if (register > 0)
            {
                return Ok(new ResponseDataVM<string>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Insert Success"
                });
            }

            return BadRequest(new ResponseErrorsVM<string>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Errors = "Insert Failed / Lost Connection"
            });
        }
    }
}
