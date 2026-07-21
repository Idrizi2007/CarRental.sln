namespace CarRental.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IFuelTypeRepository FuelType { get; }

        void Save();
    }
}
