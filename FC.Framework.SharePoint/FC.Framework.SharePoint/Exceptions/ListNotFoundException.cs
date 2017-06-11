using System;
using System.Runtime.Serialization;

namespace FC.Framework.SharePoint.Exceptions
{
    [Serializable]
    public class ListNotFoundException : Exception
    {
        public ListNotFoundException() : this("Lista não encontrada")
        {
        }

        public ListNotFoundException(string message) : base(message)
        {
        }

        public ListNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ListNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
