using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.VehiclesObjectModel
{
    public class Tyre
    {
        protected internal float m_MaxPressure;
        protected internal string m_BrandName;
        protected internal float m_CurrectPressure;

        public Tyre(string i_Brand, float i_MaxPressure, float i_CurrentPressure)
        {
            m_BrandName = i_Brand;
            m_MaxPressure = i_MaxPressure;
            AirPressure = i_CurrentPressure;
        }

        public Tyre(float i_MaxPressure) : this(string.Empty, i_MaxPressure, 0) 
        { }

        internal void inflate(float i_AirPressure)
        {
            float calculateFinalAirPressure = AirPressure + i_AirPressure;

            // Checks that there is no air overflow
            overflowCheck(calculateFinalAirPressure);

            AirPressure = calculateFinalAirPressure;
        }

        private void overflowCheck(float calculateFinalAirPressure)
        {
            if (calculateFinalAirPressure > MaxPressure)
            {
                throw new ValueOutOfRangeException("Pressure overflow");
            }
        }

        public float AirPressure
        {
            get { return m_CurrectPressure; }
            set { m_CurrectPressure = value; }
        }

        public float MaxPressure
        {
            get { return m_MaxPressure; }
            set { m_MaxPressure = value; }
        }

        public string BrandName
        {
            get { return m_BrandName; }
            set { m_BrandName = value; }
        }
    }
}
