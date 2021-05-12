using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExposureMachine.Classes
{
    class MyComException : Exception
    {
        public MyComException()
        {
        }

        public MyComException(string message) : base(message)
        {
        }

        public MyComException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MyComException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
