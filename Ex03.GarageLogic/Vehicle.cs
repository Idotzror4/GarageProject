using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        internal class Wheel
        {
            private string m_ProducerName;
            private float m_CurrentAirPressure;
            private float m_MaxAirPressure;

            public Wheel(float i_maxAirPressure)
            {
                m_MaxAirPressure = i_maxAirPressure;
            }

            //public Wheel(string i_producerName, float i_currentAirPressure, float i_maxAirPressure)
            //{
            //    m_ProducerName = i_producerName;
            //    m_CurrentAirPressure = i_currentAirPressure;
            //    m_MaxAirPressure = i_maxAirPressure;
            //}
            public string ProducerName
            {
                get { return m_ProducerName; }
                set { m_ProducerName = value; }
            }
            public float CurrentAirPressure
            {
                get { return m_CurrentAirPressure; }
                set { m_CurrentAirPressure = value; }
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

        public string LicenseNumber
        {
            get { return m_LicenseNumber; }
            set { m_LicenseNumber = value; }
        }
        public string ModelName
        {
            get { return m_ModelName; }
            set { m_ModelName = value; }
        }
        public float RemainEnergyPercent
        {
            get { return m_RemainEnergyPercent; }
            set { m_RemainEnergyPercent = value; }
        }
        public Engine VehicleEngine
        {
            get { return m_VehicleEngine; }
            set { m_VehicleEngine = value; }
        }
        public Vehicle(int i_AmountOfWheels, float i_MaxAirPressure, Engine i_Engine)
        {
            m_VehicleEngine = i_Engine;

            m_Wheels = new List<Wheel>(i_AmountOfWheels);

            for (int i = 0; i < i_AmountOfWheels; i++)
            {
                m_Wheels.Add(new Wheel(i_MaxAirPressure));
            }
        }

        public virtual bool CheckIfValidationVehicle()
        {
            bool validVehicle = false;

            return validVehicle;
        }
       

        //public Vehicle(string i_ModelName, string i_LicenseNumber, float i_RemainEnergyPercent, int i_AmountOfWheels, 
        //               float i_MaxAirPressure, float i_CurrentAirPressure, Engine i_Engine)

        //{
        //    m_ModelName = i_ModelName;
        //    m_LicenseNumber = i_LicenseNumber;
        //    m_RemainEnergyPercent = i_RemainEnergyPercent;
        //    m_VehicleEngine = i_Engine;
        //    //m_Wheels = new List<Wheel>(i_AmountOfWheels); 

        //    //for (int i = 0; i < i_AmountOfWheels; i++)
        //    //{
        //    //    m_Wheels.Add(new Wheel("Unknown", i_CurrentAirPressure, i_MaxAirPressure));
        //    //}
        //}

        //public static Dictionary<string, string> GetVehicleQuestions()
        //{
        //    Dictionary<string, string> questionsData = new Dictionary<string, string>();
        //    questionsData.Add("Enter model name: ", null);
        //    questionsData.Add("Enter license number: ", null);
        //    questionsData.Add("Enter remaining energy percent: ", null);

        //    return questionsData;
        //}
    }
}
