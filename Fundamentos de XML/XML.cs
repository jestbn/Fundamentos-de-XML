using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Fundamentos_de_XML
{
    class XML
    {
        XmlDocument doc;
        string rutaXml;

        public void _crearXml(string ruta, string nodoRaiz)
        {

            this.rutaXml = ruta;
            doc = new XmlDocument();
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);

            XmlNode root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            XmlNode element1 = doc.CreateElement(nodoRaiz);
            doc.AppendChild(element1);
            doc.Save(ruta);
        }
        public void _Añadir(string Documento,string Nombre,string Direccion,
            string Telefono,
            string Email,
            //string Rol,
            //DateTime f_ingreso
            //DateTime f_retiro
            string datosadicionales
            )
        {
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
            xdireccion.InnerText = Direccion;
            empleado.AppendChild(xtelefono);

            XmlElement xemail = doc.CreateElement("Email");
            xdireccion.InnerText = Email;
            empleado.AppendChild(xemail);

            /*XmlElement xrol = doc.CreateElement("Rol");
            xdireccion.InnerText = Rol;
            empleado.AppendChild(xrol); */

            XmlElement xdatosa = doc.CreateElement("DatosAdicionales");
            xdireccion.InnerText = datosadicionales;
            empleado.AppendChild(xdatosa);

            return empleado;
        }

        public void _ReadXml()
        {
            doc.Load(rutaXml);
            XmlNodeList listaEmpleados = doc.SelectNodes("Empleados/persona");
            XmlNode unEmpleado;
            for (int i = 0; i < listaEmpleados.Count; i++)
            {
                unEmpleado = listaEmpleados.Item(i);

                string id = unEmpleado.SelectSingleNode("id").InnerText;
                string nombre = unEmpleado.SelectSingleNode("nombre").InnerText;
                string apellidos = unEmpleado.SelectSingleNode("apellidos").InnerText;
                Console.Write("Id: {0}, Nombre: {1}, Apellidos: {2}\n", id, nombre, apellidos);
            }
        }

        public void _DeleteNodo(string id_borrar)
        {
            doc.Load(rutaXml);
            XmlNode empleados = doc.DocumentElement;
            XmlNodeList listaEmpleados = doc.SelectNodes("Empleados/persona");
            foreach (XmlNode item in listaEmpleados)
            {
                if (item.SelectSingleNode("id").InnerText == id_borrar)
                {
                    XmlNode nodoOld = item;
                    empleados.RemoveChild(nodoOld);
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
            XmlElement empleados = doc.DocumentElement;
            XmlNodeList listaEmpleados = doc.SelectNodes("Empleados/persona");
            XmlNode nuevo_empleado = _Crear_Empleado(id_update, Nombre, Direccion, Telefono, Email,
                //Rol,
                datosadicionales);
            foreach (XmlNode item in listaEmpleados)
            {
                if (item.FirstChild.InnerText == id_update)
                {
                    XmlNode nodoOld = item;
                    empleados.ReplaceChild(nuevo_empleado, nodoOld);
                }
            }
            doc.Save(rutaXml);
        }
    }
}
