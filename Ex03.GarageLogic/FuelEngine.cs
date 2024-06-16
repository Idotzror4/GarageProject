using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class FuelEngine : Engine
    {
        private string m_FuelKind;

        public void Refueling() { }

        public FuelEngine(float i_RemainEnergy, float i_MaxEnergy, string i_FuelKind)
            : base(i_RemainEnergy, i_MaxEnergy)
        {
            m_FuelKind = i_FuelKind;
        }
    }
}
