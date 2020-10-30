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
            comboBox1.DataSource = Enum.GetValues(typeof(NoteCategory));
            var project = new Project();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var notenew = new Note("Имя","Текст",NoteCategory.Miscellanea);
            textBox1.Text = notenew.Name;
            textBox2.Text= notenew.Category.ToString();
            textBox3.Text = notenew.Text;
            textBox4.Text = notenew.Created.ToString();
            textBox5.Text =notenew.Modified.ToString();
            var project = new Project();
            project.Notes.Add(notenew);
            ProjectManager.SaveToFile(project,ProjectManager.DefaultFilePath);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var project1 = new Project();
            var notenewdisp = new Note();
            project1.Notes.Add(notenewdisp);
            project1 =ProjectManager.LoadFromFile(ProjectManager.DefaultFilePath);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result=MessageBox.Show("Вы точно хотите выйти?","Выход", MessageBoxButtons.OKCancel,MessageBoxIcon.Exclamation);
            if (result == DialogResult.OK)
            {
                this.Close();
                //Application.Exit();
            }
        }

        private void addNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.ShowDialog();
        }

        private void editNoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2=new Form2();
            f2.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
