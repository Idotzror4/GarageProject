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
        Dictionary<string, VehicleOwnerData> i_VehiclesInTheGarage;

        public Dictionary<string, VehicleOwnerData> vehicleOwnerDatas
        {
            get { return i_VehiclesInTheGarage; }
        }

        public Garage()
        {
            i_VehiclesInTheGarage = new Dictionary<string, VehicleOwnerData>();
        }

        public bool CheckIfVehicleIsInTheGarage(string i_LicenseNumber)
        {
            return i_VehiclesInTheGarage.ContainsKey(i_LicenseNumber);
        }

        public void ChangeVehicleCondition(string i_LicenseNumber, eVehicleCondition i_NewCondition)
        {
            if (CheckIfVehicleIsInTheGarage(i_LicenseNumber))
            {
                i_VehiclesInTheGarage[i_LicenseNumber].VehicleCondition = i_NewCondition;
            }
            else
            {
                throw new ArgumentException("Vehicle with license number {0} is not found in the garage.", i_LicenseNumber);
            }
        }

        public void AddVehicleToGarage(VehicleOwnerData i_VehicleOwnerData)
        {
            i_VehiclesInTheGarage.Add(i_VehicleOwnerData.TheVehicle.LicenseNumber, i_VehicleOwnerData);
        }
    }
}
