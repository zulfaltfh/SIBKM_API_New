using API.Context;
using API.Models;
using API.Repository.Interface;

namespace API.Repository.Data
{
    public class ProfilingRepository : GeneralRepository<Profilings, string, MyContext>, IProfilingRepository
    {
        public ProfilingRepository(MyContext context) : base(context) { }

    }
}
