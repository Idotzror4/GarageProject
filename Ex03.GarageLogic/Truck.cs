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

        public Truck(Engine i_Engine) : base(12, 28, i_Engine)
        {

        }

        public override bool CheckIfValidationVehicle()
        {
            bool validVehicle = base.CheckIfValidationVehicle();

            if (!Enum.IsDefined(typeof(eCarColor), TransportsHazardousMaterial))
            {
                throw new ArgumentException("Car color is invalid.");
            }

            if (CargoVolume < 0)
            {
                throw new ArgumentException("Cargo Volume is invalid.");
            }

            return validVehicle;
        }

        //public Truck(string i_ModelName, string i_LicenseNumber, float i_RemainEnergyPercent, float i_CurrentAirPressure,
        //             Engine i_Engine, bool i_TransportsHazardousMaterial, float i_CargoVolume)
        //  : base(i_ModelName, i_LicenseNumber, i_RemainEnergyPercent, 12, 28, i_CurrentAirPressure, i_Engine)
        //{
        //    m_TransportsHazardousMaterial = i_TransportsHazardousMaterial;
        //    m_CargoVolume = i_CargoVolume;
        //}

        //public static Dictionary<string, string> GetVehicleQuestions()
        //{
        //    Dictionary<string, string> questionsData = Vehicle.GetVehicleQuestions();
        //    questionsData.Add("Does the truck transport hazardous materials? (yes/no): ", null);
        //    questionsData.Add("Enter cargo volume: ", null);

        //    return questionsData;
        //}
    }
}
