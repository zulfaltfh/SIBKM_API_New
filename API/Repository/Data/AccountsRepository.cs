using API.Context;
using API.Handlers;
using API.Models;
using API.Repository.Interface;
using API.ViewModels;

namespace API.Repository.Data
{
    public class AccountsRepository : GeneralRepository<Accounts, string, MyContext>, IAccountsRepository
    {
        public AccountsRepository(MyContext context) : base(context) { }

        public int Register(RegisterVM registerVM)
        {
            int result = 0;

            // Inisialisasi insert ke tabel-tabel yang terikat satu sama lain

            //insert to University table
            var university = new Universities
            {
                Name = registerVM.UniversityName
            };

            //sebelum insert, cek apakah university sudah ada atau belum
            if (_context.Universities.Any(u => u.Name.Contains(registerVM.UniversityName)))
            {
                university.Id = _context.Universities.FirstOrDefault(u => u.Name.Contains(registerVM.UniversityName))!.Id;
            }
            else
            {
                _context.Set<Universities>().Add(university);
                result = _context.SaveChanges();
            }

            //insert to Education table
            var education = new Educations
            {
                Major = registerVM.Major,
                Degree = registerVM.Degree,
                GPA = registerVM.GPA,
                UniversityId = university.Id
            };
            _context.Set<Educations>().Add(education);
            result = _context.SaveChanges();

            //insert to Employee table
            var employee = new Employee
            {
                NIK = registerVM.NIK,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                BirthDate = registerVM.BirthDate,
                Gender = registerVM.Gender,
                HiringDate = DateTime.Now,
                Email = registerVM.Email,
                PhoneNumber = registerVM.PhoneNumber
            };
            _context.Set<Employee>().Add(employee);
            result = _context.SaveChanges();

            //insert to Account table
            var account = new Accounts
            {
                EmployeeNIK = registerVM.NIK,
                Password = Hashing.HashPassword(registerVM.Password),
            };
            _context.Set<Accounts>().Add(account);
            result = _context.SaveChanges();

            //insert to Profiling table
            var profiling = new Profilings
            {
                EmployeeNIK = registerVM.NIK,
                EducationId = education.Id
            };
            _context.Set<Profilings>().Add(profiling);
            result = _context.SaveChanges();

            //insert to AccountRole table
            var accRole = new AccountRoles
            {
                AccountNIK = registerVM.NIK,
                RoleId = 1 //default as user
            };
            _context.Set<AccountRoles>().Add(accRole);
            result = _context.SaveChanges();

            return result;
        }

        public bool Login(LoginVM loginVM)
        {
            var employeeAccount = _context.Employees.Join
                                    (_context.Accounts, e => e.NIK, a => a.EmployeeNIK,
                                        (e, a) => new {
                                            Email = e.Email,
                                            Password = a.Password,
                                        }).FirstOrDefault(e => e.Email == loginVM.Email);

            if (employeeAccount == null)
            {
                return false;
            }

            return Hashing.ValidatePassword(loginVM.Password, employeeAccount.Password);
        }

    }
}
