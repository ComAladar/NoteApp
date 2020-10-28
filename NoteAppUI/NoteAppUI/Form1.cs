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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.DataSource = Enum.GetValues(typeof(NoteCategory));
            var project = new Project();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
 
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var notenew = new Note();
            notenew.Name = "Имя";
            notenew.Text = "Текст";
            notenew.Category = NoteCategory.Miscellanea;
            textBox1.Text = notenew.Name;
            textBox2.Text= notenew.Category.ToString();
            textBox3.Text = notenew.Text;
            textBox4.Text = notenew.CreationTime.ToString();
            textBox5.Text =notenew.ModifiedTime.ToString();
            var project = new Project();
            project.Notes.Add(notenew);
            ProjectManager.SaveToFile(project,ProjectManager.DefaultFilePath);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var project1 = new Project();
            var notenewdisp = new Note();
            project1=ProjectManager.LoadFromFile(ProjectManager.DefaultFilePath);
        }
    }
}
