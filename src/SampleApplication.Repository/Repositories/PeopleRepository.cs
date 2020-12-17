using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SampleApplication.Repository.Models;

namespace SampleApplication.Repository
{
    public interface IPeopleRepository: IBaseRepository<Person>
    {
        Task<int> CountAllAsync();
    }

    public class PeopleRepository : BaseRepository<Person>, IPeopleRepository
    {
        public PeopleRepository(AppContext context): base(context) {}

        public async Task<int> CountAllAsync()
        {
            return await _dbSet.CountAsync().ConfigureAwait(false);
        }
    }
}
