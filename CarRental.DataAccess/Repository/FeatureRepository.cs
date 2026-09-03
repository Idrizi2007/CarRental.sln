using CarRental.DataAccess.Repository.IRepository;
using CarRental.Models;

namespace CarRental.DataAccess.Repository
{
    public class FeatureRepository : Repository<Feature>, IFeatureRepository
    {
        public FeatureRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
