using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ex03.VehiclesObjectModel;

namespace GarageManagementSystem.Console
{
    public class Garage
    {
        private const int k_ExitCode = 0;

        List<CustomerFile> m_CustomersFiles;

        public Garage() 
        {
            m_CustomersFiles = new List<CustomerFile>();
        }

        public List<CustomerFile> CustomerFiles
        {
            get { return m_CustomersFiles; }
        }

        private static void printWelcomeScreen()
        {
            System.Console.WriteLine(@"
/*------------------------------------------------------------------*\



                  Welcome to the Garage Management System!
                      By Jeremy Aboohi & David Weisz


                                                         _\|/_
                                                         (o o)
\*----------------------------------------------------oOO-{_}-OOo---*/");
            System.Console.ReadLine();
        }

        private static void printMainMenu()
        {
            System.Console.Clear();
            System.Console.Write(@"
/*===================================================================*\
  -------------------------Main Menu---------------------------------
\*===================================================================*/
 * (0) Quit                                                          *
 * (1) Enter new vehicle                                             *
 * (2) Show Garage status                                            *
 * (3) Change Vehicle status                                         *
 * (4) Inflate vehicle tyres to maximum possible                     *
 * (5) Refuel vehicle                                                *
 * (6) Charge electrical vehicle                                     *
 * (7) Show vehicle full information                                 *
 * (8) Delete vehicle from system                                    *
\*===================================================================*/

>>");
        }

        public void Run()
        {
            // Print a welcome note.
            printWelcomeScreen();

            //Setting color details for console
            changeConsoleColors(ConsoleColor.Blue, ConsoleColor.Yellow);

           int userInputInt;

            while (true)
            {
                printMainMenu();
                userInputInt = getIntegerInputFromUser();
                if (!isExitCode(userInputInt))
                {
                    switch (userInputInt)
                    {
                        case 0:
                            Environment.Exit(0);
                            break;
                        case 1:
                            addNewVehicle();
                            break;
                        case 2:
                            showGarageStatus();
                            waitForUser();
                            break;
                        case 3:
                            changeVehicleStatus();
                            waitForUser();
                            break;
                        case 4:
                            inflateVehicleTyres();
                            waitForUser();
                            break;
                        case 5:
                            refuelVehicle();
                            waitForUser();
                            break;
                        case 6:
                            chargeVehicle();
                            waitForUser();
                            break;
                        case 7:
                            showVehicleFullInformation();
                            waitForUser();
                            break;
                        case 8:
                            deleteVehicleFromGarage();
                            waitForUser();
                            break;
                        default:
                            System.Console.WriteLine("Please enter a correct value.");
                            break;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        private static void changeConsoleColors(ConsoleColor i_BackgroundColor, ConsoleColor i_ForegroundColor)
        {
            System.Console.BackgroundColor = i_BackgroundColor;
            System.Console.ForegroundColor = i_ForegroundColor;
        }

        private void waitForUser()
        {
            System.Console.Write("Press any key to continue...");
            System.Console.ReadLine();
        }

        private void deleteVehicleFromGarage()
        {
            System.Console.Clear();
            System.Console.WriteLine(@"
/*===================================================================*\
  ------------------------Delete Vehicle-----------------------------
\*===================================================================*/");
            
            string inputLicenseString = getLicenseNumberFromUser();
            
            foreach (CustomerFile file in CustomerFiles)
            {
                if (file.Vehicle.LicenseNumber.Equals(inputLicenseString))
                {
                    CustomerFiles.Remove(file);
                    break;
                }
            }
        }

        private void addNewVehicle()
        {
            string inputLicenseString = getLicenseNumberFromUser();
            CustomerFile newCustomerFile = findFileByVehicleLicenseNumber(inputLicenseString);
            
            // If the vehicle is in the garage already, update status
            if (newCustomerFile != null)
            {
                updateVehicleStatus(newCustomerFile, CustomerFile.eVehicleStatus.Unfixed);
                System.Console.WriteLine("This vehicle is already in the Garage.");
                return;
            }
            else
            {
                addCustomerDetailsToFile(out newCustomerFile);

                VehicleFactory.eVehicleTypes vehicleTypeInput;
                while (true)
                {
                    try
                    {
                        vehicleTypeInput = getVehicleTypeFromUser();
                    }
                    catch (FormatException e)
                    {
                        System.Console.WriteLine(e.Message);
                        continue;
                    }
                    break;
                }

                newCustomerFile.Vehicle = VehicleFactory.createNewVehicle(vehicleTypeInput);

                addVehicleModelName(newCustomerFile);
                
                // Adds the license number to the vehicle
                newCustomerFile.Vehicle.LicenseNumber = inputLicenseString;

                // Get power information (fuel or electrical source)
                getPowerSourceInformation(newCustomerFile);

                // Update tyres pressure
                System.Console.WriteLine("Update tyres details - ");
                updateTyresForCustomerVehicle(newCustomerFile);


                // Add other special properties
                int propertieIndex = 0;
                bool inputPropertieSuccess;
                foreach (string propertie in newCustomerFile.Vehicle.Properties)
                {
                    inputPropertieSuccess= false;
                    do
                    {
                        System.Console.Write(propertie);
                        string userInput = System.Console.ReadLine();
                        inputPropertieSuccess = newCustomerFile.Vehicle.TrySetVehicleProperties(userInput, propertieIndex);
                    } while (inputPropertieSuccess == false);
                    propertieIndex++;
                }

                // Adds the new file
                CustomerFiles.Add(newCustomerFile);
            }
            
        }

        private void addVehicleModelName(CustomerFile i_CustomerFile)
        {
            System.Console.Write("Enter vehicle's model name: ");
            string vehicleModelName = System.Console.ReadLine();
            i_CustomerFile.Vehicle.ModelName = vehicleModelName;
        }

        private void getPowerSourceInformation(CustomerFile io_CustomerFile)
        {
            if (io_CustomerFile.Vehicle is RegularVehicle)
            {
                RegularVehicle.eFuelType fuelType = getFuelTypeFromUser();
                (io_CustomerFile.Vehicle as RegularVehicle).FuelType = fuelType;
                float fuelQuantityFromUser = getFuelQuantityFromUser();
                (io_CustomerFile.Vehicle as RegularVehicle).FuelQuantity = fuelQuantityFromUser;
            }
            else
            {
                // Add special options for electric vehicles
                float chargeLeftInput = getBatteryChargeLeftFromUser();
                (io_CustomerFile.Vehicle as ElectricVehicle).HoursOfChargeLeft = chargeLeftInput;
            }
        }

        private static void addCustomerDetailsToFile(out CustomerFile o_NewCustomerFile)
        {
            System.Console.Write("Enter customer name: ");
            string customerName = System.Console.ReadLine();
            System.Console.Write("Enter customer phone number: ");
            string customerPhoneNumber = System.Console.ReadLine();

            o_NewCustomerFile = new CustomerFile(customerName, customerPhoneNumber);

        }

        private void updateTyresForCustomerVehicle(CustomerFile newCustomerFile)
        {
            string tyresBrandName = getBrandNameFromUser();
            try
            {
                System.Console.Write("Enter tyre max pressure: ");
                float tyresMaxPressureByVendor = getPressuresFromUser();
                System.Console.Write("Enter tyre current pressure: ");
                float tyreCurrentAirPressure = getPressuresFromUser();
                foreach (Tyre tyre in newCustomerFile.Vehicle.Tyres)
                {
                    tyre.BrandName = tyresBrandName;
                    tyre.MaxPressure = tyresMaxPressureByVendor;
                    tyre.AirPressure = tyreCurrentAirPressure;
                }
            }
            catch (FormatException e)
            {
                System.Console.WriteLine(e.Data);
            }
        }

        private string getBrandNameFromUser()
        {
            System.Console.Write("Enter tyre brand:");
            return System.Console.ReadLine();
        }

        private float getPressuresFromUser()
        {
            float maxPressureFromUser;
            string maxPressureString = System.Console.ReadLine();
            if (float.TryParse(maxPressureString, out maxPressureFromUser) == false)
            {
                throw new FormatException("Pressure Error");
            }
            return maxPressureFromUser;
        }

        private float getBatteryChargeLeftFromUser()
        {
            float chargeLeftInput;
            string chargeLeftInputString = string.Empty;
            do
            {
                System.Console.Write("Enter charge left in hours: ");
                chargeLeftInputString = System.Console.ReadLine();
            } while (float.TryParse(chargeLeftInputString, out chargeLeftInput) == false);
            return chargeLeftInput;
        }

        private static VehicleFactory.eVehicleTypes getVehicleTypeFromUser()
        {
            string[] vehicleTypes = Enum.GetNames(typeof(VehicleFactory.eVehicleTypes));
            int optionCounter = 0;
            System.Console.WriteLine(@"
/*===================================================================*\
  ----------------------Choose vehicle type--------------------------
\*===================================================================*/");
            foreach (string type in vehicleTypes)
            {
                System.Console.Write(string.Format(" {0} - {1}{2}", optionCounter, type, Environment.NewLine));
                optionCounter++;
            }

            System.Console.Write(@"
\*===================================================================*/

>>");
            string vehicleTypeInputString = System.Console.ReadLine();

            VehicleFactory.eVehicleTypes vehicleTypeInput;
            if (Enum.TryParse<VehicleFactory.eVehicleTypes>(vehicleTypeInputString, out vehicleTypeInput) == false)
            {
                throw new FormatException("Illegal vehicle type.");
            }

            return vehicleTypeInput;
        }

        private void showGarageStatus()
        {
            System.Console.Clear();
            System.Console.Write(@"
/*===================================================================*\
  -------------------------Garage status-----------------------------
\*===================================================================*/
 * (0) Back to Main Menu                                             *
 * (1) Show all vehicles in the garage                               *
 * (2) Show only Fixed Vehicles                                      *
 * (3) Show only unFixed Vehicles                                    *
 * (4) Show only already paid Vehicles                               *
\*===================================================================*/

>>");

            int userInputInt = getIntegerInputFromUser();

            switch (userInputInt)
            {
                case 0:
                    break;
                case 1:
                    showAllVehiclesInGarage();
                    break;
                case 2:
                    showAllVehiclesInGarageOfStatus(CustomerFile.eVehicleStatus.Fixed);
                    break;
                case 3:
                    showAllVehiclesInGarageOfStatus(CustomerFile.eVehicleStatus.Unfixed);
                    break;
                case 4:
                    showAllVehiclesInGarageOfStatus(CustomerFile.eVehicleStatus.Paid);
                    break;
                default:
                    System.Console.WriteLine("Please enter a correct value.");
                    showGarageStatus();
                    break;
            }

        }

        private void showAllVehiclesInGarage()
        {
            foreach (CustomerFile file in m_CustomersFiles)
            {
                file.PrintFileInformation();
            } 
        }

        private void showAllVehiclesInGarageOfStatus(CustomerFile.eVehicleStatus i_Status)
        {
            foreach (CustomerFile file in m_CustomersFiles)
            {
                if (file.Status.Equals(i_Status))
                {
                    file.PrintFileInformation();
                }
            }
        }

        private void changeVehicleStatus()
        {
            System.Console.Clear();
            System.Console.WriteLine(@"
/*===================================================================*\
  ---------------------Change Vehicle Status-------------------------
\*===================================================================*/");
            string licenseNumber = getLicenseNumberFromUser();
            CustomerFile file = findFileByVehicleLicenseNumber(licenseNumber);
            if (file != null)
            {
                updateVehicleStatus(file, getVehicleStatusFromUser());
                printSuccessMsg();
            }
            else
            {
                System.Console.WriteLine("Sorry!, that vehicle is not in the garage");
            }
        }

        private void updateVehicleStatus(CustomerFile i_File, CustomerFile.eVehicleStatus i_Status)
        {
                i_File.Status = i_Status;
        }

        private static void printSuccessMsg()
        {
            System.Console.WriteLine("Update succeded");
        }

        /// <summary>
        /// Prompts user for license number
        /// </summary>
        /// <returns>Returns user input as string without check</returns>
        private static string getLicenseNumberFromUser()
        {
            System.Console.Write("Enter License Number: ");
            string licenseNumber = System.Console.ReadLine();
            if (licenseNumber.Length == 0)
            {
                licenseNumber = getLicenseNumberFromUser();
            }

            return licenseNumber;
        }

        /// <summary>
        /// Gets a vehicle status from the user
        /// </summary>
        /// <returns>vehicle status</returns>
        private CustomerFile.eVehicleStatus getVehicleStatusFromUser()
        {
            System.Console.Write("Please status [unfixed, fixed, paid]: ");
            string userInputString = System.Console.ReadLine();
            CustomerFile.eVehicleStatus newStatus;

            if (Enum.TryParse<CustomerFile.eVehicleStatus>(userInputString, out newStatus) == false)
            {
                System.Console.Write(string.Format("Incorrect status{0}", Environment.NewLine));
                return getVehicleStatusFromUser();
            }
            return newStatus;
        }

        /// <summary>
        /// Inflates vehicle tyres to maximum
        /// </summary>
        private void inflateVehicleTyres()
        {
            string licenseNumber = getLicenseNumberFromUser();
            CustomerFile file = findFileByVehicleLicenseNumber(licenseNumber);
            if (file != null)
            {
                foreach (Tyre tyre in file.Vehicle.Tyres)
                {
                    tyre.AirPressure = tyre.MaxPressure;
                }
                printSuccessMsg();
            }
        }

        /// <summary>
        /// Finds a file by searching vehicles numbers
        /// </summary>
        /// <param name="licenseNumber"></param>
        /// <returns></returns>
        private CustomerFile findFileByVehicleLicenseNumber(string i_LicenseNumber)
        {
            CustomerFile foundFile = null;

            foreach (CustomerFile file in m_CustomersFiles)
            {
                if (file.Vehicle.LicenseNumber != null && file.Vehicle.LicenseNumber.Equals(i_LicenseNumber))
                {
                    foundFile = file;
                    break;
                }
            }

            return foundFile;
        }

        /// <summary>
        /// Refuels a vehicle which is regular (not electric)
        /// </summary>
        private void refuelVehicle()
        {
            System.Console.Clear();
            System.Console.WriteLine(@"
/*===================================================================*\
  -------------------------Refuel Vehicle----------------------------
\*===================================================================*/");
            string userInputLicenseNumber = getLicenseNumberFromUser();
            CustomerFile file = findFileByVehicleLicenseNumber(userInputLicenseNumber);

            if (file != null && file.Vehicle is RegularVehicle)
            {
                RegularVehicle.eFuelType inputFuelType = getFuelTypeFromUser();
                float fuelQuantity;

                do
                {
                    fuelQuantity = getFuelQuantityFromUser();
                    try
                    {
                        ((RegularVehicle)file.Vehicle).Refuel(fuelQuantity, inputFuelType);
                    }
                    catch (ValueOutOfRangeException e)
                    {
                        System.Console.WriteLine(e.Message);
                        fuelQuantity = -1;
                    }
                    
                } while (fuelQuantity == -1);

                printSuccessMsg();
            }
        }

        /// <summary>
        /// Prompts the user for a fuel float and positive number
        /// </summary>
        /// <returns>The float value of fuel to refuel</returns>
        private float getFuelQuantityFromUser()
        {
            System.Console.Write("Enter fuel in litres: ");
            float userInputFuel;
            while (float.TryParse(System.Console.ReadLine(), out userInputFuel) == false || userInputFuel < 0)
            {
                System.Console.Write("Enter a positive number: ");
            }

            return userInputFuel;
        }

        private RegularVehicle.eFuelType getFuelTypeFromUser()
        {

            string[] fuelTypeArray = Enum.GetNames(typeof(RegularVehicle.eFuelType));
            int optionCounter = 0;
            System.Console.WriteLine(@"
/*===================================================================*\
  ----------------------Choose fuel type--------------------------
\*===================================================================*/");
            foreach (string fuelTypeString in fuelTypeArray)
            {
                System.Console.Write(string.Format(" {0} - {1}{2}", optionCounter, fuelTypeString, Environment.NewLine));
                optionCounter++;
            }
            System.Console.Write(@"
\*===================================================================*/

>>");

            string userInputString = System.Console.ReadLine();
            RegularVehicle.eFuelType userInputFuelType;

            // Try to parse the string input to a correct option
            Enum.TryParse<RegularVehicle.eFuelType>(userInputString, out userInputFuelType);

            // If the input was not a correct a option, try again
            if (userInputFuelType.ToString().Equals(""))
            {
                return getFuelTypeFromUser();
            }

            return userInputFuelType;
        }

        private void chargeVehicle()
        {
            System.Console.Clear();
            System.Console.WriteLine(@"
/*===================================================================*\
  -------------------------Charge Vehicle----------------------------
\*===================================================================*/");
            string userInputLicenseNumber = getLicenseNumberFromUser();
            CustomerFile file = findFileByVehicleLicenseNumber(userInputLicenseNumber);

            if (file != null && file.Vehicle is ElectricVehicle)
            {
                float batteryChargeInMinFromUser;

                do
                {
                    batteryChargeInMinFromUser = getBatteryChargeFromUser();
                    try
                    {
                        ((ElectricVehicle)file.Vehicle).chargeBattery(batteryChargeInMinFromUser);
                    }
                    catch (ValueOutOfRangeException e)
                    {
                        System.Console.WriteLine(e.Message);
                        batteryChargeInMinFromUser = -1;
                    }

                } while (batteryChargeInMinFromUser == -1);

                printSuccessMsg();
            }
        }

        private float getBatteryChargeFromUser()
        {
            System.Console.Write("Enter charge time in minutes: ");
            float userInputMintues;
            while (float.TryParse(System.Console.ReadLine(), out userInputMintues) == false || userInputMintues < 0)
            {
                System.Console.Write("Enter a positive float number: ");
            }

            return userInputMintues;
        }

        private void showVehicleFullInformation()
        {
            System.Console.Clear();
            System.Console.WriteLine(@"
/*===================================================================*\
  ---------------Show Vehicle Full Information-----------------------
\*===================================================================*/");
            string userInputLicense = getLicenseNumberFromUser();
            CustomerFile file = findFileByVehicleLicenseNumber(userInputLicense);

            if (file != null)
            {
                printFullInformationForFile(file);
            }
        }

        private static void printFullInformationForFile(CustomerFile file)
        {
            System.Console.Write(string.Format(@"
License Number: {0}
Model: {1}
Owner: {2}
Vehicle Status: {3}
Tyres: 
",
file.Vehicle.LicenseNumber,
file.Vehicle.ModelName,
file.Name,
file.Status));
            foreach (Tyre tyre in file.Vehicle.Tyres)
            {
                System.Console.WriteLine(string.Format("Brand: {1} Pressure: {2}{0}", Environment.NewLine, tyre.BrandName, tyre.AirPressure));
            }

            if (file.Vehicle is RegularVehicle)
            {
                System.Console.WriteLine(string.Format("Fuel type: {1}{0}Fuel percentange: {2:0.00}%{0}Tank Max capacity: {3}", Environment.NewLine,
                    ((RegularVehicle)file.Vehicle).FuelType, ((RegularVehicle)file.Vehicle).FuelPercentage, ((RegularVehicle)file.Vehicle).FuelMaxCapacity));
            }
            else if (file.Vehicle is ElectricVehicle)
            {
                System.Console.WriteLine(string.Format("Charge left: {1}{0}Max charge: {2}", Environment.NewLine,
                    ((ElectricVehicle)file.Vehicle).HoursOfChargeLeft, ((ElectricVehicle)file.Vehicle).MaxBatteryCharge));
            }
        }

        /// <summary>
        /// Returns true if the exit number was entered, else false.
        /// </summary>
        /// <param name="i_UserInputString">The input as string</param>
        /// <param name="o_UserInputInt">The code entered converted to int</param>
        /// <returns>True if the exit number was entered, else false</returns>
        private bool isExitCode(int i_UserInputInt)
        {
            bool result = false;
            if (i_UserInputInt == k_ExitCode)
            {
                result = true;
            }
            return result;
        }

        private static int getIntegerInputFromUser()
        {
            string userInputString = System.Console.ReadLine();
            int userInputConvertedToInt;

            // If not a number entered, prompt again
            if (int.TryParse(userInputString, out userInputConvertedToInt) == false)
            {
                System.Console.WriteLine("Please enter a natural number in the range: ");
                return getIntegerInputFromUser();
            }

            return userInputConvertedToInt;
        }
    }
}
