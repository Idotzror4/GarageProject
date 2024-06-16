using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal abstract class Vehicle
    {
        internal class Wheel
        {
            private string m_ProducerName;
            private float m_CurrentAirPressure;
            private float m_MaxAirPressure;

            public Wheel(string i_producerName, float i_currentAirPressure, float i_maxAirPressure)
            {
                m_ProducerName = i_producerName;
                m_CurrentAirPressure = i_currentAirPressure;
                m_MaxAirPressure = i_maxAirPressure;
            }

            public void InflatingAWheel(float i_AmountOfAirAddindToTheWheel) //throwing exception but not catching it
            {
                if (i_AmountOfAirAddindToTheWheel + m_CurrentAirPressure > m_MaxAirPressure)
                {
                    throw new ValueOutOfRangeException(0, m_MaxAirPressure - m_CurrentAirPressure);
                }
                else 
                {
                    m_CurrentAirPressure += i_AmountOfAirAddindToTheWheel;
                }
            }
        }
        private string m_ModelName;
        private string m_LicenseNumber;
        private float m_RemainEnergyPercent;
        private List<Wheel> m_Wheels;
        private Engine m_VehicleEngine;

        public Vehicle(string i_ModelName, string i_LicenseNumber, float i_RemainEnergyPercent, int i_AmountOfWheels, 
                       float i_MaxAirPressure, float i_CurrentAirPressure, Engine i_Engine)

        {
            m_ModelName = i_ModelName;
            m_LicenseNumber = i_LicenseNumber;
            m_RemainEnergyPercent = i_RemainEnergyPercent;
            m_VehicleEngine = i_Engine;
            m_Wheels = new List<Wheel>(i_AmountOfWheels); //
            for (int i = 0; i < i_AmountOfWheels; i++)
            {
                m_Wheels.Add(new Wheel("Unknown", i_CurrentAirPressure, i_MaxAirPressure));
            }
        }
    }
}
