using Org.BouncyCastle.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Voter
{

    public class Confirmation
    {
        private int numColumn;
        private string column;
        private BigInteger token;
        private BigInteger signedColumn;
        private ListView ListView;

        public Confirmation(ListView ListView)
        {
            this.ListView = ListView;
        }

        public int ColumnNumber
        {
            set { numColumn = value; }
            get { return numColumn;  }
        }

        public string Column
        {
            set { column = value; }
            get { return column; }
        }

        public BigInteger Token
        {
            set { token = value; }
            get { return token;  }
        }

        public BigInteger SignedColumn
        {
            set { signedColumn = value; }
            get { return signedColumn; }
        }

        public int Index
        {
            get { return numColumn; }
        }

        public void addConfirm()
        {
            Utils.Logs.addLog("Voter", "Column: " + this.numColumn, true, NetworkLib.Constants.LOG_INFO);
            Utils.Logs.addLog("Voter", "Column (your voting): " + this.column, true, NetworkLib.Constants.LOG_INFO); ;
            Utils.Logs.addLog("Voter", "Token: " + this.token, true, NetworkLib.Constants.LOG_INFO); ;
            Utils.Logs.addLog("Voter", "Signed Column: " + this.signedColumn, true, NetworkLib.Constants.LOG_INFO); ;
        }

        public int getLogsCounter()
        {
            return Utils.Logs.getLogsCounter();
        }
    }
}
