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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CategoryComboBox.DataSource = Enum.GetValues(typeof(NoteCategory));
            var project = new Project();
            project = ProjectManager.LoadFromFile(ProjectManager.DefaultFilePath);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void AddNoteButton_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.ShowDialog();
            /*
            var notenew = new Note("Имя", "Текст", NoteCategory.Miscellanea);
            var project = new Project();
            project.Notes.Add(notenew);
            ProjectManager.SaveToFile(project, ProjectManager.DefaultFilePath);
            */
        }

        private void EditNoteButton_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.ShowDialog();
            /*
            var project1 = new Project();
            var notenewdisp = new Note();
            project1.Notes.Add(notenewdisp);
            project1 = ProjectManager.LoadFromFile(ProjectManager.DefaultFilePath);
            */
        }

        private void DeleteNoteButton_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.ShowDialog();
        }
        private void CategoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void addNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNoteButton_Click(sender, e);
        }

        private void editNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditNoteButton_Click(sender, e);

        }

        private void removeNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteNoteButton_Click(sender, e);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы точно хотите выйти?", "Выход", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (result != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
    }
}
