using LR7.Models;

namespace LR7.Services {
    public class ParkingSectionService : IParkingSectionService {
        private static readonly List<ParkingSection> _parkingSection = new List<ParkingSection> {
            new ParkingSection { Id = 1, Name = "A1", Capacity = 5 },
            new ParkingSection { Id = 2, Name = "A2", Capacity = 5 },
            new ParkingSection { Id = 3, Name = "A3", Capacity= 5 },
            new ParkingSection { Id = 4, Name = "B1", Capacity = 10 },
            new ParkingSection { Id = 5, Name = "B2", Capacity = 10 },
            new ParkingSection { Id = 6, Name = "B3", Capacity = 10 },
            new ParkingSection { Id = 7, Name = "C1", Capacity = 3 },
            new ParkingSection { Id = 8, Name = "C2", Capacity = 3 },
            new ParkingSection { Id = 9, Name = "C3", Capacity = 3 },
            new ParkingSection { Id = 10, Name = "D1", Capacity = 3 }
        };

        public async Task<IEnumerable<ParkingSection>> GetParkingSectionsAsync() {
            try {
                return await Task.FromResult(_parkingSection);
            } catch (Exception ex) {
                Console.WriteLine($"Error getting parking section: {ex.Message}");
                return null;
            }
        }

        public async Task<ParkingSection> GetParkingSectionByIdAsync(int id) {
            try {
                return await Task.FromResult(_parkingSection.FirstOrDefault(ps => ps.Id == id));
            } catch (Exception ex) {
                Console.WriteLine($"Error getting parking section by ID: {id}. Exception: {ex.Message}");
                return null;
            }
        }

        public async Task CreateParkingSectionAsync(ParkingSection parkingSection) {
            try {
                _parkingSection.Add(parkingSection);
                await Task.CompletedTask;
            } catch (Exception ex) {
                Console.WriteLine($"Error creating parking section: {parkingSection.Name}. Exception: {ex.Message}");
            }
        }
       
        public async Task UpdateParkingSectionAsync(ParkingSection parkingSection) {
            try {
                var existingSection = _parkingSection.FirstOrDefault(ps => ps.Id == parkingSection.Id);
                if (existingSection != null) {
                    existingSection.Id = parkingSection.Id;
                    existingSection.Name = parkingSection.Name;
                    existingSection.Capacity = parkingSection.Capacity;
                }
                await Task.CompletedTask;
            } catch (Exception ex) {
                Console.WriteLine($"Error updating parking section: {ex.Message}");
            }
        }

        public async Task DeleteParkingSectionAsync(int id) {
            try {
                var existingSection = _parkingSection.FirstOrDefault(ps => ps.Id == id);
                if (existingSection != null) {
                    _parkingSection.Remove(existingSection);
                }
                await Task.CompletedTask;
            } catch (Exception ex) {
                Console.WriteLine($"Error deleting parking section: {ex.Message}");
            }
        }
    }
}
