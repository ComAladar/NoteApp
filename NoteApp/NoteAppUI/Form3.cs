using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NoteApp;

namespace NoteAppUI
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            CategoryComboBox.DataSource = Enum.GetValues(typeof(NoteCategory));
        }

        private void ExitOKButton_Click(object sender, EventArgs e)
        {

        }

        private void ExitCancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
