using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Garage
    {
        internal struct VehicleOwnerData
        {
            string m_OwnerName;
            string m_OwnerPhoneNumber;
            eVehicleCondition m_VehicleCondition;
            Vehicle m_Vehicle;
           
            public VehicleOwnerData(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_Vehicle)
            {
                m_OwnerName = i_OwnerName;
                m_OwnerPhoneNumber = i_OwnerPhoneNumber;
                m_VehicleCondition = eVehicleCondition.UnderRepair;
                m_Vehicle = i_Vehicle;
            }
        }
        List<VehicleOwnerData> i_VehiclesInTheGarage;

        public Garage()
        {
            i_VehiclesInTheGarage = new List<VehicleOwnerData>();
        }
    }
}
