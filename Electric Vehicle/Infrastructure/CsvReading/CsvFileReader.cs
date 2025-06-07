
using CsvHelper;
using Infrastructure.CsvReader;
using System.Globalization;

namespace Infrastructure.CsvReading.CsvReading

{
    public class CsvFileReader : ICsvFileReader
    {
        public List<Model> GetData()
        {
            using (var reader = new StreamReader(GlobalParams.GlobalParams.FileDir))
            using (var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<ModelClassMap>();
                var vehicles = csv.GetRecords<Model>()
                    .Where(m => !string.IsNullOrWhiteSpace(m.VIN1_10))
                    .ToList();
                return vehicles;
            }

        }
    }
}
