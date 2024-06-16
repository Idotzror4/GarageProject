using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Car : Vehicle
    {
        private string m_CarColor;
        private int m_DoorsNumbers;

        public Car(string i_ModelName, string i_LicenseNumber, float i_RemainEnergyPercent, int i_CurrentAirPressure, 
                   Engine i_Engine, string i_CarColor, int i_DoorsNumbers)
          : base(i_ModelName, i_LicenseNumber, i_RemainEnergyPercent, 5, 31, i_CurrentAirPressure, i_Engine)
        {
            m_CarColor = i_CarColor;
            m_DoorsNumbers = i_DoorsNumbers;
        }
    }
}
