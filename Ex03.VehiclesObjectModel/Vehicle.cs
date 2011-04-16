using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.VehiclesObjectModel
{
    public abstract class Vehicle
    {
        protected internal string m_ModelName;
        protected internal string m_LicenseNumber;
        protected internal float m_FuelPercentage;
        protected internal List<Tyre> m_Tyres;
        protected internal List<string> m_VehicleProperties;
        
        public string ModelName
        {
            get { return m_ModelName; }
            set { m_ModelName = value; }
        }

        public string LicenseNumber
        {
            get { return m_LicenseNumber; }
            set { m_LicenseNumber = value; }
        }

        public float FuelPercentage
        {
            get { return m_FuelPercentage; }
            set { m_FuelPercentage = value; }
        }

        public List<Tyre> Tyres
        {
            get { return m_Tyres; }
            set { m_Tyres = value; }
        }

        public List<string> Properties
        {
            get { return m_VehicleProperties; }
            set { m_VehicleProperties = value; }
        }

        public void updateTyreDetails(int i_TyreIndex, string i_TyreBrandName, float i_CurrentPressure)
        {
            overflowTyreIndex(i_TyreIndex);
            Tyres[i_TyreIndex].BrandName = i_TyreBrandName;
            Tyres[i_TyreIndex].AirPressure = i_CurrentPressure;
        }

        private void overflowTyreIndex(int i_TyreIndex)
        {
            if (Tyres.Count < i_TyreIndex)
            {
                throw new ValueOutOfRangeException("Tyre index overflow");
            }
        }

        public override bool Equals(object obj)
        {
            bool result = false;
            Vehicle castedVehicle = obj as Vehicle;
            if (castedVehicle != null)
            {
                result = castedVehicle.LicenseNumber.Equals(this.LicenseNumber);
            }
            return result;
        }

        public abstract void initProperties();
        public abstract bool TrySetVehicleProperties(string i_InputUser, int i_Idx);
    }
}
