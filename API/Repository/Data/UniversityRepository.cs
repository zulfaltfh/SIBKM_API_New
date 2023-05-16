using API.Context;
using API.Models;
using API.Repository.Interface;

namespace API.Repository.Data
{
    public class UniversityRepository : GeneralRepository<Universities, int, MyContext>, IUniversityRepository
    {
        public UniversityRepository(MyContext context) : base(context) { }
    }
}
