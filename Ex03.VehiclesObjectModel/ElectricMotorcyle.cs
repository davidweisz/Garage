using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.VehiclesObjectModel
{
    public sealed class ElectricMotorcyle : ElectricVehicle
    {
        protected internal eLicenseType m_LicenseType;

        private const int k_DefaultNumberOfTyres = 2;
        private const float k_DefaultMaxTyreAirPressure = 33;
        private const float k_DefaultMaxChargeCapacity = (float) 2.5;

        public ElectricMotorcyle()
        {
            Tyres = new List<Tyre>();
            for (int i = 0; i < k_DefaultNumberOfTyres; i++)
            {
                Tyres.Add(new Tyre(k_DefaultMaxTyreAirPressure));
            }

            MaxBatteryCharge = k_DefaultMaxChargeCapacity;
        }

        public eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }
    }
}
