using Ex03.GarageLogic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    internal class ConsoleUI
    {
        Garage garage = new GarageLogic.Garage();

        public void GetDataFromNewVehicle()
        {
            int kindOfVehicle = 0;
            string licenseNumber;

            Console.Write("Please enter a license number: ");
            licenseNumber = Console.ReadLine();
            if (garage.CheckIfVehicleIsInTheGarage(licenseNumber))
            {
                garage.ChangeVehicleConditionToUnderRepair(licenseNumber);
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
            }
        }

        public void PrintsTheChooseOfVehicle()
        {
            Console.Write("Please choose a kind of vehicle (number):" + Environment.NewLine);
            Console.Write("1. Motorcycle with a fuel engine" + Environment.NewLine);
            Console.Write("2. Motorcycle with an electric engine" + Environment.NewLine);
            Console.Write("3. Car with a fuel engine" + Environment.NewLine);
            Console.Write("4. Car with an electric engine" + Environment.NewLine);
            Console.Write("5. Truck with a fuel engine" + Environment.NewLine);
        }

        public void ValidationCheckForVehicleKind(int i_kindOfVehicle)
        {
            if (i_kindOfVehicle < 1 || i_kindOfVehicle > 5)
            {
                throw new ValueOutOfRangeException(1, 5);
            }
        }

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
        }

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
        }

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
        }

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
                Console.Write("Please enter the current wheels air pressure name: ");

                while (!validChoice)
                {
                    try
                    {
                        if (float.TryParse(Console.ReadLine(), out currentAirPressure))
                        {
                            if(i_Vehicle.WheelsList.First().CheckingIfCurrentPressureValid(currentAirPressure))
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
        }
    }
}
        

