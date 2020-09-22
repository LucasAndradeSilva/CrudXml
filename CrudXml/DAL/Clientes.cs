using CrudXml.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CrudXml.DAL
{
    public class Clientes
    {
        private List<Cliente> _clientes;

        public Clientes()
        {
            this._clientes = new List<Cliente>();
        }

        public void Adicionar(Cliente cliente)
        {
            if (this._clientes.Count(c => c.Email.Equals(cliente.Email)) > 0)
            {
                throw new Exception("Já existe um cliente com este Email");
            }
            else
            {
                this._clientes.Add(cliente);
            }
        }

        public void Remover(Cliente cliente)
        {
            this._clientes.RemoveAll(x => x.Id == cliente.Id);
        }

        public List<Cliente> ListarTodos()
        {
            return this._clientes;
        }

        public void Salvar()
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Cliente>));
            FileStream fs = new FileStream("C://Users//lucas.andrade//source//repos//Testes//CrudXml//DataXml//Clientes.xml", FileMode.OpenOrCreate);
            ser.Serialize(fs, this._clientes);
            fs.Close();
        }

        public void Carregar()
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<Cliente>));
            FileStream fs = new FileStream("C://Users//lucas.andrade//source//repos//Testes//CrudXml//DataXml//Clientes.xml", FileMode.OpenOrCreate);
            try
            {
                this._clientes = ser.Deserialize(fs) as List<Cliente>;
            }
            catch (InvalidOperationException ex)
            {
                ser.Serialize(fs, this._clientes);
            }
            finally
            {
                fs.Close();
            }
        }
    }
}
