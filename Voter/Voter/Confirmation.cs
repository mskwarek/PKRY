using Org.BouncyCastle.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Voter
{

    class Confirmation
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
