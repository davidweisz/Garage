using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.VehiclesObjectModel
{
    public sealed class RegularMotorcycle : RegularVehicle
    {
        private const int k_DefaultNumberOfTyres = 2;
        private const float k_DefaultMaxTyreAirPressure = 33;
        private const eFuelType k_DefaultFuelType = eFuelType.Octan95;
        private const float k_DefaultMaxTankCapacity = 7;

        private Motorcyle m_RegularMotorcyle;

        internal Motorcyle Motorcyle
        {
            get { return m_RegularMotorcyle; }
            set { m_RegularMotorcyle = value; }
        }

        public RegularMotorcycle()
        {
            initProperties();
            Tyres = new List<Tyre>();
            for (int i = 0; i < k_DefaultNumberOfTyres; i++)
            {
                Tyres.Add(new Tyre(k_DefaultMaxTyreAirPressure));
            }

            FuelMaxCapacity = k_DefaultMaxTankCapacity;
            FuelType = k_DefaultFuelType;
        }

        public override void initProperties()
        {
            Motorcyle = new Motorcyle();
            Properties = Motorcyle.MotorcyleProperties;
        }

        public override bool TrySetVehicleProperties(string i_InputUser, int i_IdxProperties)
        {
            return Motorcyle.TrySetMotorcyleProperties(i_InputUser);
        }
    }
}
