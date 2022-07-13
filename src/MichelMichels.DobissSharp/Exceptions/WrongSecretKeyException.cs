using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MichelMichels.DobissSharp.Exceptions
{
    public class WrongSecretKeyException : Exception
    {
        public WrongSecretKeyException()
        {
        }

        public WrongSecretKeyException(string message) : base(message)
        {
        }

        public WrongSecretKeyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongSecretKeyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
