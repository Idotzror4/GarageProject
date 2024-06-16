using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle
    {
        private bool m_TransportsHazardousMaterial;
        private float m_CargoVolume;

        public Truck(string i_ModelName, string i_LicenseNumber, float i_RemainEnergyPercent, float i_CurrentAirPressure,
                     Engine i_Engine, bool i_TransportsHazardousMaterial, float i_CargoVolume)
          : base(i_ModelName, i_LicenseNumber, i_RemainEnergyPercent, 12, 28, i_CurrentAirPressure, i_Engine)
        {
            m_TransportsHazardousMaterial = i_TransportsHazardousMaterial;
            m_CargoVolume = i_CargoVolume;
        }
    }
}
