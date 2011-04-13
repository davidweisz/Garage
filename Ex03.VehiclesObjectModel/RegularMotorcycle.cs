using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.VehiclesObjectModel
{
    public sealed class RegularMotorcycle : RegularVehicle
    {
        private eLicenseType m_LicenseType;

        private const int k_DefaultNumberOfTyres = 2;
        private const float k_DefaultMaxTyreAirPressure = 33;
        private const eFuelType k_DefaultFuelType = eFuelType.Octan95;
        private const float k_DefaultMaxTankCapacity = 7;

        public RegularMotorcycle()
        {
            Tyres = new List<Tyre>();
            for (int i = 0; i < k_DefaultNumberOfTyres; i++)
            {
                Tyres.Add(new Tyre(k_DefaultMaxTyreAirPressure));
            }

            FuelMaxCapacity = k_DefaultMaxTankCapacity;
            FuelType = k_DefaultFuelType;
        }

        public eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }
    }
}
