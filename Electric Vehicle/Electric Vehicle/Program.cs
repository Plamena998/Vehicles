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
            using (var context = new DataContext.VehicleDbContext())
            {
                CsvFileReader csv = new CsvFileReader();
                csv.GetData();                        
                Console.WriteLine("DA");
                var dataSeed = new DataContext.DataSeed(context, csv); 
                dataSeed.SeedData();
                Console.WriteLine("DA");
            }

        }
    }
}
