using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Xml;


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

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            
                Mixml._Añadir(txtDocumento.Text, txtNombre.Text, txtDireccion.Text, txtTelefono.Text, txtEmail.Text, txtDatosAdicionales.Text);
            
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

        private void dgEmpleados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(Mixml.rutaXml);
        }
    }
}
