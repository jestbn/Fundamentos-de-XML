using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Data;

namespace Fundamentos_de_XML
{
    class XML
    {
        public XmlDocument doc;
        public string rutaXml;

        public void _crearArchivo()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = @"C:\";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.FileName = "Empleados.xml";
            saveFileDialog.DefaultExt = "xml";
            saveFileDialog.Filter = "XML-File | *.xml";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                Stream fileStream = saveFileDialog.OpenFile();
                StreamWriter sw = new StreamWriter(fileStream);
                rutaXml = saveFileDialog.FileName;
                sw.Write(rutaXml);
                fileStream.Close();
                _crearXml(rutaXml, "Empleados");
            }
        }

        public void _cargarArchivo()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.RestoreDirectory = true;
            openFileDialog.FileName = "Empleados.xml";
            openFileDialog.DefaultExt = "xml";
            openFileDialog.Filter = "XML-File | *.xml";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                rutaXml = openFileDialog.FileName;
            }
        }


        public void _crearXml(string ruta, string nodoRaiz)
        {

            this.rutaXml = ruta;
            doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);

            XmlNode root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            XmlNode element1 = doc.CreateElement(nodoRaiz);
            doc.AppendChild(element1);
            try
            {
                doc.Save(ruta);
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Se ha producido un error : "+e.ToString());
            }
            
        }
        public void _Añadir(
            string Documento,
            string Nombre,
            string Direccion,
            string Telefono,
            string Email,
            //string Rol,
            //DateTime f_ingreso
            //DateTime f_retiro
            string datosadicionales
            )
        {
            doc = new XmlDocument();
            doc.Load(rutaXml);
            XmlNode empleado = _Crear_Empleado(Documento, Nombre, Direccion,Telefono,Email,
                //Rol,
                datosadicionales);
            XmlNode nodoRaiz = doc.DocumentElement;
            nodoRaiz.InsertAfter(empleado, nodoRaiz.LastChild);
            doc.Save(rutaXml);
        }

        private XmlNode _Crear_Empleado(string Documento,
            string Nombre,
            string Direccion,
            string Telefono,
            string Email,
            //string Rol,
            //DateTime f_ingreso
            //DateTime f_retiro
            string datosadicionales
            )
        {

            XmlNode empleado = doc.CreateElement("Empleado");


            XmlElement xid = doc.CreateElement("Documento");
            xid.InnerText = Documento;
            empleado.AppendChild(xid);

            XmlElement xnombre = doc.CreateElement("Nombre");
            xnombre.InnerText = Nombre;
            empleado.AppendChild(xnombre);

            XmlElement xdireccion = doc.CreateElement("Dirección");
            xdireccion.InnerText = Direccion;
            empleado.AppendChild(xdireccion);

            XmlElement xtelefono = doc.CreateElement("Teléfono");
            xtelefono.InnerText = Telefono;
            empleado.AppendChild(xtelefono);

            XmlElement xemail = doc.CreateElement("Email");
            xemail.InnerText = Email;
            empleado.AppendChild(xemail);

            /*XmlElement xrol = doc.CreateElement("Rol");
            xdireccion.InnerText = Rol;
            empleado.AppendChild(xrol); */

            XmlElement xdatosa = doc.CreateElement("DatosAdicionales");
            xdatosa.InnerText = datosadicionales;
            empleado.AppendChild(xdatosa);

            return empleado;
        }

        public void _DeleteNodo(string id_borrar)
        {
            doc = new XmlDocument();
            doc.Load(rutaXml);
            XmlNode empleados = doc.DocumentElement;
            XmlNodeList listaEmpleados = doc.SelectNodes("Empleados/Empleado");
            foreach (XmlNode item in listaEmpleados)
            {
                if (item.SelectSingleNode("Documento").InnerText == id_borrar)
                {
                    XmlNode nodoOld = item;
                    empleados.RemoveChild(nodoOld);
                    break;
                }
            }
            doc.Save(rutaXml);
        }

        public void _UpdateXml(string id_update, string Nombre, string Direccion,
            string Telefono,
            string Email,
            //string Rol,
            //DateTime f_ingreso
            //DateTime f_retiro
            string datosadicionales
            )
        {
            doc = new XmlDocument();
            doc.Load(rutaXml);
            XmlElement empleados = doc.DocumentElement;
            XmlNodeList listaEmpleados = doc.SelectNodes("Empleados/Empleado");
            XmlNode nuevo_empleado = _Crear_Empleado(id_update, Nombre, Direccion, Telefono, Email,
                //Rol,
                datosadicionales);
            foreach (XmlNode item in listaEmpleados)
            {
                if (item.FirstChild.InnerText == id_update)
                {
                    XmlNode nodoOld = item;
                    empleados.ReplaceChild(nuevo_empleado, nodoOld);
                    break;
                }
            }
            doc.Save(rutaXml);
        }
    }
}
