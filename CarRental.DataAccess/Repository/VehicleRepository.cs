using CarRental.DataAccess.Repository.IRepository;
using CarRental.Models;
namespace CarRental.DataAccess.Repository
{
    public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {
        private readonly ApplicationDbContext _db;
        public VehicleRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public override void Update(Vehicle obj)
        {
            var vehicleFromDb = _db.Vehicles.FirstOrDefault(u => u.Id == obj.Id);
            if (vehicleFromDb != null)
            {
                vehicleFromDb.RegistrationPlate = obj.RegistrationPlate;

                vehicleFromDb.VehicleModelId = obj.VehicleModelId;
                vehicleFromDb.CategoryId = obj.CategoryId;
                vehicleFromDb.FuelTypeId = obj.FuelTypeId;
                vehicleFromDb.TransmissionTypeId = obj.TransmissionTypeId;

                vehicleFromDb.Year = obj.Year;
                vehicleFromDb.Color = obj.Color;
                vehicleFromDb.Seats = obj.Seats;
                vehicleFromDb.Doors = obj.Doors;
                vehicleFromDb.EngineSize = obj.EngineSize;
                vehicleFromDb.Mileage = obj.Mileage;
                vehicleFromDb.Description = obj.Description;

                vehicleFromDb.IsForRent = obj.IsForRent;
                vehicleFromDb.RentalPricePerDay = obj.RentalPricePerDay;
                vehicleFromDb.IsForSale = obj.IsForSale;
                vehicleFromDb.SalePrice = obj.SalePrice;

                vehicleFromDb.IsActive = obj.IsActive;
            }


        }
    }
}
