using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieApplication
{
    public partial class Form2 : Form
    {
        private Form parent;
        public Form2(Form parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

       

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            parent.Enabled= true;
        }
    }
}
