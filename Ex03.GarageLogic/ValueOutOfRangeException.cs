using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        float m_MinValue;
        float m_MaxValue;
        private static readonly string msr_ErrorMessage = "An error occurred: The value is out of range. It should be between {0} and {1}.";

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue) 
            :base(String.Format(msr_ErrorMessage, i_MinValue, i_MaxValue))
        {
            m_MinValue = i_MinValue;
            m_MaxValue = i_MaxValue;
        }

        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue, Exception innerException)
            : base(String.Format(msr_ErrorMessage, i_MinValue, i_MaxValue), innerException)
        {
            m_MinValue = i_MinValue;
            m_MaxValue = i_MaxValue;
        }
    }
}
