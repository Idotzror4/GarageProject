using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class VehicleOwnerData
    {
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private eVehicleCondition m_VehicleCondition;
        private Vehicle m_Vehicle;

        public VehicleOwnerData(string i_OwnerName, string i_OwnerPhoneNumber)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_VehicleCondition = eVehicleCondition.UnderRepair;
        }
        public Vehicle TheVehicle
        {
            get { return m_Vehicle; }
            set { m_Vehicle = value; }
        }
        public eVehicleCondition VehicleCondition
        {
            get { return m_VehicleCondition; }
            set { m_VehicleCondition = value; }
        }

        public string OwnerName
        {
            get { return m_OwnerName; }
            set { m_OwnerName = value; }
        }
    }
}
