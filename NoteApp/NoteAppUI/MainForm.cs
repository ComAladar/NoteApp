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
    public partial class MainForm : Form
    {
        /// <summary>
        /// Поле экземпляра класса проекта Project.
        /// </summary>
        private Project _project=new Project();

        /// <summary>
        /// Возвращает и задает экземпляр класса Project.
        /// </summary>
        public Project Project
        {
            get => _project;
            set => _project = value;
        }

        /// <summary>
        /// Считывание количества изменений в программе.
        /// </summary>
        public int CountOfChanges;

        public MainForm()
        {
            InitializeComponent();
            CategoryComboBox.DataSource = Enum.GetValues(typeof(NoteCategory));
            Project = ProjectManager.LoadFromFile(ProjectManager.DefaultFilePath);
            foreach (var item in Project.Notes)
            {
                NotesListBox.Items.Add(item.Name);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void AddNoteButton_Click(object sender, EventArgs e)
        {
            AddEditForm f3 = new AddEditForm();
            f3.Note=new Note();
            f3.ShowDialog();
            if (f3.IsOK == true)
            {
                var updatedNote = f3.Note;
                Project.Notes.Add(updatedNote);
                NotesListBox.Items.Add(updatedNote.Name);
                CountOfChanges++;
            }
        }

        private void EditNoteButton_Click(object sender, EventArgs e)
        {
            AddEditForm f3 = new AddEditForm();
            if (NotesListBox.SelectedIndex == -1)
            {
                return;
            }
            f3.Note = Project.Notes[NotesListBox.SelectedIndex];
            f3.ShowDialog();
            if(f3.IsOK==true)
            {
                var index = NotesListBox.SelectedIndex;
                NotesListBox.Items.RemoveAt(index);
                NotesListBox.Items.Insert(index,f3.Note.Name);
                NoteTitleTextBox.Text = f3.Note.Name;
                NoteTextBox.Text = f3.Note.Text;
                CategotyTextBox.Text = f3.Note.Category.ToString();
                ModifiedDateTimePicker.Value = f3.Note.Modified;
                CountOfChanges++;
            }
        }

        private void DeleteNoteButton_Click(object sender, EventArgs e)
        {
            if (NotesListBox.SelectedIndex == -1)
            {
                return;
            }
            DialogResult deleteResult = MessageBox.Show("Вы точно хотите удалить запись? Название Записи: "+NotesListBox.Text, "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (deleteResult==DialogResult.OK)
            {
                Project.Notes.RemoveAt(NotesListBox.SelectedIndex);
                NotesListBox.Items.RemoveAt(NotesListBox.SelectedIndex);
                CountOfChanges++;
            }
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
            AboutForm f2 = new AboutForm();
            f2.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(CountOfChanges>0)
            {
                {
                    DialogResult result = MessageBox.Show("Сохранить изменения?", "Изменения", MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Exclamation);
                    if (result == DialogResult.OK)
                    {
                        ProjectManager.SaveToFile(Project, ProjectManager.DefaultFilePath);
                    }
                }
            }
        }

        private void NotesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NotesListBox.SelectedIndex == -1)
            {
                return;
            }
            Note tempnote=new Note();
            tempnote = Project.Notes[NotesListBox.SelectedIndex];
            NoteTitleTextBox.Text = tempnote.Name;
            NoteTextBox.Text = tempnote.Text;
            CategotyTextBox.Text = tempnote.Category.ToString();
            CreatedDateTimePicker.Value = (DateTime)tempnote.Created;
            ModifiedDateTimePicker.Value = (DateTime)tempnote.Modified;
        }
    }
}
