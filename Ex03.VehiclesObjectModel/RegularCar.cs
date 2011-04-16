using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.VehiclesObjectModel
{
    public sealed class RegularCar : RegularVehicle
    {
        private const int k_DefaultNumberOfTyres = 4;
        private const float k_DefaultMaxTyreAirPressure = 31;
        private const eFuelType k_DefaultFuelType = eFuelType.Octan98;
        private const float k_DefaultMaxTankCapacity = 42;

        private Car m_RegularCar;

        public Car Car
        {
            get { return m_RegularCar; }
            set { m_RegularCar = value; }
        }

        public RegularCar()
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
            Car = new Car();
            Properties = Car.CarProperties;
        }

        public override bool TrySetVehicleProperties(string i_InputUser, int i_IdxProperties)
        {
            return Car.TrySetCarProperties(i_InputUser, i_IdxProperties);
        }
    }
}
