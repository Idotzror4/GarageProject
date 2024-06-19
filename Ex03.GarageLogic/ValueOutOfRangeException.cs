using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MinValue;
        private float m_MaxValue;
        private static string ms_ErrorMessage = 
                   "An error occurred: The value is out of range. It should be between {0} and {1}.";
 
        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue, string ErrorMessage)
            : base(String.Format(ErrorMessage, i_MinValue, i_MaxValue))
        {
            m_MinValue = i_MinValue;
            m_MaxValue = i_MaxValue;
        }
        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue) 
            :base(String.Format(ms_ErrorMessage, i_MinValue, i_MaxValue))
        {
            m_MinValue = i_MinValue;
            m_MaxValue = i_MaxValue;
        }
        public ValueOutOfRangeException(float i_MinValue, float i_MaxValue, Exception i_innerException)
            : base(String.Format(ms_ErrorMessage, i_MinValue, i_MaxValue), i_innerException)
        {
            m_MinValue = i_MinValue;
            m_MaxValue = i_MaxValue;
        }
    }
}
