using System;
using System.Runtime.Serialization;

namespace FKIntegration
{
    [Serializable]
    public class FKIntegrationException : Exception
    {
        public FKIntegrationException()
        {
        }

        public FKIntegrationException(string message)
            : base(message)
        {
        }

        public FKIntegrationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected FKIntegrationException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}
