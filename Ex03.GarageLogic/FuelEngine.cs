using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        private eFuelType m_FuelKind;

        public eFuelType FuelKind
        {
            get { return m_FuelKind; }
            set { m_FuelKind = value; }
        }

        public FuelEngine(float i_MaxEnergy, eFuelType i_FuelKind) : base(i_MaxEnergy)
        {
            m_FuelKind = i_FuelKind;
        }

        public void Refueling(String i_FuelKind, float i_FuelAmountToAdd)
        {
            if (!i_FuelKind.Equals(m_FuelKind))
            {
                throw new ArgumentException();
            }
            else if (i_FuelAmountToAdd + RemainEnergy > MaxEnergy)
            {
                throw new ValueOutOfRangeException(0, MaxEnergy - RemainEnergy);
            }
            else
            {
                RemainEnergy += i_FuelAmountToAdd;
            }
        }
    }
}
