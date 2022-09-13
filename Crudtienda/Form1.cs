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
            cargarCategorias();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int idCategoria = int.Parse(cbxCategoria.SelectedValue.ToString());
            MessageBox.Show(cbxCategoria.Text);
            try
            {
                string codigo = txtCodigo.Text;
                string nombre = txtNombre.Text;
                string descripcion = txtDescripcion.Text;
                double precio_publico = double.Parse(txtPreciopublico.Text);
                int existencias = int.Parse(txtExistencia.Text);


                if (codigo != "" && nombre != "" && descripcion != "" && precio_publico > 0 && existencias > 0)
                {

                    string sql = "INSERT INTO productos (codigo, nombre, descripcion, precio_publico, existencias, idCategoria)" +
                        "VALUES ('" + codigo + "','" + nombre + "','" + descripcion + "','" + precio_publico + "','" + existencias + "', '"+ idCategoria +"')";

                    MySqlConnection conexionBD = conexion.Conexion();
                    conexionBD.Open();

                    try
                    {
                        MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                        comando.ExecuteNonQuery();

                        MessageBox.Show("Registro Guardado");
                        Limpiar();
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show("Error al guaradar" + ex.Message);

                    }
                    finally
                    {
                        conexionBD.Close();
                    }
                }else
                {
                    MessageBox.Show("Debe completar todos los campos");
                }
            }
            catch (FormatException fex)

            {
                MessageBox.Show("Datos incorrectos: " + fex.Message);
            }
        
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string codigo = txtCodigo.Text;
            MySqlDataReader reader = null;

            string sql = "SELECT id, codigo, nombre, descripcion, precio_publico, existencias, idCategoria FROM" +
                " productos WHERE codigo LIKE'" + codigo +"' LIMIT 1";
            MySqlConnection conexionBD = conexion.Conexion();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        txtId.Text = reader.GetString(0);
                        txtCodigo.Text = reader.GetString(1);
                        txtNombre.Text = reader.GetString(2);
                        txtDescripcion.Text = reader.GetString(3);
                        txtPreciopublico.Text = reader.GetString(4);
                        txtExistencia.Text = reader.GetString(5);
                        cbxCategoria.SelectedValue = reader.GetString(6);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron registros");
                }

            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al buscar" + ex.Message);
               
            }
            finally
            {
                conexionBD.Close();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
            string codigo = txtCodigo.Text;
            string nombre = txtNombre.Text;
            string descripcion = txtDescripcion.Text;
            double precio_publico = double.Parse(txtPreciopublico.Text);
            int existencias = int.Parse(txtExistencia.Text);
            int idCategoria = int.Parse(cbxCategoria.SelectedValue.ToString());

            string sql = "UPDATE productos SET codigo='"+ codigo +"', nombre='"+ nombre +"', " +
                "descripcion='"+ descripcion+"', precio_publico='"+ precio_publico +"', " +
                "existencias='"+ existencias + "', idCategoria='"+idCategoria+"' WHERE id='"+ id + "'";

            MySqlConnection conexionBD = conexion.Conexion();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.ExecuteNonQuery();

                MessageBox.Show("Registro Actualizado");
                Limpiar();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al actualizar" + ex.Message);

            }
            finally
            {
                conexionBD.Close();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        private void Limpiar()
        {
            txtId.Text = "";
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtDescripcion.Text = "";
            txtPreciopublico.Text = "";
            txtExistencia.Text = "";
            cbxCategoria.Text = "";

        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            string id = txtId.Text;
           

            string sql = "DELETE FROM productos  WHERE id='" + id + "'";

            MySqlConnection conexionBD = conexion.Conexion();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.ExecuteNonQuery();

                MessageBox.Show("Registro Elimindo");
                Limpiar();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error al Eliminar" + ex.Message);

            }
            finally
            {
                conexionBD.Close();
            }
        }

        private void cargarCategorias()
        {
            cbxCategoria.DataSource = null;
            cbxCategoria.Items.Clear();
            string sql = "SELECT id, nombre FROM categorias ORDER BY nombre ASC";

            MySqlConnection conexionBD = conexion.Conexion();
            conexionBD.Open();

            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                MySqlDataAdapter data = new MySqlDataAdapter(comando);
                DataTable dt = new DataTable();
                data.Fill(dt);

                cbxCategoria.ValueMember = "id";
                cbxCategoria.DisplayMember = "nombre";
                cbxCategoria.DataSource = dt;
            }
            catch (MySqlException ex)
            {

                MessageBox.Show("Error al cargar categoria" + ex.Message);
            }
            finally
            {
                conexionBD.Close();
            }
        }

       
        private void txtCodigo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >=32 && e.KeyChar <=44) || (e.KeyChar >= 58 && e.KeyChar <= 255))
            {
                MessageBox.Show("SOlo numeros", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 32 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96) || (e.KeyChar >= 123 && e.KeyChar <= 255))
            {
                MessageBox.Show("SOlo letras", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }
    }
}
