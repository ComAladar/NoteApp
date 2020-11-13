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
        /// Метод добавления заметки.
        /// </summary>
        private void AddNote()
        {
            NoteForm noteForm = new NoteForm();
            noteForm.Note = new Note();
            noteForm.ShowDialog();
            if (noteForm.DialogResult == DialogResult.OK)
            {
                var updatedNote = noteForm.Note;
                Project.Notes.Add(updatedNote);
                NotesListBox.Items.Add(updatedNote.Name);
                ProjectManager.SaveToFile(Project, ProjectManager.DefaultFilePath);
            }
        }

        /// <summary>
        /// Метод редактирования заметки.
        /// </summary>
        private void EditNote()
        {
            var index = NotesListBox.SelectedIndex;
            if (index == -1)
            {
                return;
            }
            NoteForm noteForm = new NoteForm();
            noteForm.Note = Project.Notes[index];
            noteForm.ShowDialog();
            if (noteForm.DialogResult == DialogResult.OK)
            {
                NotesListBox.Items.RemoveAt(index);
                NotesListBox.Items.Insert(index, noteForm.Note.Name);
                NoteTitleTextBox.Text = noteForm.Note.Name;
                NoteTextBox.Text = noteForm.Note.Text;
                CategotyTextBox.Text = noteForm.Note.Category.ToString();
                ModifiedDateTimePicker.Value = noteForm.Note.Modified;
                NotesListBox.SelectedIndex = index;
                ProjectManager.SaveToFile(Project, ProjectManager.DefaultFilePath);
            }

        }

        /// <summary>
        /// Метод удаления заметки.
        /// </summary>
        private void DeleteNote()
        {
            if (NotesListBox.SelectedIndex == -1)
            {
                return;
            }
            DialogResult deleteResult = MessageBox.Show("Вы точно хотите удалить запись? Название Записи: " + NotesListBox.Text, "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (deleteResult == DialogResult.OK)
            {
                Project.Notes.RemoveAt(NotesListBox.SelectedIndex);
                NotesListBox.Items.RemoveAt(NotesListBox.SelectedIndex);
                NoteTitleTextBox.Text = "Название Заметки";
                NoteTextBox.Text = "";
                CategotyTextBox.Text = "Наименование Категории";
                ProjectManager.SaveToFile(Project, ProjectManager.DefaultFilePath);
            }
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
            foreach (var item in Project.Notes)
            {
                NotesListBox.Items.Add(item.Name);
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
            var note = Project.Notes[NotesListBox.SelectedIndex];
            NoteTitleTextBox.Text = note.Name;
            NoteTextBox.Text = note.Text;
            CategotyTextBox.Text = note.Category.ToString();
            CreatedDateTimePicker.Value = note.Created;
            ModifiedDateTimePicker.Value = note.Modified;
        }
    }
}