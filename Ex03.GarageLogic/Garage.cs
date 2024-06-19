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
        Dictionary<string, VehicleOwnerData> m_VehiclesInTheGarage;

        public Dictionary<string, VehicleOwnerData> VehicleOwnerDatas
        {
            get { return m_VehiclesInTheGarage; }
        }

        public Garage()
        {
            m_VehiclesInTheGarage = new Dictionary<string, VehicleOwnerData>();
        }

        public bool CheckIfVehicleIsInTheGarage(string i_LicenseNumber)
        {
            return m_VehiclesInTheGarage.ContainsKey(i_LicenseNumber);
        }

        public void ChangeVehicleCondition(string i_LicenseNumber, eVehicleCondition i_NewCondition)
        {
            if (CheckIfVehicleIsInTheGarage(i_LicenseNumber))
            {
                m_VehiclesInTheGarage[i_LicenseNumber].VehicleCondition = i_NewCondition;
            }
            else
            {
                throw new ArgumentException("Vehicle with license number {0} is not found in the garage.", i_LicenseNumber);
            }
        }

        public void AddVehicleToGarage(VehicleOwnerData i_VehicleOwnerData)
        {
            m_VehiclesInTheGarage.Add(i_VehicleOwnerData.TheVehicle.LicenseNumber, i_VehicleOwnerData);
        }

        public void CheckIfTheFuelSuitableForVehicle(string i_LicenseNumber,
                                                           eFuelType i_FuelKind, float i_AmountOfFuel)
        {
            VehicleOwnerData vehicleOwnerData = VehicleOwnerDatas[i_LicenseNumber];

            if (vehicleOwnerData.TheVehicle.VehicleEngine is FuelEngine)
            {
                vehicleOwnerData.TheVehicle.AddEnergy(i_AmountOfFuel, i_FuelKind);
                vehicleOwnerData.TheVehicle.RemainEnergyPercent =
                    vehicleOwnerData.TheVehicle.VehicleEngine.RemainEnergy /
                         (vehicleOwnerData.TheVehicle.VehicleEngine.MaxEnergy) * 100;
            }
            else
            {
                throw new ArgumentException("The vehicle engine is not a fuel engine.");
            }
        }

        public void CheckIfTheElectricSuitableForVehicle(string i_LicenseNumber, float i_AmountOfFuel)
        {
            VehicleOwnerData vehicleOwnerData = VehicleOwnerDatas[i_LicenseNumber];

            if (vehicleOwnerData.TheVehicle.VehicleEngine is ElectricEngine)
            {
                vehicleOwnerData.TheVehicle.AddEnergy(i_AmountOfFuel);
                vehicleOwnerData.TheVehicle.RemainEnergyPercent =
                    vehicleOwnerData.TheVehicle.VehicleEngine.RemainEnergy /
                         (vehicleOwnerData.TheVehicle.VehicleEngine.MaxEnergy) * 100;
            }
            else
            {
                throw new ArgumentException("The vehicle engine is not an electric engine.");
            }
        }

        public void CreateNewVehicleFromTheGarage(VehicleOwnerData newOwnerInTheGarage, int kindOfVehicle)
        {
            newOwnerInTheGarage.TheVehicle = Factory.CreateNewVehicle(kindOfVehicle);
        }
    }
}
