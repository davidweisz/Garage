using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.VehiclesObjectModel
{
    public sealed class ElectricMotorcyle : ElectricVehicle
    {
        private const int k_DefaultNumberOfTyres = 2;
        private const float k_DefaultMaxTyreAirPressure = 33;
        private const float k_DefaultMaxChargeCapacity = (float)2.5;

        private Motorcyle m_ElectricMotorcyle;

        internal Motorcyle Motorcyle
        {
            get { return m_ElectricMotorcyle; }
            set { m_ElectricMotorcyle = value; }
        }

        public ElectricMotorcyle()
        {
            initProperties();
            Tyres = new List<Tyre>();
            for (int i = 0; i < k_DefaultNumberOfTyres; i++)
            {
                Tyres.Add(new Tyre(k_DefaultMaxTyreAirPressure));
            }

            MaxBatteryCharge = k_DefaultMaxChargeCapacity;
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
