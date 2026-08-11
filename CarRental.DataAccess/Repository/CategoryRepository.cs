using CarRental.DataAccess.Repository.IRepository;
using CarRental.Models;

namespace CarRental.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
