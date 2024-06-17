using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Car : Vehicle
    {
        private eCarColor m_CarColor;
        private eDoorsNumber m_DoorsNumber;

        public eCarColor CarColor
        {
            get { return m_CarColor; }
            set { m_CarColor = value; }
        }
        public eDoorsNumber DoorsNumber
        {
            get { return m_DoorsNumber; }
            set { m_DoorsNumber = value; }
        }

        public Car(Engine i_Engine) : base(5, 31, i_Engine)
        {

        }

        public override bool CheckIfValidationVehicle()
        {
            bool validVehicle = base.CheckIfValidationVehicle();

            if (!Enum.IsDefined(typeof(eCarColor), CarColor)) 
            {
                throw new ArgumentException("Car color is invalid.");
            }

            if (DoorsNumber < eDoorsNumber.Two || DoorsNumber > eDoorsNumber.Five)
            {
                throw new ValueOutOfRangeException((float)eDoorsNumber.Two,(float)eDoorsNumber.Five);
            }

            return validVehicle;
        }


        //public Car(string i_ModelName, string i_LicenseNumber, float i_RemainEnergyPercent, int i_CurrentAirPressure, 
        //           Engine i_Engine, string i_CarColor, int i_DoorsNumbers)
        //  : base(i_ModelName, i_LicenseNumber, i_RemainEnergyPercent, 5, 31, i_CurrentAirPressure, i_Engine)
        //{
        //    m_CarColor = i_CarColor;
        //    m_DoorsNumbers = i_DoorsNumbers;
        //}

        //public static Dictionary<string, string> GetVehicleQuestions()
        //{
        //    Dictionary<string, string> questionsData = Vehicle.GetVehicleQuestions();
        //    questionsData.Add("Enter car color: ", null);
        //    questionsData.Add("Enter number of doors: ", null);

        //    return questionsData;
        //}
    }
}
