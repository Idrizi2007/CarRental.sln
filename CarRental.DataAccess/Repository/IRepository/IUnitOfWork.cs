namespace CarRental.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IFuelTypeRepository FuelType { get; }

        ITransmissionTypeRepository TransmissionType { get; }

        ICategoryRepository Category { get; }

        IBrandRepository Brand { get; }

        IVehicleModelRepository VehicleModel { get; }

        IFeatureRepository Feature { get; }

        IVehicleRepository Vehicle { get; }

        void Save();
    }
}
