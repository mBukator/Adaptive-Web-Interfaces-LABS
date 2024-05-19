using LR7.Models;

namespace LR7.Services.ParkingSpaces
{
    public class ParkingSpaceService : IParkingSpaceService
    {
        private static readonly List<ParkingSpace> _parkingSpaces = new List<ParkingSpace> {
            new ParkingSpace { Id = 1, Number = 1, IsOccupied = true },
            new ParkingSpace { Id = 2, Number = 2, IsOccupied = true },
            new ParkingSpace { Id = 3, Number = 3, IsOccupied = false },
            new ParkingSpace { Id = 4, Number = 4, IsOccupied = false },
            new ParkingSpace { Id = 5, Number = 5, IsOccupied = true },
            new ParkingSpace { Id = 6, Number = 6, IsOccupied = true },
            new ParkingSpace { Id = 7, Number = 7, IsOccupied = false },
            new ParkingSpace { Id = 8, Number = 8, IsOccupied = false },
            new ParkingSpace { Id = 9, Number = 9, IsOccupied = true },
            new ParkingSpace { Id = 10, Number = 10, IsOccupied = false },
        };

        public async Task<IEnumerable<ParkingSpace>> GetParkingSpacesAsync()
        {
            try
            {
                return await Task.FromResult(_parkingSpaces);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting parking space. Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<ParkingSpace> GetParkingSpaceByIdAsync(int id)
        {
            try
            {
                return await Task.FromResult(_parkingSpaces.FirstOrDefault(p_sp => p_sp.Id == id));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting parking space by id = {id}. Exception: {ex.Message}");
                return null;
            }
        }

        public async Task CreateParkingSpaceAsync(ParkingSpace parkingSpace)
        {
            try
            {
                _parkingSpaces.Add(parkingSpace);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating parking space. Exception: {ex.Message}");
            }
        }

        public async Task UpdateParkingSpaceAsync(ParkingSpace parkingSpace)
        {
            try
            {
                var existingSpace = _parkingSpaces.FirstOrDefault(p_sp => p_sp.Id == parkingSpace.Id);
                if (existingSpace != null)
                {
                    existingSpace.Id = parkingSpace.Id;
                    existingSpace.Number = parkingSpace.Number;
                    existingSpace.IsOccupied = parkingSpace.IsOccupied;
                }
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating parking space. Exception: {ex.Message}");
            }
        }

        public async Task DeleteParkingSpaceAsync(int id)
        {
            try
            {
                var parkingSpace = _parkingSpaces.FirstOrDefault(p_sp => p_sp.Id == id);
                if (parkingSpace != null)
                {
                    _parkingSpaces.Remove(parkingSpace);
                }
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting parking space. Exception: {ex.Message}");
            }
        }
    }
}
