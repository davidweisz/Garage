using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.VehiclesObjectModel
{
    public sealed class RegularCar : RegularVehicle
    {
        private eLicenseType m_LicenseType;

        private const int k_DefaultNumberOfTyres = 4;
        private const float k_DefaultMaxTyreAirPressure = 31;
        private const eFuelType k_DefaultFuelType = eFuelType.Octan98;
        private const float k_DefaultMaxTankCapacity = 42;

        public RegularCar()
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
