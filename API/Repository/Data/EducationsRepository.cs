using API.Context;
using API.Models;
using API.Repository.Interface;

namespace API.Repository.Data
{
    public class EducationsRepository : GeneralRepository<Educations, int, MyContext>, IEducationRepository
    {
        public EducationsRepository(MyContext context) : base(context) { }

    }
}
