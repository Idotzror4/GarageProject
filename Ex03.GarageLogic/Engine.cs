﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal abstract class Engine
    {
        private float m_RemainEnergy;
        private float m_MaxEnergy;

        public Engine(float i_RemainEnergy, float i_MaxEnergy)
        {
            m_RemainEnergy = i_RemainEnergy;
            m_MaxEnergy = i_MaxEnergy;
        }
    }
}