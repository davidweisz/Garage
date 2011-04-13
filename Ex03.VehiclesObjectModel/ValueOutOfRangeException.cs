using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.VehiclesObjectModel
{
    public class ValueOutOfRangeException : Exception
    {
        public ValueOutOfRangeException(string i_Message)
        {
        }

        public ValueOutOfRangeException() : this(null) { }
    }
}
