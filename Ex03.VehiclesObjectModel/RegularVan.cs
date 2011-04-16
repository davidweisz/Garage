using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.VehiclesObjectModel
{
    public sealed class RegularVan : RegularVehicle
    {
        public enum eCargoTypes
        {
            Passengers,
            Goods,
            DangerousCargo
        }
        
        private const int k_DefaultNumberOfTyres = 8;
        private const float k_DefaultMaxTyreAirPressure = 25;
        private const eFuelType k_DefaultFuelType = eFuelType.Soler;
        private const float k_DefaultMaxTankCapacity = 90;

        private eCargoTypes m_CargoType;

        public eCargoTypes Cargo
        {
            get { return m_CargoType; }
            set { m_CargoType = value; }
        }

        public RegularVan()
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
            Properties.Add("Enter the cargo type");
        }

        private bool tryParseCargoType(string i_Value, out eCargoTypes o_Licene)
        {
            bool validCargo = false;
            if (Enum.TryParse(i_Value, out o_Licene))
            {
                if (Enum.IsDefined(typeof(eCargoTypes), o_Licene))
                {
                    validCargo = true;
                }
            }

            return validCargo;
        }

        public override bool TrySetVehicleProperties(string i_InputUser, int i_IdxProperties)
        {
            eCargoTypes o_Cargo;
            bool success = tryParseCargoType(i_InputUser, out o_Cargo) ? true : false;
            
            if (success)
            {
                Cargo = o_Cargo;
            }

            return success;
        }
    }
}
