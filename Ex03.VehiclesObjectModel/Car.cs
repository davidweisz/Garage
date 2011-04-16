using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.VehiclesObjectModel
{
    public class Car
    {
        public enum eVehicleColor
        {
            Red,
            Yellow,
            Black,
            White,
            Blue
        }

        private eVehicleColor m_CarColor;
        private int m_NumOfDoors;
        private List<string> m_CarProperties;

        public List<string> CarProperties
        {
            get { return m_CarProperties; }
            set { m_CarProperties = value; }
        }

        public eVehicleColor Color
        {
            get { return m_CarColor; }
            set { m_CarColor = value; }
        }

        public int Doors
        {
            get { return m_NumOfDoors; }
            set { m_NumOfDoors = value; }
        }

        public Car()
        {
            carProperties();
        }

        private void carProperties()
        {
            CarProperties = new List<string>();
            CarProperties.Add("Enter the car's color: ");
            CarProperties.Add("Enter the number of doors: ");
        }

        private bool tryParseColor(string i_Value, out eVehicleColor o_ColorValue)
        {
            return Enum.TryParse(i_Value, out o_ColorValue);
        }

        protected internal bool TrySetCarProperties(string i_InputUser, int i_IdxProperties)
        {
            bool success = false;
            switch (i_IdxProperties)
            {
                case 0:
                    {
                        eVehicleColor carColor;
                        if (tryParseColor(i_InputUser, out carColor))
                        {
                            Color = carColor;
                            success = true;
                        }
                        break;
                    }

                case 1:
                    {
                        int doorsNum;
                        if (Int32.TryParse(i_InputUser, out doorsNum))
                        {
                            Doors = doorsNum;
                            success = true;
                        }
                        break;
                    }
            }

            return success;
        }
    }
}
