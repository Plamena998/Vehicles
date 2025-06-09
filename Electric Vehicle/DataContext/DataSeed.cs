using Infrastructure.CsvReader;
using Infrastructure.CsvReading.CsvReading;
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
    public class DataSeed : IDataSeed
    {
        private readonly VehicleDbContext _vehicleDbContext;
        private readonly CsvFileReader _csvFileReader;

        public DataSeed(CsvFileReader csvFile, VehicleDbContext context)
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
            Console.WriteLine($"{vehiclesFromCsv.Count}");

            Dictionary<string, State> states = new Dictionary<string, State>();
            Dictionary<string, County> counties = new Dictionary<string, County>();
            Dictionary<string, City> cities = new Dictionary<string, City>();
            Dictionary<string, Electricity> electricities = new Dictionary<string, Electricity>();
            Dictionary<int, Vehicle.Models.Models.Vehicle> vehicles = new Dictionary<int, Vehicle.Models.Models.Vehicle>();
            List<CencusTract> cencusList = new List<CencusTract>();
            
            for (int i = 0; vehiclesFromCsv.Count > i; i++)
            {
                if (!states.ContainsKey(vehiclesFromCsv[i].State))
                {
                    State state = new State()
                    {
                        StateName = vehiclesFromCsv[i].State
                    };
                    states.Add(vehiclesFromCsv[i].State, state);
                }
                if (!counties.ContainsKey(vehiclesFromCsv[i].County))
                {
                    County county = new County()
                    {
                        CountyName = vehiclesFromCsv[i].County,
                        State = states[vehiclesFromCsv[i].State]
                    };
                    counties.Add(vehiclesFromCsv[i].County, county);
                }
                if (!cities.ContainsKey(vehiclesFromCsv[i].City))
                {
                    City city = new City()
                    {
                        CityName = vehiclesFromCsv[i].City,
                        County = counties[vehiclesFromCsv[i].County]
                    };
                    cities.Add(vehiclesFromCsv[i].City, city);
                }

                CencusTract census = new CencusTract()
                {
                    CensusTract2020 = vehiclesFromCsv[i].num2020CensusTract,
                    City = cities[vehiclesFromCsv[i].City]
                };
                cencusList.Add(census);
               

                if (!electricities.ContainsKey(vehiclesFromCsv[i].ElectricVehicleType))
                {
                    Electricity electricity = new Electricity()
                    {
                        ElectricVehicleType = vehiclesFromCsv[i].ElectricVehicleType,
                        ElectricUtility = vehiclesFromCsv[i].ElectricUtility,
                        ElectricRange = vehiclesFromCsv[i].ElectricRange,
                        CAFV = vehiclesFromCsv[i].CleanAlternativeFuelVehicleCAFVEligibility,
                        LegislativeDistrict = vehiclesFromCsv[i].LegislativeDistrict,
                    };
                    electricities.Add(vehiclesFromCsv[i].ElectricVehicleType, electricity);
                }
                if (!vehicles.ContainsKey(vehiclesFromCsv[i].DOLVehicleID))
                {
                    Vehicle.Models.Models.Vehicle vehicle = new Vehicle.Models.Models.Vehicle()
                    {
                        VIN = vehiclesFromCsv[i].VIN1_10,
                        Make = vehiclesFromCsv[i].Make,
                        Model = vehiclesFromCsv[i].VModel,
                        ModelYear = vehiclesFromCsv[i].ModelYear,
                        BaseMSRP = vehiclesFromCsv[i].BaseMSRP,
                        DOLVehicleID = vehiclesFromCsv[i].DOLVehicleID,
                        Location = vehiclesFromCsv[i].VehicleLocation,
                        PostalCode = vehiclesFromCsv[i].PostalCode,
                        Electricity = electricities[vehiclesFromCsv[i].ElectricVehicleType],
                        State = states[vehiclesFromCsv[i].State],
                        City = cities[vehiclesFromCsv[i].City],
                        County = counties[vehiclesFromCsv[i].County],
                        CencusTract = census,

                    };
                    vehicles.Add(vehiclesFromCsv[i].DOLVehicleID, vehicle);
                    
                }

            }
            _vehicleDbContext.AddRange(states.Values);
            _vehicleDbContext.AddRange(counties.Values);
            _vehicleDbContext.AddRange(cities.Values);
            _vehicleDbContext.AddRange(cencusList);
            _vehicleDbContext.AddRange(electricities.Values);
            _vehicleDbContext.AddRange(vehicles.Values);
            _vehicleDbContext.SaveChanges();
        }
    }
}