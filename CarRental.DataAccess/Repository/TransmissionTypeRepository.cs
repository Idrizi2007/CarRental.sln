using CarRental.DataAccess.Repository.IRepository;
using CarRental.Models;

namespace CarRental.DataAccess.Repository
{
    public class TransmissionTypeRepository : Repository<TransmissionType>, ITransmissionTypeRepository
    {
        public TransmissionTypeRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
