using CarRental.DataAccess.Repository.IRepository;
using CarRental.Models;

namespace CarRental.DataAccess.Repository
{
    public class FuelTypeRepository : Repository<FuelType>, IFuelTypeRepository
    {
        public FuelTypeRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
