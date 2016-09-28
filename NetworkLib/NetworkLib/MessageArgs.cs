using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace NetworkLib
{
    public class MessageArgs : EventArgs
    {
        public TcpClient ID { get; set; }
        private string message;
        public MessageArgs(string message)
        {
            this.message = message;
        }
        public string Message
        {
            get
            {
                return message;
            }
        }
    }
}
