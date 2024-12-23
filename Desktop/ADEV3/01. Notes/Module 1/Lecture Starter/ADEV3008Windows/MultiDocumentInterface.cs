using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADEV3008Windows
{
    public partial class MultiDocumentInterface : Form
    {
        public MultiDocumentInterface()
        {
            InitializeComponent();
        }

        /// <summary>
        /// given - opens LINQ form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lINQToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LinqExample linqExample = new LinqExample();
            linqExample.MdiParent = this;
            linqExample.Show();
        }



        /// <summary>
        /// given - opens file i/o
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileIOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileIOExample fileIOExample = new FileIOExample();
            fileIOExample.MdiParent = this;
            fileIOExample.Show();
        }

        /// <summary>
        /// given - close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// given
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wCFServiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WCFServiceExample wCFServiceExample = new WCFServiceExample();
            wCFServiceExample.MdiParent = this;
            wCFServiceExample.Show();
        }

        /// <summary>
        /// given
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataSourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataSourceExample dataSourceExample = new DataSourceExample();
            dataSourceExample.MdiParent = this;
            dataSourceExample.Show();
        }

    }
}
