using Infrastructure.CsvReader;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Vehicle.Models.Models;


namespace DataContext
{
    public class DataSeed //: IDataSeed
    {
        private readonly VehicleDbContext _vehicleDbContext;
        private readonly Infrastructure.CsvReading.CsvReading.CsvFileReader _csvFileReader;
        public DataSeed(VehicleDbContext context, Infrastructure.CsvReading.CsvReading.CsvFileReader csvFile)
        {
            this._csvFileReader = csvFile;
            this._vehicleDbContext = context;

        }
        public void SeedData()
        {

            if (_vehicleDbContext.Vehicles.Any())
            {
                Console.WriteLine("Има вече данни в таблицата Vehicles.");
                return;
            }

            var vehiclesFromCsv = _csvFileReader.GetData();
            Console.WriteLine($"CSV rows loaded: {vehiclesFromCsv.Count}");

            foreach (var vehicle in vehiclesFromCsv)
            {
                var state = _vehicleDbContext.States.FirstOrDefault(s => s.StateName == vehicle.State)
                    ?? new State { StateName = vehicle.State };

                var county = _vehicleDbContext.Counties.FirstOrDefault(c => c.CountyName == vehicle.County)
                    ?? new County { CountyName = vehicle.County, State = state };

                var city = _vehicleDbContext.Cities.FirstOrDefault(c => c.CityName == vehicle.City)
                    ?? new City { CityName = vehicle.City, County = county };

                var cencusTract = _vehicleDbContext.CencusTracts
                    .FirstOrDefault(c => c.CencusTract2020 == vehicle.num2020CensusTract)
                    ?? new CencusTract { CencusTract2020 = vehicle.num2020CensusTract, City = city };

                var electricity = _vehicleDbContext.Electricities.FirstOrDefault(e =>
                    e.ElectricVehicleType == vehicle.ElectricVehicleType &&
                    e.ElectricUtility == vehicle.ElectricUtility &&
                    e.ElectricRange == vehicle.ElectricRange &&
                    e.CAFV == vehicle.CleanAlternativeFuelVehicleCAFVEligibility)
                    ?? new Electricity
                    {
                        ElectricVehicleType = vehicle.ElectricVehicleType,
                        ElectricUtility = vehicle.ElectricUtility,
                        ElectricRange = vehicle.ElectricRange,
                        CAFV = vehicle.CleanAlternativeFuelVehicleCAFVEligibility
                    };

                // Добавяне към контекста, ако не съществуват
                if (state.Id == 0) _vehicleDbContext.States.Add(state);
                if (county.Id == 0) _vehicleDbContext.Counties.Add(county);
                if (city.Id == 0) _vehicleDbContext.Cities.Add(city);
                if (cencusTract.Id == 0) _vehicleDbContext.CencusTracts.Add(cencusTract);
                if (electricity.Id == 0) _vehicleDbContext.Electricities.Add(electricity);

                // И SaveChanges() преди Vehicle
                _vehicleDbContext.SaveChanges();


                var vehcl = new Vehicle.Models.Models.Vehicle
                {
                    VIN = vehicle.VIN1_10,
                    Make = vehicle.Make,
                    Model = vehicle.VModel,
                    ModelYear = vehicle.ModelYear,
                    BaseMSRP = vehicle.BaseMSRP,
                    DOLVehicleID = vehicle.DOLVehicleID,
                    Location = vehicle.VehicleLocation,
                    PostalCode = vehicle.PostalCode,
                    State = state,
                    Electricity = electricity,
                    City = city,
                    County = county,
                    CencusTract = cencusTract,
                    CityId = city.Id,
                    CountyId = county.Id,
                    CencusTractId = cencusTract.Id
                };

                _vehicleDbContext.Vehicles.Add(vehcl);
            }

            try
            {
                _vehicleDbContext.SaveChanges();
                Console.WriteLine("Данните са успешно наляти.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ГРЕШКА при SaveChanges: " + ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
