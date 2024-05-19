using LR7.Models;

namespace LR7.Services.ParkingSections
{
    public interface IParkingSectionService
    {
        Task<IEnumerable<ParkingSection>> GetParkingSectionsAsync();
        Task<ParkingSection> GetParkingSectionByIdAsync(int id);
        Task CreateParkingSectionAsync(ParkingSection parkingSection);
        Task UpdateParkingSectionAsync(ParkingSection parkingSection);
        Task DeleteParkingSectionAsync(int id);
    }
}
