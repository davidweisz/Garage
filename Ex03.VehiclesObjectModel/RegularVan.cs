using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.VehiclesObjectModel
{
    public sealed class RegularVan : RegularVehicle
    {
        private const int k_DefaultNumberOfTyres = 8;
        private const float k_DefaultMaxTyreAirPressure = 25;
        private const eFuelType k_DefaultFuelType = eFuelType.Soler;
        private const float k_DefaultMaxTankCapacity = 90;

        public RegularVan()
        {
            Tyres = new List<Tyre>();
            for (int i = 0; i < k_DefaultNumberOfTyres; i++)
            {
                Tyres.Add(new Tyre(k_DefaultMaxTyreAirPressure));
            }

            FuelMaxCapacity = k_DefaultMaxTankCapacity;
            FuelType = k_DefaultFuelType;
        }
    }
}
