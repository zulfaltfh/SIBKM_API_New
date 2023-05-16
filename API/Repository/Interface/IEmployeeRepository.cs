using API.Models;

namespace API.Repository.Interface
{
    public interface IEmployeeRepository : IGeneralRepository<Employee, string>
    {
        string GetFullNameByEmail(string email);
    }
}
