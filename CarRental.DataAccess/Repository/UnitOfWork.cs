using CarRental.DataAccess.Repository.IRepository;

namespace CarRental.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IFuelTypeRepository FuelType { get; private set; }



        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            FuelType = new FuelTypeRepository(_db);
        }



        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
