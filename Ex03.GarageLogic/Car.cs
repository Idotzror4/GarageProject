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
        private const int k_CarWheelsNumber = 5;
        private const int k_CarMaxAirPressure = 31;

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
        public Car(Engine i_Engine) : base(k_CarWheelsNumber, k_CarMaxAirPressure, i_Engine) { }
    }
}
