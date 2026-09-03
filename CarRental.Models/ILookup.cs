namespace CarRental.Models
{

    public interface ILookup
    {
        int Id { get; }
        string Name { get; }
        bool IsActive { get; }
    }
}
