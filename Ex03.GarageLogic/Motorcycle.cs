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


        public Motorcycle(Engine i_Engine) : base(2, 33, i_Engine)
        {

        }



        //public Motorcycle(string i_ModelName, string i_LicenseNumber, float i_RemainEnergyPrecent, float i_CurrentAirPressure, 
        //                  Engine i_Engine, string i_LicenseType, int i_EngineVolumeCC)
        //   : base(i_ModelName, i_LicenseNumber, i_RemainEnergyPrecent, 2, 33, i_CurrentAirPressure, i_Engine)
        //{
        //    m_LicenseType = i_LicenseType;
        //    m_EngineVolumeCC = i_EngineVolumeCC;
        //}

        //public static Dictionary<string, string> GetVehicleQuestions()
        //{
        //    Dictionary<string, string> questionsData = Vehicle.GetVehicleQuestions();
        //    questionsData.Add("Enter license type: ", null);
        //    questionsData.Add("Enter engine volume (CC): ", null);

        //    return questionsData;
        //}
    }
}
