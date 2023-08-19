using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkScheduleValidator.Exceptions
{
    public class ScheduleArgumentException : Exception
    {
        private string _message;

        public ScheduleArgumentException(string message)
        {
            _message = message;
        }

        public new string Message
        {
            get
            {
                return "Argument exception: " + _message;
            }
            set
            {
                _message = value;
            }
        }
    }
}
