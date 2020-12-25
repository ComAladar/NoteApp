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
        private Project _project = new Project();

        /// <summary>
        /// Возвращает и задает экземпляр класса Project.
        /// </summary>
        private Project Project
        {
            get => _project;
            set => _project = value;
        }
        
        /// <summary>
        /// Поле отображаемого списка записей.
        /// </summary>
        private List<Note> _viewedNotes= new List<Note>();

        /// <summary>
        /// Возвращает и задает отображаемый список записей.
        /// </summary>
        private List<Note> ViewedNotes
        {
            get => _viewedNotes;
            set => _viewedNotes = value;
        }

        /// <summary>
        /// Метод добавления заметки.
        /// </summary>
        /// <param name="category"></param>
        private void AddNote()
        {
            NoteForm noteForm = new NoteForm();
            noteForm.Note = new Note();
            noteForm.ShowDialog();
            if (noteForm.DialogResult == DialogResult.OK)
            {
                var updatedNote = noteForm.Note;
                Project.Notes.Add(updatedNote);
                if (CategoryComboBox.SelectedItem.ToString() == "All")
                {
                    ViewedNotes = Project.SortList();
                    EditAddListBoxRefillNotes();
                }
                else
                {
                    if (updatedNote.Category == (NoteCategory)CategoryComboBox.SelectedIndex-1)
                    {
                        ViewedNotes = Project.SortList(updatedNote.Category);
                        EditAddListBoxRefillNotes();
                    }
                }
                ProjectManager.SaveToFile(Project, ProjectManager.DefaultFilePath);
            }
        }

        /// <summary>
        /// Метод редактирования заметки.
        /// </summary>
        /// <param name="category"></param>
        private void EditNote()
        {
            var index = NotesListBox.SelectedIndex;
            if (index == -1)
            {
                return;
            }
            NoteForm noteForm = new NoteForm();
            noteForm.Note = ViewedNotes[index];
            noteForm.ShowDialog();
            if (noteForm.DialogResult == DialogResult.OK)
            {
                if (CategoryComboBox.SelectedItem.ToString() == "All")
                {
                    ViewedNotes = Project.SortList();
                    EditAddListBoxRefillNotes();
                }
                else
                {
                    ViewedNotes = Project.SortList((NoteCategory)CategoryComboBox.SelectedItem);
                    EditAddListBoxRefillNotes();
                }
                ProjectManager.SaveToFile(Project, ProjectManager.DefaultFilePath);
                if (ViewedNotes.Count != 0)
                { 
                    NotesListBox.SelectedIndex = 0;
                }
                else
                {
                    NoteTitleTextBox.Text = "Название Заметки";
                    NoteTextBox.Text = "";
                    CategotyTextBox.Text = "Наименование Категории";
                    return;
                }
                NoteTitleTextBox.Text = noteForm.Note.Name;
                NoteTextBox.Text = noteForm.Note.Text;
                CategotyTextBox.Text = noteForm.Note.Category.ToString();
                ModifiedDateTimePicker.Value = noteForm.Note.Modified;
            }
        }

        /// <summary>
        /// Метод удаления заметки.
        /// </summary>
        /// <param name="category"></param>
        private void DeleteNote()
        {
            if (NotesListBox.SelectedIndex == -1)
            {
                return;
            }

            var index = NotesListBox.SelectedIndex;
            DialogResult deleteResult = MessageBox.Show("Вы точно хотите удалить запись? Название Записи: " + NotesListBox.Text, "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (deleteResult == DialogResult.OK)
            {
                var SelectedNote = ViewedNotes[index];
                ViewedNotes.RemoveAt(NotesListBox.SelectedIndex);
                foreach (var item in Project.Notes)
                {
                    if (SelectedNote.Created == item.Created)
                    {
                        Project.Notes.Remove(item);
                        break;
                    }
                }
                if (CategoryComboBox.SelectedItem.ToString() == "All")
                {
                    ViewedNotes = Project.SortList();
                    EditAddListBoxRefillNotes();
                }
                else
                {
                    ViewedNotes = Project.SortList((NoteCategory)CategoryComboBox.SelectedItem);
                    EditAddListBoxRefillNotes();
                }
                ProjectManager.SaveToFile(Project, ProjectManager.DefaultFilePath);
                NoteTitleTextBox.Text = "Название Заметки";
                NoteTextBox.Text = "";
                CategotyTextBox.Text = "Наименование Категории";
            }
        }

        /// <summary>
        /// Метод получения последней выбранной заметки.
        /// </summary>
        private void GetCurrentNote()
        {
            try
            {
                NotesListBox.SelectedIndex = Project.CurrentNote;
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// Метод получения последней выбранной категории.
        /// </summary>
        private void GetCurrentCategory()
        {
            try
            {
                CategoryComboBox.SelectedIndex = Project.CurrentCategory;
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// Метод сортировки записей.
        /// </summary>
        private void SortFillListBoxNotes()
        {
            foreach (var item in ViewedNotes)
            {
                NotesListBox.Items.Add(item.Name);
            }
        }

        /// <summary>
        /// Метод сортировки и очистики ListBox от заметок.
        /// </summary>
        private void EditAddListBoxRefillNotes()
        {
            NotesListBox.Items.Clear();
            SortFillListBoxNotes();
        }

        /// <summary>
        /// Используется в UI для отображения при смене категории.
        /// </summary>
        /// <param name="category"></param>
        private void CategoryComboBoxChanged(NoteCategory category)
        {
            if (CategoryComboBox.SelectedItem.ToString()=="All")
            {
                ViewedNotes = Project.SortList();
            }
            else
            {
                ViewedNotes = Project.SortList(category);
            }
            EditAddListBoxRefillNotes();
            Project.CurrentCategory = (int)category;
        }

        public MainForm()
        {
            InitializeComponent();
            CategoryComboBox.Items.Add("All");
            var categories = Enum.GetValues(typeof(NoteCategory));
            for (int i = 0; i < categories.Length; i++)
            {
                CategoryComboBox.Items.Add(categories.GetValue(i));
            }
            CategoryComboBox.SelectedIndex = 0;
            Project = ProjectManager.LoadFromFile(ProjectManager.DefaultFilePath);
            ViewedNotes = Project.Notes;
            ViewedNotes = Project.SortList();
            SortFillListBoxNotes();
            if (Project.Notes.Count != 0)
            {
                GetCurrentCategory();
                GetCurrentNote();
            }
        }

        private void AddNoteButton_Click(object sender, EventArgs e)
        {
            AddNote();
        }

        private void EditNoteButton_Click(object sender, EventArgs e)
        {
            EditNote();
        }

        private void DeleteNoteButton_Click(object sender, EventArgs e)
        {
            DeleteNote();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNote();
        }

        private void editNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditNote();
        }

        private void removeNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteNote();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Вы точно хотите выйти?", "Выход", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    if (result != DialogResult.OK)
                    {
                        e.Cancel=true;
                    }
                    else
                    {
                        ProjectManager.SaveToFile(Project, ProjectManager.DefaultFilePath);
                    }
        }

        private void NotesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NotesListBox.SelectedIndex == -1)
            {
                return;
            }
            var note = ViewedNotes[NotesListBox.SelectedIndex];
            NoteTitleTextBox.Text = note.Name;
            NoteTextBox.Text = note.Text;
            CategotyTextBox.Text = note.Category.ToString();
            CreatedDateTimePicker.Value = note.Created;
            ModifiedDateTimePicker.Value = note.Modified;
            Project.CurrentNote = NotesListBox.SelectedIndex;
        }

        private void CategoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            NoteTitleTextBox.Text = "Название Заметки";
            NoteTextBox.Text = "";
            CategotyTextBox.Text = "Наименование Категории";
            Project.CurrentCategory = CategoryComboBox.SelectedIndex;
            CategoryComboBoxChanged((NoteCategory)CategoryComboBox.SelectedIndex-1);
            if (CategoryComboBox.SelectedItem.ToString()=="All")
            {
                Project.CurrentCategory = CategoryComboBox.SelectedIndex;
                EditAddListBoxRefillNotes();
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteNote();
            }
        }
    }
}
