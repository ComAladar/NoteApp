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
    public partial class NoteForm : Form
    {
        /// <summary>
        /// Поле класса Note для передачи данных.
        /// </summary>
        private Note _note;

        /// <summary>
        /// Свойство Note для отображения на форме данных записи.
        /// </summary>
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
                CreatedDateTimePicker.Value = _note.Created;
                ModifiedDateTimePicker.Value = _note.Modified;
            }
        }

        public NoteForm()
        {
            InitializeComponent();
            CategoryComboBox.DataSource = Enum.GetValues(typeof(NoteCategory));
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            try
            {
                Note.Name = TitleTextBox.Text;
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Название записи не должно превышать 50 символов. Измените название.", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Note.Name = TitleTextBox.Text;
            Note.Text = NoteTextBox.Text;
            Note.Category = (NoteCategory)CategoryComboBox.SelectedIndex;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void TitleTextBox_TextChanged(object sender, EventArgs e)
        {
            string text = TitleTextBox.Text;
            if (text.Length >= 50)
            {
                TitleTextBox.BackColor = Color.Red;
            }
            else
            {
                TitleTextBox.BackColor = SystemColors.Window;
            }
        }
    }
}
