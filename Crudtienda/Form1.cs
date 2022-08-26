using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crudtienda
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string codigo = txtCodigo.Text;
            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;
            double precio_publico = double.Parse(txtPreciopublico.Text);
            int existencias = int.Parse(txtExistencia.Text);

            string sql = "INSERT INTO productos (codigo, nombre, descripcion, precio_publico, existencias)" +
                "VALUES ('" + codigo + "','" + nombre + "','" + descripcion + "','" + precio_publico + "','" + existencias + "')";

            MySqlConnection conexionBD = conexion.Conexion();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.ExecuteNonQuery();

                MessageBox.Show("Registro Guardado");
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al guaradar" + ex.Message);
                
            }
            finally
            {
                conexionBD.Close();
            }
        
        }
    }
}
