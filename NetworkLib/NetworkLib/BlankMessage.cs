using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkLib
{
    public class BlankMessage : Message
    {
        public override void Parse(object subject, string msg)
        {
            throw new NotImplementedException();
        }
    }
}
