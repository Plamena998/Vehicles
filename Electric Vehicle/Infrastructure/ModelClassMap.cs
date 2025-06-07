using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ModelClassMap : ClassMap<Model>
    {
        public ModelClassMap()
        {
            Map(m => m.VIN1_10).Name("VIN (1-10)");
            Map(m => m.County).Name("County");
            Map(m => m.City).Name("City");
            Map(m => m.State).Name("State");
            Map(m => m.PostalCode).Name("Postal Code");
            Map(m => m.ModelYear).Name("Model Year");
            Map(m => m.Make).Name("Make");
            Map(m => m.VModel).Name("Model");
            Map(m => m.ElectricVehicleType).Name("Electric Vehicle Type");
            Map(m => m.CleanAlternativeFuelVehicleCAFVEligibility).Name("Clean Alternative Fuel Vehicle (CAFV) Eligibility");
            Map(m => m.ElectricRange).Name("Electric Range");
            Map(m => m.BaseMSRP).Name("Base MSRP");
            Map(m => m.LegislativeDistrict).Name("Legislative District");
            Map(m => m.DOLVehicleID).Name("DOL Vehicle ID");
            Map(m => m.VehicleLocation).Name("Vehicle Location");
            Map(m => m.ElectricUtility).Name("Electric Utility");
            Map(m => m.num2020CensusTract).Name("2020 Census Tract");
        }
    }
}
