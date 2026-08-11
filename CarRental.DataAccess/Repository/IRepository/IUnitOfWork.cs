namespace CarRental.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IFuelTypeRepository FuelType { get; }
        ITransmissionTypeRepository TransmissionType { get; }
        ICategoryRepository Category { get; }

        void Save();
    }
}
