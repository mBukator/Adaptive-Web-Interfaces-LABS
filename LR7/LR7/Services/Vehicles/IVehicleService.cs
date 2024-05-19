using LR7.Models;

namespace LR7.Services.Vehicles
{
    public interface IVehicleService
    {
        Task<IEnumerable<Vehicle>> GetVehiclesAsync();
        Task<Vehicle> GetVehicleByIdAsync(int id);
        Task CreateVehicleAsync(Vehicle vehicle);
        Task UpdateVehicleAsync(Vehicle vehicle);
        Task DeleteVehicleAsync(int id);
    }
}
