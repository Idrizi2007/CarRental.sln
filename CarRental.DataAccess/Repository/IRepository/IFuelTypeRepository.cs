using CarRental.Models;

namespace CarRental.DataAccess.Repository.IRepository
{
    public interface IFuelTypeRepository : IRepository<FuelType>
    {
        void Update(FuelType obj);
    }
}
