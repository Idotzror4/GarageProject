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

        public override void AddEnergy(float i_AmountEnergyToAdd)
        {
            if (i_AmountEnergyToAdd + RemainEnergy > MaxEnergy)
            {
                throw new ValueOutOfRangeException(0, MaxEnergy - RemainEnergy, 
                    "you exceed the amount of fuel you can fill.It should be between {0} and {1}");
            }
            else
            {
                RemainEnergy += i_AmountEnergyToAdd;
            }
        }
    }
}
