using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalManagementSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static SqlConnection GetConnection()
        {
            SqlConnection con = new SqlConnection(
                "Data Source=DESKTOP-21ATSCV\\SQLEXPRESS;Initial Catalog=HospitalDB;Integrated Security=True"
            );
            return con;
        }

        int selectedId;

        void LoadData()
        {
            SqlConnection con = GetConnection();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Patients", con);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);

            dgvPatients.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SqlConnection con = GetConnection();
            con.Open();

            string query = "INSERT INTO Patients (Name, Age, Disease) VALUES (@n, @a, @d)";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@n", txtName.Text);
            cmd.Parameters.AddWithValue("@a", txtAge.Text);
            cmd.Parameters.AddWithValue("@d", txtDisease.Text);

            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Patient Added Successfully!");
            LoadData();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection con = GetConnection();
            con.Open();

            string query = "UPDATE Patients SET Name=@n, Age=@a, Disease=@d WHERE Id=@id";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@id", selectedId);
            cmd.Parameters.AddWithValue("@n", txtName.Text);
            cmd.Parameters.AddWithValue("@a", txtAge.Text);
            cmd.Parameters.AddWithValue("@d", txtDisease.Text);

            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Updated Successfully!");
            LoadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlConnection con = GetConnection();
            con.Open();

            string query = "DELETE FROM Patients WHERE Id=@id";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@id", selectedId);

            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Deleted Successfully!");
            LoadData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SqlConnection con = GetConnection();
            con.Open();

            string query = "SELECT * FROM Patients WHERE Name LIKE @n";
            SqlDataAdapter da = new SqlDataAdapter(query, con);

            da.SelectCommand.Parameters.AddWithValue("@n", "%" + txtName.Text + "%");

            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);

            dgvPatients.DataSource = dt;

            con.Close();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();

        }

        private void dgvPatients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedId = Convert.ToInt32(dgvPatients.Rows[e.RowIndex].Cells[0].Value);
            txtName.Text = dgvPatients.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtAge.Text = dgvPatients.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtDisease.Text = dgvPatients.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAge.Focus();
            }
        }

        private void txtAge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDisease.Focus();
            }
        }

        private void txtDisease_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd.PerformClick(); 
            }
        }
    }
}
