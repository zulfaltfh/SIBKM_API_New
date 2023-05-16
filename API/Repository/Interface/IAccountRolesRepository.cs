using API.Models;

namespace API.Repository.Interface
{
    public interface IAccountRolesRepository : IGeneralRepository<AccountRoles, int>
    {
        IEnumerable<string> GetRolesByEmail(string email);
    }
}
