using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using DeskApp;
using System.Runtime.Remoting.Messaging;

namespace Ashton_Desk_Application
{
    public partial class Add : Form
     {
        List<Class> b;
        Point lastPoint;
        public Add(List<Class> a)
        {
            InitializeComponent();
            b = a;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "Client Name (50 Char Limit)")
            {
                MessageBox.Show("Please enter correct info!", "Chase's Help Desk", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (richTextBox2.Text == "Client Phone (15 Char Limit)")
            {
                MessageBox.Show("Please enter correct info!", "Chase's Help Desk", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (richTextBox3.Text == "Client Issue (No Limit)")
            {
                MessageBox.Show("Please enter correct info!", "Chase's Help Desk", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                SqlConnection conn = new SqlConnection($"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={Directory.GetCurrentDirectory()}\\Database1.mdf;Integrated Security=True");
                conn.Open();

                SqlCommand command = new SqlCommand("INSERT INTO [Clients] (Name, Phone, Issue) VALUES (@Name, @Phone, @Issue)", conn);
                command.Parameters.AddWithValue("@Name", richTextBox1.Text);
                command.Parameters.AddWithValue("@Phone", richTextBox2.Text);
                command.Parameters.AddWithValue("@Issue", richTextBox3.Text);
                command.ExecuteNonQuery();

                conn.Close();

                Class p = new Class();
                //p.ID = (int)reader.GetValue(0);
                p.Name = (string)richTextBox1.Text;
                p.Phone = (string)richTextBox2.Text;
                p.Issue = (string)richTextBox3.Text;

                b.Add(p);

                MessageBox.Show("Client Added!", "Chase's Help Desk", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void richTextBox1_Enter(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "Client Name (50 Char Limit)")
            {
                richTextBox1.ForeColor = Color.White;
                richTextBox1.Text = "";
            }
        }

        private void richTextBox1_Leave(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                richTextBox1.ForeColor = Color.Gray;
                richTextBox1.Text = "Client Name (50 Char Limit)";
            }
        }

        private void richTextBox2_Enter(object sender, EventArgs e)
        {
            if (richTextBox2.Text == "Client Phone (15 Char Limit)")
            {
                richTextBox2.ForeColor = Color.White;
                richTextBox2.Text = "";
            }
        }

        private void richTextBox2_Leave(object sender, EventArgs e)
        {
            if (richTextBox2.Text == "")
            {
                richTextBox2.ForeColor = Color.Gray;
                richTextBox2.Text = "Client Phone (15 Char Limit)";
            }
        }

        private void richTextBox3_Enter(object sender, EventArgs e)
        {
            if (richTextBox3.Text == "Client Issue (No Limit)")
            {
                richTextBox3.ForeColor = Color.White;
                richTextBox3.Text = "";
            }
        }

        private void richTextBox3_Leave(object sender, EventArgs e)
        {
            if (richTextBox3.Text == "")
            {
                richTextBox3.ForeColor = Color.Gray;
                richTextBox3.Text = "Client Issue (No Limit)";
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }
    }
}
