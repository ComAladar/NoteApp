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
    public partial class AddEditForm : Form
    {
        private Note _note;
        private bool _isOk;

        public Note Note
        {
            get
            {
                return _note;
            }
            set
            {
                _note = value;
                TitleTextBox.Text = _note.Name;
                NoteTextBox.Text = _note.Text;
                CategoryComboBox.SelectedIndex = (int) _note.Category;
                CreatedDateTimePicker.Value = Note.Created;
                ModifiedDateTimePicker.Value = Note.Modified;
            }
        }

        public bool IsOK
        {
            get
            {
                return _isOk;
            }

            private set
            {
                _isOk = value;
            }
        }

        public AddEditForm()
        {
            InitializeComponent();
            CategoryComboBox.DataSource = Enum.GetValues(typeof(NoteCategory));
        }

        private void ExitOKButton_Click(object sender, EventArgs e)
        {
            IsOK = true;
            try
            {
                Note.Name = TitleTextBox.Text;
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Название записи не должно превышать 50 символов. Измените название.","", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Note.Name = TitleTextBox.Text;
            Note.Text = NoteTextBox.Text;
            Note.Category = (NoteCategory)CategoryComboBox.SelectedIndex;
            this.Close();
        }

        private void ExitCancelButton_Click(object sender, EventArgs e)
        {
            IsOK = false;
            this.Close();
        }

        private void TitleTextBox_TextChanged(object sender, EventArgs e)
        {
            string text = TitleTextBox.Text;
            if (text.Length >= 50)
            {
                TitleTextBox.BackColor = Color.Red;
            }
            else TitleTextBox.BackColor = SystemColors.Window;
        }
    }
}
