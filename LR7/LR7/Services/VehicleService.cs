using LR7.Models;
using System.Reflection;
using System;

namespace LR7.Services {
    public class VehicleService : IVehicleService {
        private static readonly List<Vehicle> _vehicles = new List<Vehicle> {
            new Vehicle { Id = 1, Brand = "Toyota", Model = "Corolla", IsParked = false },
            new Vehicle { Id = 2, Brand = "Honda", Model = "Accord", IsParked = true },
            new Vehicle { Id = 3, Brand = "Mercedes", Model = "E-Class", IsParked = false },
            new Vehicle { Id = 4, Brand = "BMW", Model = "3 Series", IsParked = true },
            new Vehicle { Id = 5, Brand = "Volkswagen", Model = "Jetta", IsParked = false },
            new Vehicle { Id = 6, Brand = "Hyundai", Model = "Elantra", IsParked = true },
            new Vehicle { Id = 7, Brand = "Ferrari", Model = "458 Italia", IsParked = false },
            new Vehicle { Id = 8, Brand = "Porsche", Model = "911", IsParked = true },
            new Vehicle { Id = 9, Brand = "Lamborghini", Model = "Huracan", IsParked = false },
            new Vehicle { Id = 10, Brand = "Renault", Model = "Megane", IsParked = true },
        };

        public async Task<IEnumerable<Vehicle>> GetVehiclesAsync() {
            try {
                return await Task.FromResult(_vehicles);
            } catch (Exception ex) {
                Console.WriteLine($"Error getting vehicle. Exception: {ex.Message}");
                return null;
            }
        }

        public async Task<Vehicle> GetVehicleByIdAsync(int id) {
            try {
                return await Task.FromResult(_vehicles.FirstOrDefault(x => x.Id == id));
            } catch (Exception ex) {
                Console.WriteLine($"Error getting vehicle. Exception: {ex.Message}");
                return null;
            }
        }

        public async Task CreateVehicleAsync(Vehicle vehicle) {
            try {
                _vehicles.Add(vehicle);
                await Task.CompletedTask;
            } catch (Exception ex) {
                Console.WriteLine($"Error creating vehicle. Exception: {ex.Message}");
            }
        }

        public async Task UpdateVehicleAsync(Vehicle vehicle) {
            try {
                var existingVehicle = _vehicles.FirstOrDefault(x => x.Id == vehicle.Id);
                if (existingVehicle != null) {
                    existingVehicle.Brand = vehicle.Brand;
                    existingVehicle.Model = vehicle.Model;
                    existingVehicle.IsParked = vehicle.IsParked;
                }
                await Task.CompletedTask;
            } catch (Exception ex) {
                Console.WriteLine($"Error updating vehicle. Exception: {ex.Message}");
            }
        }

        public async Task DeleteVehicleAsync(int id) {
            try {
                var vehicle = _vehicles.FirstOrDefault(x => x.Id == id);
                if (vehicle != null) {
                    _vehicles.Remove(vehicle);
                }
                await Task.CompletedTask;
            } catch (Exception ex) {
                Console.WriteLine($"Error deleting vehicle. Exception: {ex.Message}");
            }
        }
    }
}
