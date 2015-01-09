using Org.BouncyCastle.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Voter
{

    /// <summary>
    /// confirmation for voter - proxy sends him this column which he/she has choosen
    /// (precisely signed, explicit and token)
    /// </summary>
    class Confirmation
    {
        /// <summary>
        /// number of column which was choosen
        /// </summary>
        private int numColumn;

        /// <summary>
        /// vote as string 
        /// </summary>
        private string column;

        /// <summary>
        /// token corresponding to choosen column
        /// </summary>
        private BigInteger token;

        /// <summary>
        /// signed column (recived from EA)
        /// </summary>
        private BigInteger signedColumn;

        /// <summary>
        /// list view - to represent confirmation
        /// </summary>
        private ListView ListView;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="ListView">list view to represent confirm</param>
        public Confirmation(ListView ListView)
        {
            this.ListView = ListView;
        }

        /// <summary>
        /// number of column choosen as confirmation 
        /// </summary>
        public int ColumnNumber
        {
            set { numColumn = value; }
        }

        /// <summary>
        /// string which contains value of column 
        /// </summary>
        public string Column
        {
            set { column = value; }
            get { return column; }
        }

        /// <summary>
        /// token received from EA as a confirmation 
        /// </summary>
        public BigInteger Token
        {
            set { token = value; }
            get { return token;  }
        }

        /// <summary>
        /// representation of column signed by EA
        /// </summary>
        public BigInteger SignedColumn
        {
            set { signedColumn = value; }
            get { return signedColumn; }
        }

        public int Index
        {
            get { return numColumn; }
        }

        /// <summary>
        /// add and display confirmation
        /// </summary>
        /// <param name="anotherThread">thread flag</param>
        public void addConfirm( bool anotherThread = false)
        {
            ListViewItem item1 = new ListViewItem();
            ListViewItem item2 = new ListViewItem();
            ListViewItem item3 = new ListViewItem();
            ListViewItem item4 = new ListViewItem();
            item1.Text = "Column: " + this.numColumn;
            item2.Text = "Column (your voting): " + this.column;
            item3.Text = "Token: " + this.token;
            item4.Text = "Signed Column: " + this.signedColumn;

            if (!anotherThread)
            {
                ListView.Items.Add(item1);
                ListView.Items[ListView.Items.Count - 1].EnsureVisible();
                ListView.Items.Add(item2);
                ListView.Items[ListView.Items.Count - 1].EnsureVisible();
                ListView.Items.Add(item3);
                ListView.Items[ListView.Items.Count - 1].EnsureVisible();
                ListView.Items.Add(item4);
                ListView.Items[ListView.Items.Count - 1].EnsureVisible();
                
            }
            else
            {
                ListView.Invoke(new MethodInvoker(delegate()
                {
                    ListView.Items.Add(item1);
                    ListView.Items[ListView.Items.Count - 1].EnsureVisible();
                    ListView.Items.Add(item2);
                    ListView.Items[ListView.Items.Count - 1].EnsureVisible();
                    ListView.Items.Add(item3);
                    ListView.Items[ListView.Items.Count - 1].EnsureVisible();
                    ListView.Items.Add(item4);
                    ListView.Items[ListView.Items.Count - 1].EnsureVisible();
                })
                    );
            }
        }

    }
}
