using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        List<VehicleOwnerData> i_VehiclesInTheGarage;
        
        public Garage()
        {
            i_VehiclesInTheGarage = new List<VehicleOwnerData>();
        }

        public bool CheckIfVehicleIsInTheGarage(string i_LicenseNumber)
        {
            bool foundVehicle = false;

            foreach (VehicleOwnerData item in i_VehiclesInTheGarage)
            {
                if (item.TheVehicle.LicenseNumber.Equals(i_LicenseNumber))
                {
                    foundVehicle = true;
                    break;
                }
            }
            return foundVehicle;
        }

        public void ChangeVehicleConditionToUnderRepair(string i_LicenseNumber)
        {
            foreach (VehicleOwnerData item in i_VehiclesInTheGarage)
            {
                if (item.TheVehicle.LicenseNumber.Equals(i_LicenseNumber))
                {
                    item.VehicleCondition = eVehicleCondition.UnderRepair;
                    break;
                }
            }
        }


        //    public Dictionary<string, string>  chooseDictionery(string i_KindVehicle)
        //    {
        //        Dictionary<string, string> vehicleQuestions = new Dictionary<string, string>();
        //        switch (i_KindVehicle.ToLower())
        //        {
        //            case "car":
        //                vehicleQuestions = Car.GetVehicleQuestions();
        //                break;
        //            case "motorcycle":
        //                vehicleQuestions = Motorcycle.GetVehicleQuestions();
        //                break;
        //            case "truck":
        //                vehicleQuestions = Truck.GetVehicleQuestions();
        //                break;
        //            default:
        //                throw new ArgumentException("Invalid vehicle type.");
        //        }
        //        return vehicleQuestions;
        //    }
    }
}
