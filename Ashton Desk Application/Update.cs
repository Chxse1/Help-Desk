using System;
using DeskApp;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlClient;
using System.IO;

namespace Ashton_Desk_Application
{
    public partial class Update : Form
    {
        List<Class> b;
        Point lastPoint;
        public Update(List<Class> a)
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

        private void Update_Load(object sender, EventArgs e)
        {
            Class p = (Class)this.Tag;
            label2.Text = p.ID.ToString();
            richTextBox1.Text = p.Name;
            richTextBox2.Text = p.Phone;
            richTextBox3.Text = p.Issue;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Class p = (Class)this.Tag;
            SqlConnection conn = new SqlConnection($"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={Directory.GetCurrentDirectory()}\\Database1.mdf;Integrated Security=True");
            conn.Open();

            SqlCommand command = new SqlCommand("UPDATE [Clients] SET Name = @Name, Phone = @Phone, Issue = @Issue WHERE ID = @ID", conn);
            command.Parameters.AddWithValue("@Name", richTextBox1.Text);
            command.Parameters.AddWithValue("@Phone", richTextBox2.Text);
            command.Parameters.AddWithValue("@Issue", richTextBox3.Text);
            command.Parameters.AddWithValue("@ID", label2.Text);
            command.ExecuteNonQuery();

            conn.Close();
            p.ID = (int)Int32.Parse(label2.Text);
            p.Name = (string)richTextBox1.Text;
            p.Phone = (string)richTextBox2.Text;
            p.Issue = (string)richTextBox3.Text;

            //b.Add(p);

            MessageBox.Show("Client Updated!", "Chase's Help Desk", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
