using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Motorcycle : Vehicle
    {
        private string m_LicenseType;
        private int m_EngineVolumeCC;

        public Motorcycle(string i_ModelName, string i_LicenseNumber, float i_RemainEnergyPrecent, float i_CurrentAirPressure, 
                          Engine i_Engine, string i_LicenseType, int i_EngineVolumeCC)
           : base(i_ModelName, i_LicenseNumber, i_RemainEnergyPrecent, 2, 33, i_CurrentAirPressure, i_Engine)
        {
            m_LicenseType = i_LicenseType;
            m_EngineVolumeCC = i_EngineVolumeCC;
        }
    }
}
