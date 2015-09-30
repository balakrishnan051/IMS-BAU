using System;

namespace Framework
{
    [Serializable]
    public class AutomationException : Exception
    {
        //private string headerMessage = string.Empty;

        public AutomationException(string message)
            : base("Automation Exception: " + message)
        { }

        public AutomationException(string message, Exception innerException)
            : base("Automation Exception: " + message + " " + innerException.Message, innerException)
        {  
        }

        public AutomationException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        { }
    }
}
