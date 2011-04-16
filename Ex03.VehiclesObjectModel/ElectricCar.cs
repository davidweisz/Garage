using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.VehiclesObjectModel
{
    public sealed class ElectricCar : ElectricVehicle
    {
        private const int k_DefaultNumberOfTyres = 4;
        private const float k_DefaultMaxTyreAirPressure = 31;
        private const float k_DefaultMaxBatteryCapacity = 3;

        private Car m_ElectricCar;

        public Car Car
        {
            get { return m_ElectricCar; }
            set { m_ElectricCar = value; }
        }

        public ElectricCar()
        {
            initProperties();
            Tyres = new List<Tyre>();

            for (int i = 0; i < k_DefaultNumberOfTyres; i++)
            {
                Tyres.Add(new Tyre(k_DefaultMaxTyreAirPressure));
            }

            MaxBatteryCharge = k_DefaultMaxBatteryCapacity;
        }

        public override void initProperties()
        {
            Car = new Car();
            Properties = Car.CarProperties;
        }

        public override bool TrySetVehicleProperties(string i_InputUser, int i_IdxProperties)
        {
            return Car.TrySetCarProperties(i_InputUser, i_IdxProperties);
        }
    }
}
