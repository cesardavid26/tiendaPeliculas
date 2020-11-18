using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Tienda
{

    public partial class Form1 : Form

    {
        string connectionString = @"Server=localhost;Database=tienda;Uid=root;Pwd=;";
        int peliculaid = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Clear();
            GridFill();
        }

       
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            using (MySqlConnection mysqlcon = new MySqlConnection(connectionString))
            {
                mysqlcon.Open();
                MySqlCommand mysqlcmd = new MySqlCommand("PeliculaAddOrEdit", mysqlcon);
                mysqlcmd.CommandType = CommandType.StoredProcedure;
                mysqlcmd.Parameters.AddWithValue("_idpelicula", peliculaid);
                mysqlcmd.Parameters.AddWithValue("_nombre", txtNombre.Text.Trim());
                mysqlcmd.Parameters.AddWithValue("_genero", txtGenero.Text.Trim());
                mysqlcmd.Parameters.AddWithValue("_descripcion", txtDescripcion.Text.Trim());
                mysqlcmd.Parameters.AddWithValue("_año", txtAño.Text.Trim());
                mysqlcmd.ExecuteNonQuery();
                MessageBox.Show("Registro exitoso");
                Clear();
                GridFill();
            }
        }
        void GridFill()
        {
            using (MySqlConnection mysqlcon = new MySqlConnection(connectionString))
            {
                mysqlcon.Open();
                MySqlDataAdapter sqlDa = new MySqlDataAdapter("PeliculaViewAll", mysqlcon);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dt = new DataTable();
                sqlDa.Fill(dt);
                dgvPeliculas.DataSource = dt;
                dgvPeliculas.Columns[0].Visible = false;
            }
        }
        void Clear()
        {
            txtNombre.Text = txtGenero.Text = txtDescripcion.Text = txtBuscar.Text = txtAño.Text = "";
            peliculaid = 0;
            btnAgregar.Text = "Agregar";
            btnBorrar.Enabled = Enabled;
        }

        private void dgvPeliculas_DoubleClick(object sender, EventArgs e)
        {
            if(dgvPeliculas.CurrentRow.Index != -1)
            {
                txtNombre.Text = dgvPeliculas.CurrentRow.Cells[1].Value.ToString();
                txtGenero.Text = dgvPeliculas.CurrentRow.Cells[2].Value.ToString();
                txtDescripcion.Text = dgvPeliculas.CurrentRow.Cells[3].Value.ToString();
                txtAño.Text = dgvPeliculas.CurrentRow.Cells[4].Value.ToString();
                peliculaid = Convert.ToInt32(dgvPeliculas.CurrentRow.Cells[0].Value.ToString());
                btnAgregar.Text = "Actualizar";
            }

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (MySqlConnection mysqlcon = new MySqlConnection(connectionString))
            {
                mysqlcon.Open();
                MySqlDataAdapter sqlDa = new MySqlDataAdapter("PeliculaSearchByValue", mysqlcon);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("_SearchValue", txtBuscar.Text);
                DataTable dt = new DataTable();
                sqlDa.Fill(dt);
                dgvPeliculas.DataSource = dt;
                dgvPeliculas.Columns[0].Visible = false;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            using (MySqlConnection mysqlcon = new MySqlConnection(connectionString))
            {
                mysqlcon.Open();
                MySqlCommand mysqlcmd = new MySqlCommand("PeliculaDeleteById", mysqlcon);
                mysqlcmd.CommandType = CommandType.StoredProcedure;
                mysqlcmd.Parameters.AddWithValue("_peliculaid", peliculaid);
                mysqlcmd.ExecuteNonQuery();
                MessageBox.Show("Eliminacion exitosa");
                Clear();
                GridFill();
            }
        }

       
    }
}
