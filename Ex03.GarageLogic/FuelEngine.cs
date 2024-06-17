﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class FuelEngine : Engine
    {
        private string m_FuelKind;

        public string FuelKind
        {
            get { return m_FuelKind; }
            set { m_FuelKind = value; }
        }

        public FuelEngine(float i_MaxEnergy) : base(i_MaxEnergy)
        {
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


        //public FuelEngine(float i_RemainEnergy, float i_MaxEnergy, string i_FuelKind)
        //    : base(i_RemainEnergy, i_MaxEnergy)
        //{
        //    m_FuelKind = i_FuelKind;
        //}
    }
}
