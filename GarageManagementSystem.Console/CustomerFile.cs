using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ex03.VehiclesObjectModel;

namespace GarageManagementSystem.Console
{
    public sealed class CustomerFile
    {
        [Flags]
        public enum eVehicleStatus
        {
            Unfixed = 1,
            Fixed = 2,
            Paid = 4
        }

        private Vehicle m_CustomerVehicle;
        private string m_CustomerName;
        private string m_CustomerPhoneNumber;
        private eVehicleStatus m_VehicleStatus;

        /// <summary>
        /// Constructor for a Customer File
        /// </summary>
        /// <param name="i_CustomerVehicle">The customers vehicle</param>
        /// <param name="i_CustomerName">Customers Name as string</param>
        /// <param name="i_CustomerPhoneNumber">Customers phone number as string</param>
        public CustomerFile(Vehicle i_CustomerVehicle, string i_CustomerName, string i_CustomerPhoneNumber)
        {
            Vehicle = i_CustomerVehicle;
            Name = i_CustomerName;
            PhoneNumber = i_CustomerPhoneNumber;
            m_VehicleStatus = eVehicleStatus.Unfixed;
        }

        public CustomerFile(Vehicle i_CustomerVehicle) : this(i_CustomerVehicle, null, null) { }

        public CustomerFile(string i_CustomerName, string i_CustomerPhone) : this(null, i_CustomerName, i_CustomerPhone) { }

        public Vehicle Vehicle
        {
            get { return m_CustomerVehicle; }
            set { m_CustomerVehicle = value; }
        }

        public string Name
        {
            get { return m_CustomerName; }
            set { m_CustomerName = value; }
        }

        public string PhoneNumber
        {
            get { return m_CustomerPhoneNumber; }
            set { m_CustomerPhoneNumber = value; }
        }
        
        public eVehicleStatus Status
        {
            get { return m_VehicleStatus; }
            set { m_VehicleStatus = value; }
        }

        public void PrintFileInformation()
        {
            System.Console.WriteLine(string.Format("Vehicle owner: {1}{0}Owner Phone Number: {2}{0}Vehicle details:{0}License Number: {3}{0}Model Name: {4}{0}Fuel Tank Percentage: {5:00}%{0}Vehicle Status: {6}{0}", Environment.NewLine, Name, PhoneNumber, Vehicle.LicenseNumber, Vehicle.ModelName, Vehicle.FuelPercentage, Status));
        }
    }
}
