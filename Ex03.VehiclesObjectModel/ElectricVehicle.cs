using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.VehiclesObjectModel
{
    public abstract class ElectricVehicle : Vehicle
    {
        // Number of Minutes in one hour
        private const float k_MinutesInHour = 60;

        protected internal float m_MaxBatteryCharge;
        protected internal float m_HoursOfChargeLeft;

        public float MaxBatteryCharge
        {
            get { return m_MaxBatteryCharge; }
            set 
            { 
                m_MaxBatteryCharge = value;
                updateBattertCharge();
            }
        }

        private void updateBattertCharge()
        {
            if (m_MaxBatteryCharge != 0)
            {
                this.FuelPercentage = HoursOfChargeLeft * 100 / m_MaxBatteryCharge;
            }
        }

        public float HoursOfChargeLeft
        {
            get { return m_HoursOfChargeLeft; }
            set 
            { 
                m_HoursOfChargeLeft = value;
                updateBattertCharge();
            }
        }

        public void chargeBattery(float i_MinutesOfChargeToAdd)
        {
            float hoursOfChargeAddition = i_MinutesOfChargeToAdd / k_MinutesInHour;

            float reChargeQuantity = HoursOfChargeLeft + hoursOfChargeAddition;

            // Checks that there is no overflow after adding the charge
            ovechargeCheck(reChargeQuantity);

            // Charges the battery
            HoursOfChargeLeft = reChargeQuantity;
        }

        private void ovechargeCheck(float reChargeQuantity)
        {
            if (reChargeQuantity > MaxBatteryCharge)
            {
                throw new ValueOutOfRangeException("Electrical charge overflow");
            }
        }
    }
}
