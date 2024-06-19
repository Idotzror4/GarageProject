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
        public class Wheel
        {
            private string m_ProducerName;
            private float m_CurrentAirPressure;
            private float m_MaxAirPressure;

            public Wheel(float i_MaxAirPressure)
            {
                m_MaxAirPressure = i_MaxAirPressure;
            }
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
            public float MaxAirPressure
            {
                get { return m_MaxAirPressure; }
                set { m_MaxAirPressure = value; }
            }

            public void InflatingAWheel(float i_AmountOfAirAddindToTheWheel)
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
            public bool CheckingIfCurrentPressureValid(float i_CurrentAirPressure)
            {
                bool isValid = true;

                if(i_CurrentAirPressure > m_MaxAirPressure)
                {
                    isValid = false;
                }    

                return isValid;
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
        public List<Wheel> WheelsList
        {
            get { return m_Wheels; }
            set { m_Wheels = value; }
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

        public void AddEnergy(float i_FuelAmountToAdd, eFuelType i_FuelKind)
        {
            if(this.m_VehicleEngine is FuelEngine)
            {
                if (!i_FuelKind.Equals((this.m_VehicleEngine as FuelEngine).FuelKind))
                {
                    throw new ArgumentException("The fuel kind isn't suitable.");
                }
                else
                {
                    this.m_VehicleEngine.AddEnergy(i_FuelAmountToAdd);
                }
            }
        }
        public void AddEnergy(float i_HoursAmountToAdd)
        {
            this.m_VehicleEngine.AddEnergy(i_HoursAmountToAdd);
        }
    }
}
