using CarRental.DataAccess.Repository.IRepository;
using CarRental.Models;

namespace CarRental.DataAccess.Repository
{
    public class FuelTypeRepository : Repository<FuelType>, IFuelTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public FuelTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(FuelType obj)
        {
            _db.FuelTypes.Update(obj);
        }
    }
}
