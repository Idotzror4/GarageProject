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
        private const int k_TruckWheelsNumber = 12;
        private const int k_TruckMaxAirPressure = 28;

        public bool TransportsHazardousMaterial
        {
            get { return m_TransportsHazardousMaterial; }
            set { m_TransportsHazardousMaterial = value; }
        }
        public float CargoVolume
        {
            get { return m_CargoVolume; }
            set { m_CargoVolume = value; }
        }
        public Truck(Engine i_Engine) : base(k_TruckWheelsNumber, 
                                             k_TruckMaxAirPressure, i_Engine) { }
    }
}
