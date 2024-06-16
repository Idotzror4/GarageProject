﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class ElectricEngine : Engine
    {
        public ElectricEngine(float i_RemainEnergy, float i_MaxEnergy)
            : base(i_RemainEnergy, i_MaxEnergy)
        {
        }
        public void BatteryCharging() { }
    }
}