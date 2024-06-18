using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    internal class ConsoleUI
    {
        Garage garage = new GarageLogic.Garage();

        public void RunTheGarage()
        {
            while (true)
            {
                PrintMenu();
                int actionChoice = ChooseAction();

                if (actionChoice == 1)
                    GetDataFromNewVehicle();
                if (actionChoice == 2)
                    DisplayListOfLicenseNumbers();
                if (actionChoice == 3)
                    GetConditionAndLicenseNumberAndChangeCondition();
                if (actionChoice == 4)
                    InflatingWheelToMaximum();
                if (actionChoice == 7)
                    PresentsFullDataAccordingToLicenseNumber();
            }
        }
        public void PrintMenu()
        {
            Console.WriteLine("Welcome to our garage, please choose from the actions:");
            Console.WriteLine("1. Add a new vehicle to the garage");
            Console.WriteLine("2. Present the list of license numbers of the vehicles in the garage");
            Console.WriteLine("3. Change vehicle condition");
            Console.WriteLine("4. Inflate a vehicle's wheels to maximum");
            Console.WriteLine("5. Fuel a vehicle");
            Console.WriteLine("6. Charge a vehicle");
            Console.WriteLine("7. Present full vehicle's data");
        }
        public int ChooseAction()
        {
            int actionChoice = 0;
            bool validInput = false;

            while (!validInput)
            {
                try
                {
                    if (int.TryParse(Console.ReadLine(), out actionChoice))
                    {
                        if (actionChoice < 1 || actionChoice > 7)
                        {
                            throw new ArgumentException("please enter between 1-7 actions only.");
                        }
                        validInput = true;
                    }
                    else
                    {
                        throw new FormatException();
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Console.Clear();
            return actionChoice;
        }
        public void GetDataFromNewVehicle()
        {
            int kindOfVehicle = 0;
            string licenseNumber;

            Console.Write("Please enter a license number: ");
            licenseNumber = Console.ReadLine();
            if (garage.CheckIfVehicleIsInTheGarage(licenseNumber))
            {
                Console.WriteLine("The vehicle is already in the garage, the condition change to: 'Under Repair'!");
                garage.ChangeVehicleCondition(licenseNumber, eVehicleCondition.UnderRepair);
            }
            else
            {
                Console.Write("Enter owner name: ");
                string ownerName = Console.ReadLine();

                Console.Write("Enter owner phone number: ");
                string ownerPhoneNumber = Console.ReadLine();

                VehicleOwnerData newOwnerInTheGarage = new VehicleOwnerData(ownerName, ownerPhoneNumber);

                PrintsTheChooseOfVehicle();
                bool validInput = false;
                while (!validInput)
                {
                    try
                    {
                        if (int.TryParse(Console.ReadLine(), out kindOfVehicle))
                        {
                            ValidationCheckForVehicleKind(kindOfVehicle);
                            validInput = true;
                        }
                        else
                        {
                            throw new FormatException();
                        }
                    }
                    catch (ValueOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                newOwnerInTheGarage.TheVehicle = Factory.CreateNewVehicle(kindOfVehicle);
                newOwnerInTheGarage.TheVehicle.LicenseNumber = licenseNumber;

                GetGeneralVehicleData(newOwnerInTheGarage.TheVehicle);

                GetSpecificVehicleData(newOwnerInTheGarage.TheVehicle);

                garage.AddVehicleToGarage(newOwnerInTheGarage);
                Console.WriteLine("The vehicle added to the garage successfuly!");
                Console.WriteLine("Press any key to get back to main menu...");
                Console.ReadKey();
                Console.Clear();
            }
        } //task 1

        public void PrintsTheChooseOfVehicle()
        {
            Console.Write("Please choose a kind of vehicle (number):" + Environment.NewLine);
            Console.Write("1. Motorcycle with a fuel engine" + Environment.NewLine);
            Console.Write("2. Motorcycle with an electric engine" + Environment.NewLine);
            Console.Write("3. Car with a fuel engine" + Environment.NewLine);
            Console.Write("4. Car with an electric engine" + Environment.NewLine);
            Console.Write("5. Truck with a fuel engine" + Environment.NewLine);
        } //task 1

        public void ValidationCheckForVehicleKind(int i_kindOfVehicle)
        {
            if (i_kindOfVehicle < 1 || i_kindOfVehicle > 5)
            {
                throw new ValueOutOfRangeException(1, 5);
            }
        } //task 1

        public void GetSpecificVehicleData(Vehicle i_Vehicle)
        {
            bool validInput;
            Type vehicleType = i_Vehicle.GetType();
            foreach (var property in vehicleType.GetProperties())
            {
                validInput = false;
                if (property.Name == "LicenseNumber" || property.Name == "ModelName"
                    || property.Name == "RemainEnergyPercent" || property.Name == "VehicleEngine" || property.Name == "WheelsList")
                {
                    continue;
                }

                string nameWithSpaces = FormatPropertyName(property.Name);
                Console.WriteLine("Please enter {0} value:", nameWithSpaces);

                while (!validInput)
                {
                    try
                    {
                        if (property.PropertyType == typeof(bool))
                        {
                            Console.WriteLine("Enter '0' - for NO and '1' - for YES: ");
                            string userInput = Console.ReadLine();
                            if (userInput != "1" && userInput != "0")
                            {
                                throw new ArgumentException(string.Format("Invalid input for {0}.", nameWithSpaces));
                            }
                            bool convertedValue = userInput == "1" ? true : false;
                            property.SetValue(i_Vehicle, convertedValue);
                        }
                        else if (property.PropertyType.IsEnum)
                        {
                            foreach (var enumValue in Enum.GetValues(property.PropertyType))
                            {
                                Console.WriteLine("({0}): {1}", (int)enumValue, enumValue);
                            }

                            string userInput = Console.ReadLine();
                            bool validEnumInput = false;
                            foreach (var enumValue in Enum.GetValues(property.PropertyType))
                            {
                                if (userInput == ((int)enumValue).ToString())
                                {
                                    validEnumInput = true;
                                    property.SetValue(i_Vehicle, enumValue);
                                    break;
                                }
                            }

                            if (!validEnumInput)
                            {
                                throw new ArgumentException(string.Format("Invalid input for {0}.", nameWithSpaces));
                            }
                        }
                        else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(float))
                        {
                            int userInput;

                            if (int.TryParse(Console.ReadLine(), out userInput))
                            {
                                if (userInput <= 0)
                                {
                                    throw new ArgumentException(string.Format("Invalid input for {0}.", nameWithSpaces));
                                }
                                property.SetValue(i_Vehicle, userInput);
                            }
                            else
                            {
                                throw new FormatException("Invalid input format. Please enter a valid number.");
                            }
                        }
                        else
                        {
                            string userInput = Console.ReadLine();
                            object value = Convert.ChangeType(userInput, property.PropertyType);
                            property.SetValue(i_Vehicle, value);
                        }

                        validInput = true;
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        } //task 1

        public string FormatPropertyName(string propertyName)
        {
            StringBuilder formattedName = new StringBuilder();

            foreach (char c in propertyName)
            {
                if (char.IsUpper(c) && formattedName.Length > 0)
                {
                    formattedName.Append(' ');
                }

                formattedName.Append(c);
            }

            return formattedName.ToString();
        } //task 1

        public void GetGeneralVehicleData(Vehicle i_Vehicle)
        {
            float energyPrecent;
            bool validEnergyPrecent = false;
            Console.Write("Please enter your vehicle model name: ");
            i_Vehicle.ModelName = Console.ReadLine();
            bool isFuelEngine = i_Vehicle.VehicleEngine.GetType().FullName.Contains("FuelEngine");
            Console.Write("Please enter your remain {0}: ", isFuelEngine ? "fuel" : "electric");

            while (!validEnergyPrecent)
            {
                try
                {
                    if (float.TryParse(Console.ReadLine(), out energyPrecent))
                    {
                        i_Vehicle.VehicleEngine.RemainEnergy = energyPrecent;
                        i_Vehicle.RemainEnergyPercent = (energyPrecent / i_Vehicle.VehicleEngine.MaxEnergy) * 100;
                        validEnergyPrecent = true;
                    }
                    else
                    {
                        throw new FormatException("Invalid input format. Please enter a valid number.");
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            EnterWHeelsData(i_Vehicle);
        } //task 1

        public void EnterWHeelsData(Vehicle i_Vehicle)
        {
            int userCHoiceForWHeels = 0;
            bool validChoice = false;

            Console.Write("Do you want to enter the information to all wheels at once or individual for each wheel?" + Environment.NewLine);
            Console.Write("(1) All wheels at once" + Environment.NewLine);
            Console.WriteLine("(2) individual for each wheel");

            while (!validChoice)
            {
                try
                {
                    if (int.TryParse(Console.ReadLine(), out userCHoiceForWHeels))
                    {
                        if (userCHoiceForWHeels != 1 && userCHoiceForWHeels != 2)
                        {
                            throw new ArgumentException("Choose between 1 or 2 only.");
                        }
                        validChoice = true;
                    }
                    else
                    {
                        throw new FormatException("Invalid input format. Please enter a valid number.");
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            validChoice = false;
            string producerWheelsName;
            float currentAirPressure;

            if (userCHoiceForWHeels == 1)
            {
                Console.Write("Please enter the wheels prodcer name: ");
                producerWheelsName = Console.ReadLine();
                Console.Write("Please enter the current wheels air pressure: ");

                while (!validChoice)
                {
                    try
                    {
                        if (float.TryParse(Console.ReadLine(), out currentAirPressure))
                        {
                            if (i_Vehicle.WheelsList.First().CheckingIfCurrentPressureValid(currentAirPressure))
                            {
                                foreach (var wheel in i_Vehicle.WheelsList)
                                {
                                    wheel.ProducerName = producerWheelsName;
                                    wheel.CurrentAirPressure = currentAirPressure;
                                }
                                validChoice = true;
                            }
                            else
                            {
                                throw new ValueOutOfRangeException(0, i_Vehicle.WheelsList.First().MaxAirPressure);
                            }
                        }
                        else
                        {
                            throw new FormatException("Invalid input format. Please enter a valid number.");
                        }
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    catch (ValueOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            else
            {
                for (int i = 0; i < i_Vehicle.WheelsList.Count; i++)
                {
                    Console.Write("Please enter the producer name for wheel {0}: ", i + 1);
                    producerWheelsName = Console.ReadLine();
                    validChoice = false;

                    while (!validChoice)
                    {
                        try
                        {
                            Console.Write("Please enter the current air pressure for wheel {0}: ", i + 1);
                            if (float.TryParse(Console.ReadLine(), out currentAirPressure))
                            {
                                i_Vehicle.WheelsList[i].ProducerName = producerWheelsName;
                                if (i_Vehicle.WheelsList[i].CheckingIfCurrentPressureValid(currentAirPressure))
                                {
                                    i_Vehicle.WheelsList[i].CurrentAirPressure = currentAirPressure;
                                    validChoice = true;
                                }
                                else
                                {
                                    throw new ValueOutOfRangeException(0, i_Vehicle.WheelsList[i].MaxAirPressure);
                                }
                            }
                            else
                            {
                                throw new FormatException("Invalid input format. Please enter a valid number.");
                            }
                        }
                        catch (FormatException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (ValueOutOfRangeException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        } //task 1

        public void DisplayListOfLicenseNumbers()
        {
            bool validChoice = false;
            int userCHoiceForDisplay = 0;

            DisplayOptionsForLicenseNumbers();

            while (!validChoice)
            {
                try
                {
                    if (int.TryParse(Console.ReadLine(), out userCHoiceForDisplay))
                    {
                        if (userCHoiceForDisplay < 1 && userCHoiceForDisplay > 4)
                        {
                            throw new ArgumentException("Choose between 1 - 4 only.");
                        }
                        validChoice = true;
                    }
                    else
                    {
                        throw new FormatException("Invalid input format. Please enter a valid number.");
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            Console.Clear();

            switch (userCHoiceForDisplay)
            {
                case 1:
                    PrintAllLicenseNumbers();
                    break;
                case 2:
                    PrintByCondition(eVehicleCondition.UnderRepair);
                    break;
                case 3:
                    PrintByCondition(eVehicleCondition.Fixed);
                    break;
                case 4:
                    PrintByCondition(eVehicleCondition.Paid);
                    break;
            }
        } //task 2

        public void DisplayOptionsForLicenseNumbers()
        {
            Console.Write("Please choose an option to display the license numbers:" + Environment.NewLine);
            Console.Write("1. All the vehicles in the garage" + Environment.NewLine);
            Console.Write("2. Only vehicles that are under 'Under Repair' condition" + Environment.NewLine);
            Console.Write("3. Only vehicles that are under 'Fixed' condition" + Environment.NewLine);
            Console.Write("4. Only vehicles that are under 'Paid' condition" + Environment.NewLine);
        } //task 2

        public void PrintByCondition(eVehicleCondition i_Condition)
        {
            Console.Clear();
            int countVehicles = 0;
            foreach (var vehicle in garage.vehicleOwnerDatas)
            {
                if (vehicle.Value.VehicleCondition.Equals(i_Condition))
                {
                    countVehicles++;
                    Console.WriteLine(string.Format("{0}. {1}", countVehicles, vehicle.Value.TheVehicle.LicenseNumber));
                }
            }
            if (countVehicles == 0)
            {
                Console.WriteLine(string.Format("There are no vehicles under '{0}' condition in the garage currently", i_Condition));
            }

            Console.WriteLine("Press any key to get back to main menu...");
            Console.ReadKey();
            Console.Clear();
        } //task 2

        public void PrintAllLicenseNumbers()
        {
            int countVehicles = 0;

            foreach (var vehicle in garage.vehicleOwnerDatas)
            {
                countVehicles++;
                Console.WriteLine("{0}. {1}", countVehicles, vehicle.Value.TheVehicle.LicenseNumber);
            }
            if (countVehicles == 0)
            {
                Console.WriteLine("There are no vehicles in the garage to present.");
            }
            Console.WriteLine("Press any key to get back to main menu...");
            Console.ReadKey();
            Console.Clear();
        } //task 2

        public void GetConditionAndLicenseNumberAndChangeCondition()
        {
            string licenseNumber;
            eVehicleCondition i_NewCondition;
            try
            {
                Console.Write("Please enter a license number: ");
                licenseNumber = GetLicenseNumberFromUser();

                Console.WriteLine("Please enter a condition: ");
                i_NewCondition = GetConditionFromUser();

                garage.ChangeVehicleCondition(licenseNumber, i_NewCondition);
                Console.WriteLine(string.Format("the vehilce with {0} license number is now in {1} condition!", licenseNumber, i_NewCondition));
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Press any key to get back to main menu...");
                Console.ReadKey();
                Console.Clear();
            }
        } //task 3 // mush

        public eVehicleCondition GetConditionFromUser()
        {
            int userChoise = 0;
            eVehicleCondition conditionThatTheUserChoose = 0;
            bool choiseIsValid = false;
            Console.Write("1. Under Repair" + Environment.NewLine);
            Console.Write("2. Fixed" + Environment.NewLine);
            Console.Write("3. Paid" + Environment.NewLine);

            while (!choiseIsValid)
            {
                try
                {
                    userChoise = int.Parse(Console.ReadLine());
                    switch (userChoise)
                    {
                        case 1:
                            conditionThatTheUserChoose = eVehicleCondition.UnderRepair;
                            break;
                        case 2:
                            conditionThatTheUserChoose = eVehicleCondition.Fixed;
                            break;
                        case 3:
                            conditionThatTheUserChoose = eVehicleCondition.Paid;
                            break;
                        default:
                            throw new ValueOutOfRangeException(1, 3);
                    }
                    choiseIsValid = true;
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return conditionThatTheUserChoose;
        } //task 3 // mush

        public string GetLicenseNumberFromUser()
        {
            string licenseNumber;
            licenseNumber = Console.ReadLine();

            if (!garage.CheckIfVehicleIsInTheGarage(licenseNumber))
            {
                throw new ArgumentException("The vehicle isn't in the garage at this moment. try again.");
            }

            return licenseNumber;
        }//all task

        public void InflatingWheelToMaximum()
        {
            string licenseNumber;
            try
            {
                Console.Write("Please enter a license number: ");
                licenseNumber = GetLicenseNumberFromUser();

                VehicleOwnerData vehicleOwnerData = garage.vehicleOwnerDatas[licenseNumber];

                foreach (var wheel in vehicleOwnerData.TheVehicle.WheelsList)
                {
                    wheel.InflatingAWheel(wheel.MaxAirPressure - wheel.CurrentAirPressure);
                }
                Console.WriteLine("All wheels have been inflated to their maximum air pressure.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Press any key to get back to main menu...");
                Console.ReadKey();
                Console.Clear();
            }

        }//task 4 // mush

        public void PresentsFullDataAccordingToLicenseNumber()
        {
            string licenseNumber;
            try
            {
                Console.Write("Please enter a license number: ");
                licenseNumber = GetLicenseNumberFromUser();

                VehicleOwnerData vehicleOwnerData = garage.vehicleOwnerDatas[licenseNumber];

                PrintGeneralData(vehicleOwnerData);
                PrintSpecificData(vehicleOwnerData);

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Press any key to get back to main menu...");
                Console.ReadKey();
                Console.Clear();
            }
        }//task7 //mush

        public void PrintGeneralData(VehicleOwnerData vehicleOwnerData)
        {
            Console.WriteLine("information- ");
            Console.WriteLine("License Number: {0}", vehicleOwnerData.TheVehicle.LicenseNumber);
            Console.WriteLine("Model: {0}", vehicleOwnerData.TheVehicle.ModelName);
            Console.WriteLine("Owner: {0}", vehicleOwnerData.OwnerName);
            Console.WriteLine("Condition: {0}", vehicleOwnerData.VehicleCondition);
            Console.WriteLine("Wheels data-");
            foreach (var wheel in vehicleOwnerData.TheVehicle.WheelsList)
            {
                Console.WriteLine("Producer: {0}", wheel.ProducerName);
                Console.WriteLine("Air pressure: {0}", wheel.CurrentAirPressure);
            }

            bool isFuelEngine = vehicleOwnerData.TheVehicle.VehicleEngine.GetType().FullName.Contains("FuelEngine");
            Console.Write("{0} condition: {1} {2} remain - ", isFuelEngine ? "fuel" : "electric",
                vehicleOwnerData.TheVehicle.VehicleEngine.RemainEnergy, isFuelEngine ? "liters" : "hours");
            Console.WriteLine("{0}%",vehicleOwnerData.TheVehicle.RemainEnergyPercent);
            if (isFuelEngine)
            {
                var fuelEngine = vehicleOwnerData.TheVehicle.VehicleEngine as FuelEngine;
                if (fuelEngine != null)
                {
                    Console.WriteLine("Fuel type: {0}", fuelEngine.FuelKind);
                }
            }
        }//task7 // mush

        public void PrintSpecificData(VehicleOwnerData vehicleOwnerData)
        {
            Type vehicleType = vehicleOwnerData.TheVehicle.GetType();

            foreach (var property in vehicleType.GetProperties())
            {
                string nameWithSpaces = FormatPropertyName(property.Name);

                if (property.Name == "LicenseNumber" || property.Name == "ModelName"
                || property.Name == "RemainEnergyPercent" || property.Name == "VehicleEngine" || property.Name == "WheelsList")
                {
                    continue;
                }

                Console.WriteLine("{0}: {1}", nameWithSpaces, property.GetValue(vehicleOwnerData.TheVehicle));
            }
        }//task7 // mush

        public void reRefueling()
        {
            string licenseNumber;
            try
            {
                Console.Write("Please enter a license number: ");
                licenseNumber = GetLicenseNumberFromUser();

            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Press any key to get back to main menu...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public void GetKindOfFuelFromUser()
        {
            Console.Write("Please choose a type of fuel to fill your vehicle:" + Environment.NewLine);
            Console.Write("1. {0}", eFuelType.Octan98 + Environment.NewLine);
            Console.Write("2. {1}", eFuelType.Octan95 + Environment.NewLine);
            Console.Write("3. {2}", eFuelType.Soler + Environment.NewLine);

        }
    }
}
        

