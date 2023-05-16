using API.Context;
using API.Models;
using API.Repository.Interface;

namespace API.Repository.Data
{
    public class RolesRepository : GeneralRepository<Roles, int, MyContext>, IRolesRepository
    {
        public RolesRepository(MyContext context) : base(context) { }
    }
}
