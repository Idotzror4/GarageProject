using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Factory
    {
        public static Vehicle CreateNewVehicle(int numberOfChoice)
        {
            Vehicle vehicle;
            switch (numberOfChoice)
            {
                case 1:
                    vehicle = new Motorcycle(new FuelEngine(5.5f, eFuelType.Octan98));
                    break;
                case 2:
                    vehicle = new Motorcycle(new ElectricEngine(2.5f));
                    break;
                case 3:
                    vehicle = new Car(new FuelEngine(45, eFuelType.Octan95));
                    break;
                case 4:
                    vehicle = new Car(new ElectricEngine(3.5f));
                    break;
                case 5:
                    vehicle = new Truck(new FuelEngine(120f, eFuelType.Soler));
                    break;
                default:
                    throw new ArgumentException("Invalid vehicle type.");
            }
            return vehicle;
        }
    }
}
