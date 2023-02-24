using OfficeStaff.Data.Repository.Interfaces;
using OfficeStaff.Persistence;

namespace OfficeStaff.Data.Repository
{
    public class BaseRepository : IBaseRepository
    {
        private readonly ApplicationContext _applicationContext;

        public BaseRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public bool Save()
        {
            var saved = _applicationContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}