using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proxy
{
    class ParserClient
    {
        private Logs logs;
        private Proxy proxy;


        public ParserClient(Logs logs, Proxy proxy)
        {

            this.logs = logs;
            this.proxy = proxy;
        }


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
