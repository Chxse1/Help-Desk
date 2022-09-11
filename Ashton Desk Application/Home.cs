using DeskApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ashton_Desk_Application
{
    public partial class Home : Form
    {
        List<Class> Class = new List<Class>();
        DataSet ds = new DataSet();
        Point lastPoint;
        public Home()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection($"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename={Directory.GetCurrentDirectory()}\\Database1.mdf;Integrated Security=True");
            conn.Open();

            SqlCommand command = new SqlCommand("Select * from [Clients]", conn);
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Class p = new Class();
                    p.ID = (int)reader.GetValue(0);
                    p.Name = (string)reader.GetValue(1);
                    p.Phone = (string)reader.GetValue(2);
                    p.Issue = (string)reader.GetValue(3);
                    Class.Add(p);
                }

            }
            conn.Close();
            dataGridView1.DataSource = Class;
            dataGridView1.Columns[0].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var show = new Add(Class);
            show.ShowDialog();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Class;
            dataGridView1.Columns[0].Visible = false;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Insert Client Name Here!")
            {
                textBox1.ForeColor = Color.White;
                textBox1.Text = "";
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.ForeColor = Color.Gray;
                textBox1.Text = "Insert Client Name Here!";
                dataGridView1.DataSource = Class;
                dataGridView1.Columns[0].Visible = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataView dv;
            dv = new DataView(CreateDataTable(Class), $"Name like '%{textBox1.Text}%' ", "", DataViewRowState.CurrentRows);
            dataGridView1.DataSource = dv;
        }

        public static DataTable CreateDataTable<T>(IEnumerable<T> list)
        {
            Type type = typeof(T);
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            dataTable.TableName = typeof(T).FullName;
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (T entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
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
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Class p = Class.Find(x => x.ID == (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            Update frmAdd = new Update(Class);
            frmAdd.Tag = p;
            frmAdd.ShowDialog();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = Class;
            dataGridView1.Columns[0].Visible = false;
        }
    }
}
