using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;


namespace Fundamentos_de_XML
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        XML Mixml = new XML();
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (File.Exists("empleado.xml"))
            {
                MessageBox.Show("Se añadirá el nuevo nodo");
                Mixml._Añadir(txtDocumento.Text, txtNombre.Text, txtDireccion.Text, txtTelefono.Text, txtEmail.Text, txtDatosAdicionales.Text);

            }
            else
            {
                MessageBox.Show("Se creo el documento");
                Mixml._crearXml("empleado.xml", "Empleados");
                Mixml._Añadir(txtDocumento.Text, txtNombre.Text, txtDireccion.Text, txtTelefono.Text, txtEmail.Text, txtDatosAdicionales.Text);
            }
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
    }
}
