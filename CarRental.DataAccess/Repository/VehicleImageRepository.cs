using CarRental.DataAccess.Repository.IRepository;
using CarRental.Models;

namespace CarRental.DataAccess.Repository
{
    public class VehicleImageRepository : Repository<VehicleImage>, IVehicleImageRepository
    {
        private readonly ApplicationDbContext _db;
        public VehicleImageRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
