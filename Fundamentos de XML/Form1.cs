using System;
using System.Data;
using System.Windows.Forms;


namespace Fundamentos_de_XML
{
    public partial class Form1 : Form
    {
        XML Mixml = new XML();
        public Form1()
        {
            InitializeComponent();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string message = "Desea crear(Si) ó cargar(No) el archivo XML?";
            string title = "Atención";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Mixml._crearArchivo();
            }
            else
            {
                Mixml._cargarArchivo();
            }
            for (int i = 1; i <= 5; i++)
            {
                cbRol.Items.Add(i);
            }
            cbRol.Items.Add("NA");
            cbRol.SelectedItem = "NA";
            refrescaDG();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            Mixml._Añadir(txtDocumento.Text, txtNombre.Text, txtDireccion.Text, txtTelefono.Text, txtEmail.Text, cbRol.Text, dtmIngreso.Value, dtmRetiro.Value, txtDatosAdicionales.Text);
            refrescaDG();

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult rta;
            rta = MessageBox.Show("Desea salir?", "Mensaje de advertencia", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (rta == DialogResult.OK)
            {
                this.Close();
            }

        }

        public void refrescaDG()
        {
            DataSet dataSet = new DataSet();
            try
            {
                dataSet.ReadXml(Mixml.rutaXml);
                dgEmpleados.DataSource = dataSet.Tables[0];
            }
            catch (Exception e)
            {
                MessageBox.Show("Se ha presentado un error: " + e.ToString());
            }


        }

        private void btnReEstablecer_Click(object sender, EventArgs e)
        {
            txtDocumento.Clear();
            txtNombre.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            cbRol.SelectedItem = "NA";
            dtmIngreso.Value = DateTime.Now;
            dtmRetiro.Value = Convert.ToDateTime("01/01/1900");
            txtDatosAdicionales.Clear();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            Mixml._DeleteNodo(txtDocumento.Text);
            refrescaDG();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Mixml._UpdateXml(txtDocumento.Text, txtNombre.Text, txtDireccion.Text, txtTelefono.Text, txtEmail.Text, cbRol.Text, dtmIngreso.Value, dtmRetiro.Value, txtDatosAdicionales.Text);
            refrescaDG();
        }

        private void dgEmpleados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = this.dgEmpleados.Rows[e.RowIndex];
                txtDocumento.Text = row.Cells[0].Value.ToString();
                txtNombre.Text = row.Cells[1].Value.ToString();
                txtDireccion.Text = row.Cells[2].Value.ToString();
                txtTelefono.Text = row.Cells[3].Value.ToString();
                txtEmail.Text = row.Cells[4].Value.ToString();
                cbRol.SelectedItem = row.Cells[5].Value.ToString();
                dtmIngreso.Value = Convert.ToDateTime(row.Cells[6].Value.ToString());
                dtmRetiro.Value = Convert.ToDateTime(row.Cells[7].Value.ToString());
                txtDatosAdicionales.Text = row.Cells[8].Value.ToString();
            }
            catch (Exception error)
            {
                MessageBox.Show("Ha ocurrido un error en la carga de la informacion" + error.ToString());
            }

        }
    }
}
