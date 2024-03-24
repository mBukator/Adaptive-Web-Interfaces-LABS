using LR7.Models;

namespace LR7.Services {
    public interface IParkingSpaceService {
        Task<IEnumerable<ParkingSpace>> GetParkingSpacesAsync();
        Task<ParkingSpace> GetParkingSpaceByIdAsync(int id);
        Task CreateParkingSpaceAsync(ParkingSpace parkingSpace);
        Task UpdateParkingSpaceAsync(ParkingSpace parkingSpace);
        Task DeleteParkingSpaceAsync(int id);
    }
}
