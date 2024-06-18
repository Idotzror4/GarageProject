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

        public List<VehicleOwnerData> vehicleOwnerDatas
        {
            get { return i_VehiclesInTheGarage; }
        }

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

        public void ChangeVehicleCondition(string i_LicenseNumber, eVehicleCondition i_NewCondition)
        {
            foreach (VehicleOwnerData item in i_VehiclesInTheGarage)
            {
                if (item.TheVehicle.LicenseNumber.Equals(i_LicenseNumber))
                {
                    item.VehicleCondition = i_NewCondition;
                    break;
                }
            }
        }

        public void AddVehicleToGarage(VehicleOwnerData i_VehicleOwnerData)
        {
            i_VehiclesInTheGarage.Add(i_VehicleOwnerData);
        }
    }
}
