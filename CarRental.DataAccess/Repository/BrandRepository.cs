using CarRental.DataAccess.Repository.IRepository;
using CarRental.Models;

namespace CarRental.DataAccess.Repository
{
    public class BrandRepository : Repository<Brand>, IBrandRepository

    {
        private readonly ApplicationDbContext _db;
        public BrandRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
