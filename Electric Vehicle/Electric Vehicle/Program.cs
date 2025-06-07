using DataContext;
using Infrastructure.CsvReader;
using Infrastructure.CsvReading.CsvReading;
using Infrastructure.GlobalParams;
using System.Data;

namespace ElectricVehicle
{
    public class Program
    {
        static void Main(string[] args)
        {
            using(var context = new VehicleDbContext())
            {
                CsvFileReader csvFileReader = new CsvFileReader();
                csvFileReader.GetData();
                var dataSeed = new DataSeed(csvFileReader, context);
                dataSeed.SeedData();
                Console.WriteLine("gotovo");
            }
            

        }
    }
}
