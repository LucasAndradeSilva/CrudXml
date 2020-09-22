using CrudXml.DAL;
using CrudXml.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrudXml
{
    public partial class Crud : Form
    {
        private Clientes clientes;
        public Crud()
        {
            InitializeComponent();
        }

        private void Crud_Load(object sender, EventArgs e)
        {
            clientes = new Clientes();
            clientes.Carregar();
            dgvClientes.DataSource = clientes.ListarTodos();            
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            Home frm = new Home();
            frm.Show();
            this.Hide();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNome.Text) || string.IsNullOrEmpty(txtEmail.Text))
            {
                Alert("Preencha todos os campos!");
                return;
            }

            Cliente cli = new Cliente()
            {
                Id = GerarId(),
                Nome = txtNome.Text,
                Email = txtEmail.Text
            };

            try
            {
                clientes.Adicionar(cli);
                clientes.Salvar();
                dgvClientes.DataSource = null;
                dgvClientes.DataSource = clientes.ListarTodos();
            }
            catch (Exception ex)
            {
                Alert(ex.Message);
            }            
        }

        private void Alert(string msg)
        {
            MessageBox.Show("Mensagem, " + msg, "Ops", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private int GerarId()
        {
            Random numAleatorio = new Random();
            return numAleatorio.Next(10, 21);
        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgvClientes.Rows[e.RowIndex];
            var cliente = new Cliente
            {
                Id = Convert.ToInt32(row.Cells[0].Value),
                Nome = row.Cells[1].Value.ToString(),
                Email = row.Cells[2].Value.ToString()
            };

            var result = MessageBox.Show("Tem certeza que deseja deletar o cliente "+cliente.Nome+"?", "DELETAR", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                clientes.Remover(cliente);
                clientes.Salvar();
                clientes.Carregar();
            }
        }
    }
}
