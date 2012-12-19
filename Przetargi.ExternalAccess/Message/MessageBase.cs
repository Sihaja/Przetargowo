using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Przetargi.ExternalAccess.Message
{
    public class MessageBase
    {
        public ResultType Result { get; set; }

        public string Message { get; set; }

        public object ExtensionData { get; set; }
    }
}
