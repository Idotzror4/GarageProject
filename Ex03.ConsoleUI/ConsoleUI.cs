using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
        private const int k_MinVehicleChoice = 1;
        private const int k_MaxVehicleChoice = 5;
        private const int k_MinLicensesDisplayChoice = 1;
        private const int k_MaxLicensesDisplayChoice = 4;

        public void RunTheGarage() //done
        {
            while (true)
            {
                printMenu();

                int actionChoice = chooseAction();

                switch (actionChoice)
                {
                    case 1:
                        getDataFromNewVehicle();
                        break;
                    case 2:
                        displayListOfLicenseNumbers();
                        break;
                    case 3:
                        getConditionAndLicenseNumberAndChangeCondition();
                        break;
                    case 4:
                        inflatingWheelToMaximum();
                        break;
                    case 5:
                        refueling();
                        break;
                    case 6:
                        batteryCharging();
                        break;
                    case 7:
                        presentsFullDataAccordingToLicenseNumber();
                        break;
                }
            }
        }

        private void printMenu() //done
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

        private int chooseAction() //done
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

        private void getDataFromNewVehicle()
        {
            int kindOfVehicle;
            string licenseNumber;
            bool validInput = false;

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
                kindOfVehicle = vehicleChoiceVlidationCheck(validInput);

                garage.CreateNewVehicleFromTheGarage(newOwnerInTheGarage, kindOfVehicle);
                newOwnerInTheGarage.TheVehicle.LicenseNumber = licenseNumber;

                getGeneralVehicleData(newOwnerInTheGarage.TheVehicle);
                getSpecificVehicleData(newOwnerInTheGarage.TheVehicle);
                garage.AddVehicleToGarage(newOwnerInTheGarage);

                Console.WriteLine("The vehicle added to the garage successfuly!");
            }
            pressAnyKeyToGetBackToMenu();
        } //task 1 //done

        private int vehicleChoiceVlidationCheck(bool i_ValidInput)
        {
            int kindOfVehicle = 0;

            while (!i_ValidInput)
            {
                try
                {
                    if (int.TryParse(Console.ReadLine(), out kindOfVehicle))
                    {
                        validationCheckForVehicleKind(kindOfVehicle);
                        i_ValidInput = true;
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

            return kindOfVehicle;
        } //done

        private void PrintsTheChooseOfVehicle()
        {
            Console.Write("Please choose a kind of vehicle (number):" + Environment.NewLine);
            Console.Write("1. Motorcycle with a fuel engine" + Environment.NewLine);
            Console.Write("2. Motorcycle with an electric engine" + Environment.NewLine);
            Console.Write("3. Car with a fuel engine" + Environment.NewLine);
            Console.Write("4. Car with an electric engine" + Environment.NewLine);
            Console.Write("5. Truck with a fuel engine" + Environment.NewLine);
        } //task 1 //done

        private void validationCheckForVehicleKind(int i_KindOfVehicle)
        {
            if (i_KindOfVehicle < k_MinVehicleChoice || i_KindOfVehicle > k_MaxVehicleChoice)
            {
                throw new ValueOutOfRangeException(k_MinVehicleChoice, k_MaxVehicleChoice);
            }
        } //task 1 //done

        private void getSpecificVehicleData(Vehicle i_Vehicle)
        {
            bool validInput;
            Type vehicleType = i_Vehicle.GetType();

            foreach (var property in vehicleType.GetProperties())
            {
                System.Reflection.PropertyInfo temporarlyProperty = property;
                validInput = false;
                if (property.Name == "LicenseNumber" || property.Name == "ModelName"
                    || property.Name == "RemainEnergyPercent" || property.Name == "VehicleEngine" || property.Name == "WheelsList")
                {
                    continue;
                }

                string nameWithSpaces = formatPropertyName(property.Name);
                Console.WriteLine("Please enter {0} value:", nameWithSpaces);
                while (!validInput)
                {
                    try
                    {
                        if (property.PropertyType == typeof(bool))
                        {
                            handleBoolPropertyType(ref temporarlyProperty, i_Vehicle, nameWithSpaces);
                        }
                        else if (property.PropertyType.IsEnum)
                        {
                            handleEnumPropertyType(ref temporarlyProperty, i_Vehicle, nameWithSpaces);
                        }
                        else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(float))
                        {
                            handleFloatIntPropertyType(ref temporarlyProperty, i_Vehicle, nameWithSpaces);
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
        } //task 1 //done

        private void handleBoolPropertyType(ref System.Reflection.PropertyInfo io_Property, Vehicle i_Vehicle, string i_NameWithSpaces)
        {
            Console.WriteLine("Enter '0' - for NO and '1' - for YES: ");
            string userInput = Console.ReadLine();

            if (userInput != "1" && userInput != "0")
            {
                throw new ArgumentException(string.Format("Invalid input for {0}.", i_NameWithSpaces));
            }

            bool convertedValue = userInput == "1" ? true : false;
            io_Property.SetValue(i_Vehicle, convertedValue);
        } //task1 //done

        private void handleEnumPropertyType(ref System.Reflection.PropertyInfo io_Property, Vehicle i_Vehicle, string i_NameWithSpaces)
        {
            bool validEnumInput = false;
            string userInput;

            foreach (var enumValue in Enum.GetValues(io_Property.PropertyType))
            {
                Console.WriteLine("({0}): {1}", (int)enumValue, enumValue);
            }

            userInput = Console.ReadLine();

            foreach (var enumValue in Enum.GetValues(io_Property.PropertyType))
            {
                if (userInput == ((int)enumValue).ToString())
                {
                    validEnumInput = true;
                    io_Property.SetValue(i_Vehicle, enumValue);
                    break;
                }
            }

            if (!validEnumInput)
            {
                throw new ArgumentException(string.Format("Invalid input for {0}.", i_NameWithSpaces));
            }
        }//task 1 //done

        private void handleFloatIntPropertyType(ref System.Reflection.PropertyInfo io_Property, Vehicle i_Vehicle, string i_NameWithSpaces)
        {
            int userInput;

            if (int.TryParse(Console.ReadLine(), out userInput))
            {
                if (userInput <= 0)
                {
                    throw new ArgumentException(string.Format("Invalid input for {0}.", i_NameWithSpaces));
                }
                io_Property.SetValue(i_Vehicle, userInput);
            }
            else
            {
                throw new FormatException("Invalid input format. Please enter a valid number.");
            }
        }//task1 //done

        private string formatPropertyName(string i_PropertyName)
        {
            StringBuilder formattedName = new StringBuilder();

            foreach (char c in i_PropertyName)
            {
                if (char.IsUpper(c) && formattedName.Length > 0)
                {
                    formattedName.Append(' ');
                }

                formattedName.Append(c);
            }

            return formattedName.ToString();
        } //task 1 //done

        private void getGeneralVehicleData(Vehicle i_Vehicle)
        {
            float energyRemain;
            bool validEnergyPrecent = false;

            Console.Write("Please enter your vehicle model name: ");
            i_Vehicle.ModelName = Console.ReadLine();

            bool isFuelEngine = i_Vehicle.VehicleEngine.GetType().FullName.Contains("FuelEngine");
            Console.Write("Please enter your remain {0}: ", isFuelEngine ? "fuel" : "electric");

            while (!validEnergyPrecent)
            {
                try
                {
                    if (float.TryParse(Console.ReadLine(), out energyRemain))
                    {
                        if (energyRemain <= i_Vehicle.VehicleEngine.MaxEnergy)
                        {
                            i_Vehicle.VehicleEngine.RemainEnergy = energyRemain;
                            i_Vehicle.RemainEnergyPercent = (energyRemain / i_Vehicle.VehicleEngine.MaxEnergy) * 100;
                            validEnergyPrecent = true;
                        }
                        else
                        {
                            throw new ValueOutOfRangeException(0, i_Vehicle.VehicleEngine.MaxEnergy);
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

            enterWheelsData(i_Vehicle);
        } //task 1 //done

        private void enterWheelsData(Vehicle i_Vehicle)
        {
            int userCHoiceForWHeels = 0;
            bool validChoice = false;
            string producerWheelsName = string.Empty;
            float currentAirPressure = 0;

            Console.Write("Do you want to enter the information to all wheels at once or individual for each wheel?" + Environment.NewLine);
            Console.Write("(1) All wheels at once" + Environment.NewLine);
            Console.WriteLine("(2) individual for each wheel");
            choosingWheelsDataWayValidation(ref validChoice, i_Vehicle, ref userCHoiceForWHeels);
            validChoice = false;

            if (userCHoiceForWHeels == 1)
            {
                Console.Write("Please enter the wheels prodcer name: ");
                producerWheelsName = Console.ReadLine();
                Console.Write("Please enter the current wheels air pressure: ");
                putDataAtAllTheWheelsAtOnce(ref validChoice, i_Vehicle, ref currentAirPressure, producerWheelsName);
            }
            else
            {
                putDataAtEachWheelIndividualy(ref validChoice, i_Vehicle, ref currentAirPressure, producerWheelsName);
            }
        } //task 1 //done

        private void choosingWheelsDataWayValidation(ref bool io_ValidChoice ,Vehicle i_Vehicle, ref int io_UserCHoiceForWHeels)
        {
            while (!io_ValidChoice)
            {
                try
                {
                    if (int.TryParse(Console.ReadLine(), out io_UserCHoiceForWHeels))
                    {
                        if (io_UserCHoiceForWHeels != 1 && io_UserCHoiceForWHeels != 2)
                        {
                            throw new ArgumentException("Choose between 1 or 2 only.");
                        }

                        io_ValidChoice = true;
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
        }//task 1 //done

        private void putDataAtAllTheWheelsAtOnce(ref bool io_ValidChoice, Vehicle i_Vehicle, ref float io_CurrentAirPressure, string i_ProducerWheelsName)
        {
            while (!io_ValidChoice)
            {
                try
                {
                    if (float.TryParse(Console.ReadLine(), out io_CurrentAirPressure))
                    {
                        if (i_Vehicle.WheelsList.First().CheckingIfCurrentPressureValid(io_CurrentAirPressure))
                        {
                            foreach (var wheel in i_Vehicle.WheelsList)
                            {
                                wheel.ProducerName = i_ProducerWheelsName;
                                wheel.CurrentAirPressure = io_CurrentAirPressure;
                            }

                            io_ValidChoice = true;
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
        }//task1 //done

        private void putDataAtEachWheelIndividualy(ref bool io_ValidChoice, Vehicle i_Vehicle, ref float io_CurrentAirPressure, string i_ProducerWheelsName)
        {
            for (int i = 0; i < i_Vehicle.WheelsList.Count; i++)
            {
                io_ValidChoice = false;

                Console.Write("Please enter the producer name for wheel {0}: ", i + 1);
                i_ProducerWheelsName = Console.ReadLine();
                while (!io_ValidChoice)
                {
                    try
                    {
                        Console.Write("Please enter the current air pressure for wheel {0}: ", i + 1);
                        if (float.TryParse(Console.ReadLine(), out io_CurrentAirPressure))
                        {
                            i_Vehicle.WheelsList[i].ProducerName = i_ProducerWheelsName;
                            if (i_Vehicle.WheelsList[i].CheckingIfCurrentPressureValid(io_CurrentAirPressure))
                            {
                                i_Vehicle.WheelsList[i].CurrentAirPressure = io_CurrentAirPressure;
                                io_ValidChoice = true;
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
        }//task1 //done

        private void displayListOfLicenseNumbers()
        {
            bool validChoice = false;
            int userCHoiceForDisplay = 0;

            displayOptionsForLicenseNumbers();
            while (!validChoice)
            {
                try
                {
                    if (int.TryParse(Console.ReadLine(), out userCHoiceForDisplay))
                    {
                        if (userCHoiceForDisplay < k_MinLicensesDisplayChoice || userCHoiceForDisplay > k_MaxLicensesDisplayChoice)
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
            printLicenseNumbersWays(userCHoiceForDisplay);
        } //task 2 //done

        private void printLicenseNumbersWays(int i_UserChoiceForDisplay)
        {
            switch (i_UserChoiceForDisplay)
            {
                case 1:
                    printAllLicenseNumbers();
                    break;
                case 2:
                    printByCondition(eVehicleCondition.UnderRepair);
                    break;
                case 3:
                    printByCondition(eVehicleCondition.Fixed);
                    break;
                case 4:
                    printByCondition(eVehicleCondition.Paid);
                    break;
            }
        }//task 2 //done

        private void displayOptionsForLicenseNumbers()
        {
            Console.Write("Please choose an option to display the license numbers:" + Environment.NewLine);
            Console.Write("1. All the vehicles in the garage" + Environment.NewLine);
            Console.Write("2. Only vehicles that are under 'Under Repair' condition" + Environment.NewLine);
            Console.Write("3. Only vehicles that are under 'Fixed' condition" + Environment.NewLine);
            Console.Write("4. Only vehicles that are under 'Paid' condition" + Environment.NewLine);
        } //task 2 //done

        private void printByCondition(eVehicleCondition i_Condition)
        {
            Console.Clear();
            int countVehicles = 0;

            foreach (var vehicle in garage.VehicleOwnerDatas)
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

            pressAnyKeyToGetBackToMenu();
        } //task 2 //done

        private void printAllLicenseNumbers()
        {
            int countVehicles = 0;

            foreach (var vehicle in garage.VehicleOwnerDatas)
            {
                countVehicles++;
                Console.WriteLine("{0}. {1}", countVehicles, vehicle.Value.TheVehicle.LicenseNumber);
            }

            if (countVehicles == 0)
            {
                Console.WriteLine("There are no vehicles in the garage to present.");
            }

            pressAnyKeyToGetBackToMenu();
        } //task 2 //done

        private void getConditionAndLicenseNumberAndChangeCondition()
        {
            string licenseNumber;
            eVehicleCondition newCondition;

            try
            {
                Console.Write("Please enter a license number: ");
                licenseNumber = getLicenseNumberFromUser();

                Console.WriteLine("Please enter a condition: ");
                newCondition = getConditionFromUser();

                garage.ChangeVehicleCondition(licenseNumber, newCondition);
                Console.WriteLine(string.Format("the vehilce with {0} license number is now in {1} condition!", licenseNumber, newCondition));
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                pressAnyKeyToGetBackToMenu();
            }
        } //task 3 // done

        private eVehicleCondition getConditionFromUser()
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
        } //task 3 // done

        private string getLicenseNumberFromUser()
        {
            string licenseNumber;
            licenseNumber = Console.ReadLine();

            if (!garage.CheckIfVehicleIsInTheGarage(licenseNumber))
            {
                throw new ArgumentException("The vehicle isn't in the garage at this moment. try again.");
            }

            return licenseNumber;
        }//all task // done

        private void inflatingWheelToMaximum()
        {
            string licenseNumber;

            try
            {
                Console.Write("Please enter a license number: ");
                licenseNumber = getLicenseNumberFromUser();
                VehicleOwnerData vehicleOwnerData = garage.VehicleOwnerDatas[licenseNumber];
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
                pressAnyKeyToGetBackToMenu();
            }

        }//task 4 // done

        private void presentsFullDataAccordingToLicenseNumber()
        {
            string licenseNumber;

            try
            {
                Console.Write("Please enter a license number: ");
                licenseNumber = getLicenseNumberFromUser();
                VehicleOwnerData vehicleOwnerData = garage.VehicleOwnerDatas[licenseNumber];
                printGeneralData(vehicleOwnerData);
                printSpecificData(vehicleOwnerData);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                pressAnyKeyToGetBackToMenu();
            }
        }//task7 //done

        private void printGeneralData(VehicleOwnerData i_VehicleOwnerData)
        {
            Console.WriteLine("information- ");
            Console.WriteLine("License Number: {0}", i_VehicleOwnerData.TheVehicle.LicenseNumber);
            Console.WriteLine("Model: {0}", i_VehicleOwnerData.TheVehicle.ModelName);
            Console.WriteLine("Owner: {0}", i_VehicleOwnerData.OwnerName);
            Console.WriteLine("Condition: {0}", i_VehicleOwnerData.VehicleCondition);
            Console.WriteLine("Wheels data-");

            foreach (var wheel in i_VehicleOwnerData.TheVehicle.WheelsList)
            {
                Console.WriteLine("Producer: {0}", wheel.ProducerName);
                Console.WriteLine("Air pressure: {0}", wheel.CurrentAirPressure);
            }

            bool isFuelEngine = i_VehicleOwnerData.TheVehicle.VehicleEngine.GetType().FullName.Contains("FuelEngine");
            Console.Write("{0} condition: {1} {2} remain - ", isFuelEngine ? "fuel" : "electric",
                            i_VehicleOwnerData.TheVehicle.VehicleEngine.RemainEnergy, isFuelEngine ? "liters" : "hours");
            Console.WriteLine("{0}%",i_VehicleOwnerData.TheVehicle.RemainEnergyPercent);

            if (isFuelEngine)
            {
                var fuelEngine = i_VehicleOwnerData.TheVehicle.VehicleEngine as FuelEngine;
                if (fuelEngine != null)
                {
                    Console.WriteLine("Fuel type: {0}", fuelEngine.FuelKind);
                }
            }
        }//task7 // done

        private void printSpecificData(VehicleOwnerData i_VehicleOwnerData)
        {
            Type vehicleType = i_VehicleOwnerData.TheVehicle.GetType();

            foreach (var property in vehicleType.GetProperties())
            {
                string nameWithSpaces = formatPropertyName(property.Name);

                if (property.Name == "LicenseNumber" || property.Name == "ModelName"
                       || property.Name == "RemainEnergyPercent" || property.Name == "VehicleEngine" || property.Name == "WheelsList")
                {
                    continue;
                }

                Console.WriteLine("{0}: {1}", nameWithSpaces, property.GetValue(i_VehicleOwnerData.TheVehicle));
            }
        }//task7 // done

        private void refueling()
        {
            string licenseNumber;
            float amountOfFuel;
            eFuelType eFuelType;

            try
            {
                Console.Write("Please enter a license number: ");
                licenseNumber = getLicenseNumberFromUser();
                eFuelType = getKindOfFuelFromUser();
                amountOfFuel = getAmountOfEnergyFromUser();
                garage.CheckIfTheFuelSuitableForVehicle(licenseNumber, eFuelType, amountOfFuel);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ValueOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                pressAnyKeyToGetBackToMenu();
            }
        } // task 5 // done

        private void batteryCharging()
        {
            string licenseNumber;
            float amountOfElectric;

            try
            {
                Console.Write("Please enter a license number: ");
                licenseNumber = getLicenseNumberFromUser();
                amountOfElectric = getAmountOfEnergyFromUser();
                garage.CheckIfTheElectricSuitableForVehicle(licenseNumber, amountOfElectric);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ValueOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                pressAnyKeyToGetBackToMenu();
            }
        } // task 6 //done

        private eFuelType getKindOfFuelFromUser()
        {
            bool validInput = false;
            int valueFromUser = 0;
            eFuelType eFuelType = 0;

            Console.Write("Please choose a type of fuel to fill your vehicle:" + Environment.NewLine);
            Console.Write("1. {0}", eFuelType.Octan98 + Environment.NewLine);
            Console.Write("2. {0}", eFuelType.Octan95 + Environment.NewLine);
            Console.Write("3. {0}", eFuelType.Soler + Environment.NewLine);
            while (!validInput)
            {
                try
                {
                    valueFromUser = int.Parse(Console.ReadLine());
                    if (valueFromUser < 1 || valueFromUser > 3)
                    {
                        throw new ValueOutOfRangeException(1, 3);
                    }
                    else
                    {
                        eFuelType = (eFuelType)valueFromUser;
                        validInput = true;
                    }
                }
                catch(ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return eFuelType;
        } // task 5 //done

        private float getAmountOfEnergyFromUser()
        {
            bool validInput = false;
            float valueFromUser = 0;

            Console.WriteLine("Please insert an amount of energy:");
            while (!validInput)
            {
                try
                {
                    if (!float.TryParse(Console.ReadLine(), out valueFromUser))
                    {
                        throw new FormatException("Invalid input format. Please enter a valid number.");
                    }
                    if (valueFromUser <= 0)
                    {
                        throw new ArgumentException("Energy amount must be a positive number.");
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

            return valueFromUser;
        } // task 5 + 6 //done

        private void pressAnyKeyToGetBackToMenu() //all tasks //done
        {
            Console.WriteLine("Press any key to get back to main menu...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
        

