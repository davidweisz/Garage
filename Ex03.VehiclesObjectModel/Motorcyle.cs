using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.VehiclesObjectModel
{
    class Motorcyle
    {
        public enum eLicenseType
        {
            A,
            A1,
            A2,
            B
        }

        private eLicenseType m_LicenseType;
        private List<string> m_MotorcyleProperties;

        public List<string> MotorcyleProperties
        {
            get { return m_MotorcyleProperties; }
            set { m_MotorcyleProperties = value; }
        }

        public eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }

        public Motorcyle()
        {
            MotorcyleProperties = new List<string>();
            motorcyleProperties();
        }

        private void motorcyleProperties()
        {
            MotorcyleProperties.Add("Enter licene type: ");
        }

        protected internal bool TrySetMotorcyleProperties(string i_InputUser)
        {
            eLicenseType license;
            bool sucess = false;
            if (tryParseLicense(i_InputUser, out license))
            {
                LicenseType = license;
                sucess = true;
            }

            return sucess;
        }

        private bool tryParseLicense(string i_Value, out eLicenseType o_Licene)
        {
            bool validLicene = false;
            if (Enum.TryParse(i_Value, out o_Licene))
            {
                if (Enum.IsDefined(typeof(eLicenseType), o_Licene))
                {
                    validLicene = true;
                }
            }

            return validLicene;
        }
    }
}
