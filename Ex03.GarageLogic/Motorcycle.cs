using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Motorcycle : Vehicle
    {
        private eMotorcycleLicenseType m_LicenseType;
        private int m_EngineVolumeCC;

        public eMotorcycleLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }
        public int EngineVolumeCC
        {
            get { return m_EngineVolumeCC; }
            set { m_EngineVolumeCC = value; }
        }


        public Motorcycle(Engine i_Engine) : base(2, 33, i_Engine) { }

    }
}
