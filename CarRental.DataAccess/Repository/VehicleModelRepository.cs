using CarRental.DataAccess.Repository.IRepository;
using CarRental.Models;

namespace CarRental.DataAccess.Repository
{
    public class VehicleModelRepository : Repository<VehicleModel>, IVehicleModelRepository
    {
        private readonly ApplicationDbContext _db;
        public VehicleModelRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
