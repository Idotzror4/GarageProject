using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Factory
    {
        private const float k_MaxMotorcycleFuelTank = 5.5f;
        private const float k_MaxMotorcycleBattery = 2.5f;
        private const float k_MaxCarFuelTank = 45;
        private const float k_MaxCarBattery = 3.5f;
        private const float k_MaxTruckFuelTank = 120;

        public static Vehicle CreateNewVehicle(int i_NumberOfChoice)
        {
            Vehicle vehicle;

            switch (i_NumberOfChoice)
            {
                case 1:
                    vehicle = new Motorcycle(new FuelEngine(k_MaxMotorcycleFuelTank, eFuelType.Octan98));
                    break;
                case 2:
                    vehicle = new Motorcycle(new ElectricEngine(k_MaxMotorcycleBattery));
                    break;
                case 3:
                    vehicle = new Car(new FuelEngine(k_MaxCarFuelTank, eFuelType.Octan95));
                    break;
                case 4:
                    vehicle = new Car(new ElectricEngine(k_MaxCarBattery));
                    break;
                case 5:
                    vehicle = new Truck(new FuelEngine(k_MaxTruckFuelTank, eFuelType.Soler));
                    break;
                default:
                    throw new ArgumentException("Invalid vehicle type.");
            }

            return vehicle;
        }
    }
}
