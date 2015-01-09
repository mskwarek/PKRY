using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proxy
{
    /// <summary>
    /// parser for messages exchanged between proxy and voter/ea
    /// </summary>
    class ParserClient
    {
        /// <summary>
        /// logs instance
        /// </summary>
        private Logs logs;

        /// <summary>
        /// parser for the proxy
        /// </summary>
        private Proxy proxy;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="logs">logs instance</param>
        /// <param name="proxy">proxy instance</param>
        public ParserClient(Logs logs, Proxy proxy)
        {

            this.logs = logs;
            this.proxy = proxy;
        }

        /// <summary>
        /// parse recived message 
        /// </summary>
        /// <param name="msg">recived message</param>
        public void parseMessageFromClient(string msg)
        {
            string[] elem = msg.Split('&');
            switch (elem[0])
            {
                case Constants.GET_SL_AND_SR:
                    this.proxy.sendSLAndSR(elem[1]);
                    break;
                case Constants.GET_YES_NO_POSITION:
                    this.proxy.sendYesNoPosition(elem[1]);
                    break;

                case Constants.VOTE:
                    this.proxy.saveVote(elem[1]);
                    break;

            }


        }



    }
}
