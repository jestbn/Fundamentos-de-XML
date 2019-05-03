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
        public void _Añadir(string id, string nom, string ape, string dir)
        {
            doc.Load(rutaXml);
            XmlNode empleado = _Crear_Empleado(id, nom, ape, dir);
            XmlNode nodoRaiz = doc.DocumentElement;
            nodoRaiz.InsertAfter(empleado, nodoRaiz.LastChild);
            doc.Save(rutaXml);
        }

        private XmlNode _Crear_Empleado(string id, string nom, string ape, string dir)
        {

            XmlNode empleado = doc.CreateElement("persona");
            XmlElement xid = doc.CreateElement("id");
            xid.InnerText = id;
            empleado.AppendChild(xid);
            XmlElement xnombre = doc.CreateElement("nombre");
            xnombre.InnerText = nom;
            empleado.AppendChild(xnombre);
            XmlElement xapellidos = doc.CreateElement("apellidos");
            xapellidos.InnerText = ape;
            empleado.AppendChild(xapellidos);


            XmlElement xdireccion = doc.CreateElement("direccion");
            xdireccion.InnerText = dir;
            empleado.AppendChild(xdireccion);

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

        public void _UpdateXml(string id_update, string nom, string ape, string dir)
        {
            XmlElement empleados = doc.DocumentElement;
            XmlNodeList listaEmpleados = doc.SelectNodes("Empleados/persona");
            XmlNode nuevo_empleado = _Crear_Empleado(id_update, nom, ape, dir);
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
