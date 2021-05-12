using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExposureMachine.Classes
{
    class MyCameraException : Exception
    {
        public MyCameraException()
        {
        }

        public MyCameraException(string message) : base(message)
        {
        }

        public MyCameraException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MyCameraException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
