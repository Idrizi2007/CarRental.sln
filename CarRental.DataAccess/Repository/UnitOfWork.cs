using CarRental.DataAccess.Repository.IRepository;

namespace CarRental.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IFuelTypeRepository FuelType { get; private set; }

        public ITransmissionTypeRepository TransmissionType { get; private set; }

        public ICategoryRepository Category { get; private set; }

        public IBrandRepository Brand { get; private set; }

        public IVehicleModelRepository VehicleModel { get; private set; }

        public IFeatureRepository Feature { get; private set; }

        public IVehicleRepository Vehicle { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            FuelType = new FuelTypeRepository(_db);
            TransmissionType = new TransmissionTypeRepository(_db);
            Category = new CategoryRepository(_db);
            Brand = new BrandRepository(_db);
            VehicleModel = new VehicleModelRepository(_db);
            Feature = new FeatureRepository(_db);
            Vehicle = new VehicleRepository(_db);
        }



        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
