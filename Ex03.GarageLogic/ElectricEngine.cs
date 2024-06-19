using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class ElectricEngine : Engine
    {
        public ElectricEngine(float i_MaxEnergy) : base(i_MaxEnergy) { }

        public override void AddEnergy(float i_AmountEnergyToAdd)
        {
            if (i_AmountEnergyToAdd + RemainEnergy > MaxEnergy)
            {
                throw new ValueOutOfRangeException(0, MaxEnergy - RemainEnergy);
            }
            else
            {
                RemainEnergy += i_AmountEnergyToAdd;
            }
        }
    }
}
