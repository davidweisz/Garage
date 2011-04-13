using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.VehiclesObjectModel
{
    public abstract class RegularVehicle : Vehicle
    {
        public enum eFuelType
        {
            Octan95,
            Octan96,
            Octan98,
            Soler
        }

        protected internal float m_MaxFuelTankCapacity;
        protected internal float m_FuelQuantity;
        protected internal eFuelType m_FuelType;

        public void init(string i_ModelName, string i_LicenseNumber, float i_FuelPercentageLeft)
        {
            ModelName = i_ModelName;
            LicenseNumber = i_LicenseNumber;
            FuelPercentage = i_FuelPercentageLeft;
        }

        public void Refuel(float i_FuelQuantity, eFuelType i_FuelType)
        {
            // Checks that they have the same fuel type
            if (isSameFuelType(i_FuelType))
            {
                throw new ArgumentException();
            }

            float refuelCalculation = m_FuelQuantity + i_FuelQuantity;

            // Checks that we do not receive an overflow of fuel tank
            if (refuelCalculation > FuelMaxCapacity)
            {
                throw new ValueOutOfRangeException();
            }

            // Refuels
            FuelMaxCapacity = refuelCalculation;
        }

        private bool isSameFuelType(eFuelType i_FuelType)
        {
            bool result = false;
            if (i_FuelType.Equals(FuelType))
            {
                result = true;
            }

            return result;
        }

        public eFuelType FuelType
        {
            get { return m_FuelType; }
            set { m_FuelType = value; }
        }

        public float FuelQuantity
        {
            get { return m_FuelQuantity; }
            set 
            { 
                m_FuelQuantity = value;
                updateFuelPercentage();
            }
        }

        public float FuelMaxCapacity
        {
            get { return m_MaxFuelTankCapacity; }
            set 
            { 
                m_MaxFuelTankCapacity = value;
                updateFuelPercentage();
            }
        }

        private void updateFuelPercentage()
        {
            if (m_MaxFuelTankCapacity != 0)
            {
                this.FuelPercentage = FuelQuantity * 100 / m_MaxFuelTankCapacity;
            }
        }
    }
}
