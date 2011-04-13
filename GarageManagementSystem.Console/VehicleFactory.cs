using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ex03.VehiclesObjectModel;

namespace GarageManagementSystem.Console
{
    /// <summary>
    /// This class is used to construct new vehicles object.
    /// If a new vehicle class is addeded to the system, it
    /// should also be included here.
    /// </summary>
    public class VehicleFactory
    {

        public enum eVehicleTypes
        {
            regularCar,
            regularMotorCycle,
            regularVan,
            electricCar,
            electricMotorcycle
        }

        public static Vehicle createNewVehicle(eVehicleTypes i_VehicleType)
        {
            if (i_VehicleType.Equals(eVehicleTypes.regularCar))
            {
                return newRegularCar();
            }
            if (i_VehicleType.Equals(eVehicleTypes.regularMotorCycle))
            {
                return newRegularMotorcycle();
            }
            if (i_VehicleType.Equals(eVehicleTypes.regularVan))
            {
                return newRegularVan();
            }
            if (i_VehicleType.Equals(eVehicleTypes.electricCar))
            {
                return newElectricCar();
            }
            if (i_VehicleType.Equals(eVehicleTypes.electricMotorcycle))
            {
                return newElectricMotorcycle();
            }

            return null;
        }

        /// <summary>
        /// Returns a new Regular Car Vehicle
        /// </summary>
        /// <returns>A new Regular Car Vehicle</returns>
        public static Vehicle newRegularCar()
        {
            return new RegularCar();
        }

        /// <summary>
        /// Returns a new Regular Motorcycle Vehicle
        /// </summary>
        /// <returns>A new Regular Motorcycle Vehicle</returns>
        public static Vehicle newRegularMotorcycle()
        {
            return new RegularMotorcycle();
        }

        /// <summary>
        /// Returns a new Regular Van Vehicle
        /// </summary>
        /// <returns>A new Regular Van Vehicle</returns>
        public static Vehicle newRegularVan()
        {
            return new RegularVan();
        }

        /// <summary>
        /// Returns a new Electric Car Vehicle
        /// </summary>
        /// <returns>A new Electric Car Vehicle</returns>
        public static Vehicle newElectricCar()
        {
            return new ElectricCar();
        }

        /// <summary>
        /// Returns a new Electric Motorcycle Vehicle
        /// </summary>
        /// <returns>A new Electric Motorcycle Vehicle</returns>
        public static Vehicle newElectricMotorcycle()
        {
            return new ElectricMotorcyle();
        }
    }
}
