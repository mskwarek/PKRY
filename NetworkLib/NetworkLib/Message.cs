using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkLib
{
    public abstract class Message
    {
        public abstract void Parse(object subject, string msg);
    }
}
