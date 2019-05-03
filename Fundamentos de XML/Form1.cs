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
                MessageBox.Show("Test");
            }
            else
            {
                MessageBox.Show("Se creo el documento");
                Mixml._crearXml("empleado.xml", "Empleados");
            }
        }    
    }
}
