﻿using System;
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
        private void AddNote(int category)
        {
            NoteForm noteForm = new NoteForm();
            noteForm.Note = new Note();
            noteForm.ShowDialog();
            if (noteForm.DialogResult == DialogResult.OK)
            {
                var updatedNote = noteForm.Note;
                Project.Notes.Add(updatedNote);
                Project.SortList();
                if (updatedNote.Category == (NoteCategory) category)
                {
                    ViewedNotes=Project.SortList((NoteCategory) category, ViewedNotes);
                    EditAddListBoxRefillViewedNotes();
                }
                if(category==7)
                {
                    EditAddListBoxRefillNotes();
                }
                ProjectManager.SaveToFile(Project, ProjectManager.DefaultFilePath);
            }
        }

        /// <summary>
        /// Метод редактирования заметки.
        /// </summary>
        /// <param name="category"></param>
        private void EditNote(int category)
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
                if (category == 7)
                {
                    Project.SortList();
                    EditAddListBoxRefillNotes();
                }
                else
                {
                    Project.SortList();
                    ViewedNotes = Project.SortList((NoteCategory)category, ViewedNotes);
                    EditAddListBoxRefillViewedNotes();
                    if (noteForm.Note.Category != (NoteCategory)category)
                    {
                        ViewedNotes = Project.SortList((NoteCategory)category, ViewedNotes);
                        EditAddListBoxRefillViewedNotes();
                    }
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
        private void DeleteNote(int category)
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
                Project.SortList();
                if (category == 7)
                {
                    EditAddListBoxRefillNotes();
                }
                else
                {
                    ViewedNotes = Project.SortList((NoteCategory) category, ViewedNotes);
                    EditAddListBoxRefillViewedNotes();
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
            ViewedNotes = Project.Notes;
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
        /// Метод добавления в ListBox временных заметок.
        /// </summary>
        private void FillListBoxViewedNotes()
        {
            foreach (var item in ViewedNotes)
            {
                NotesListBox.Items.Add(item.Name);
            }
        }

        /// <summary>
        /// Метод очистки и добавления в ListBox временных заметок.
        /// </summary>
        private void EditAddListBoxRefillViewedNotes()
        {
            NotesListBox.Items.Clear();
            FillListBoxViewedNotes();
        }

        /// <summary>
        /// Используется в UI для отображения при смене категории.
        /// </summary>
        /// <param name="category"></param>
        private void CategoryComboBoxChanged(int category)
        {
            Project.SortList();
            ViewedNotes=Project.SortList((NoteCategory) category, ViewedNotes);
            EditAddListBoxRefillViewedNotes();
            Project.CurrentCategory = category;
        }

        public MainForm()
        {
            InitializeComponent();
            var categories = Enum.GetValues(typeof(NoteCategory));
            for (int i = 0; i < categories.Length; i++)
            {
                CategoryComboBox.Items.Add(categories.GetValue(i));
            }
            CategoryComboBox.Items.Add("All");
            CategoryComboBox.SelectedIndex = 7;

            Project = ProjectManager.LoadFromFile(ProjectManager.DefaultFilePath);
            ViewedNotes = Project.Notes;
            SortFillListBoxNotes();
            if (Project.Notes.Count != 0)
            {
                GetCurrentCategory();
                GetCurrentNote();
            }
        }

        private void AddNoteButton_Click(object sender, EventArgs e)
        {
            if (CategoryComboBox.SelectedIndex == 7)
            {
                AddNote(7);
            }
            else AddNote(CategoryComboBox.SelectedIndex);
        }

        private void EditNoteButton_Click(object sender, EventArgs e)
        {
            if (CategoryComboBox.SelectedIndex == 7)
            {
                EditNote(7);
            }
            else EditNote(CategoryComboBox.SelectedIndex);
        }

        private void DeleteNoteButton_Click(object sender, EventArgs e)
        {
            if (CategoryComboBox.SelectedIndex == 7)
            {
                DeleteNote(7);
            }
            else DeleteNote(CategoryComboBox.SelectedIndex);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CategoryComboBox.SelectedIndex == 7)
            {
                AddNote(7);
            }
            else AddNote(CategoryComboBox.SelectedIndex);
        }

        private void editNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CategoryComboBox.SelectedIndex == 7)
            {
                EditNote(7);
            }
            else EditNote(CategoryComboBox.SelectedIndex);
        }

        private void removeNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CategoryComboBox.SelectedIndex == 7)
            {
                DeleteNote(7);
            }
            else DeleteNote(CategoryComboBox.SelectedIndex);
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
                        Project.SortList();
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
            Project.CurrentCategory = CategoryComboBox.SelectedIndex;
            CategoryComboBoxChanged(CategoryComboBox.SelectedIndex);
            if (CategoryComboBox.SelectedIndex == 7)
            {
                Project.CurrentCategory = CategoryComboBox.SelectedIndex;
                EditAddListBoxRefillNotes();
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (CategoryComboBox.SelectedIndex == 7)
                {
                    DeleteNote(7);
                }
                else DeleteNote(CategoryComboBox.SelectedIndex);
            }
        }
    }
}
