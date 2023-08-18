using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkScheduleValidator.Exceptions
{
    public class ScheduleFormatException : Exception
    {
        private string _message;
        public new string Message 
        { 
            get 
            {
                return "Bad format: " + _message;
            } 
            set 
            { 
                _message = value; 
            } 
        }
        public ScheduleFormatException(string message) 
        { 
            Message = message;
        }
    }
}
