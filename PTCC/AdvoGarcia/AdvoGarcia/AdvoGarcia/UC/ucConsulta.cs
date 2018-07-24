﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AdvoGarcia.Entidades;
using AdvoGarcia.DAO;
using AdvoGarcia.Telas;

namespace AdvoGarcia.UC
{
    public partial class ucConsulta : UserControl
    {
        EntidadesContext contexto = new EntidadesContext();

        public ucConsulta()
        {
            InitializeComponent();
            cboTipoCon.Items.Add(TipoCon.Advogado);
            cboTipoCon.Items.Add(TipoCon.Caso);
            cboTipoCon.Items.Add(TipoCon.Cliente);
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.MultiSelect = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.ReadOnly = true;
        }

        public enum TipoCon
        {
            Advogado, Caso, Cliente
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cboTipoCon.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione o tipo de consulta","Atenção");
            }
            else
            {
                if (txtNome.Text.Trim().Equals(string.Empty))
                {
                    switch (cboTipoCon.SelectedItem)
                    {
                        case TipoCon.Advogado:
                            Visivel();
                            var buscaA = from a in contexto.Advogados select a;
                            dataGridView1.DataSource = buscaA.ToList();
                            break;
                        case TipoCon.Caso:
                            Oculto();
                            var buscaC = from c in contexto.Casos select c;
                            dataGridView1.DataSource = buscaC.ToList();
                            break;
                        case TipoCon.Cliente:
                            Visivel();
                            var buscaCl = from c in contexto.Clientes select c;
                            dataGridView1.DataSource = buscaCl.ToList();
                            break;
                    }
                }
                else
                {
                    switch (cboTipoCon.SelectedItem)
                    {
                        case TipoCon.Advogado:
                            Visivel();
                            var buscaA = from a in contexto.Advogados where a.Nome.Equals(txtNome.Text) select a;
                            dataGridView1.DataSource = buscaA.ToList();
                            break;
                        case TipoCon.Caso:
                            Oculto();
                            var buscaC = from c in contexto.Casos select c;
                            dataGridView1.DataSource = buscaC.ToList();
                            break;
                        case TipoCon.Cliente:
                            Visivel();
                            var buscaCl = from c in contexto.Clientes where c.Nome.Equals(txtNome.Text) select c;
                            dataGridView1.DataSource = buscaCl.ToList();
                            break;
                    }
                }
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (txtNome.Text.Trim().Equals(string.Empty))
            {
                switch (cboTipoCon.SelectedItem)
                {
                    case TipoCon.Advogado:
                        Visivel();
                        DataGridViewRow linhaAtual = dataGridView1.CurrentRow;
                        int indice = linhaAtual.Index;
                        AdvogadoDAO adao = new AdvogadoDAO();
                        Advogado aa = adao.BuscaPorID(Convert.ToInt32(dataGridView1.Rows[indice].Cells["ID"].Value));
                        adao.Remover(aa);
                        MessageBox.Show("Removido", "Atenção");
                        var buscaA = from a in contexto.Advogados select a;
                        dataGridView1.DataSource = buscaA.ToList();
                        break;
                    case TipoCon.Caso:
                        Oculto();
                        var buscaC = from c in contexto.Casos select c;
                        dataGridView1.DataSource = buscaC.ToList();
                        break;
                    case TipoCon.Cliente:
                        Visivel();
                        DataGridViewRow linhaAtualC = dataGridView1.CurrentRow;
                        int indiceC = linhaAtualC.Index;
                        ClienteDAO cdao = new ClienteDAO();
                        Cliente cc = cdao.BuscaPorID(Convert.ToInt32(dataGridView1.Rows[indiceC].Cells["ID"].Value));
                        cdao.Remover(cc);
                        MessageBox.Show("Removido", "Atenção");
                        var buscaCl = from c in contexto.Clientes select c;
                        dataGridView1.DataSource = buscaCl.ToList();
                        break;
                }
            }
            else
            {
                switch (cboTipoCon.SelectedItem)
                {
                    case TipoCon.Advogado:
                        Visivel();
                        DataGridViewRow linhaAtual = dataGridView1.CurrentRow;
                        int indice = linhaAtual.Index;
                        AdvogadoDAO adao = new AdvogadoDAO();
                        Advogado aa = adao.BuscaPorID(Convert.ToInt32(dataGridView1.Rows[indice].Cells["ID"].Value));
                        adao.Remover(aa);
                        MessageBox.Show("Removido", "Atenção");
                        var buscaA = from a in contexto.Advogados where a.Nome.Equals(txtNome.Text) select a;
                        dataGridView1.DataSource = buscaA.ToList();
                        break;
                    case TipoCon.Caso:
                        Oculto();
                        var buscaC = from c in contexto.Casos select c;
                        dataGridView1.DataSource = buscaC.ToList();
                        break;
                    case TipoCon.Cliente:
                        Visivel();
                        DataGridViewRow linhaAtualC = dataGridView1.CurrentRow;
                        int indiceC = linhaAtualC.Index;
                        ClienteDAO cdao = new ClienteDAO();
                        Cliente cc = cdao.BuscaPorID(Convert.ToInt32(dataGridView1.Rows[indiceC].Cells["ID"].Value));
                        cdao.Remover(cc);
                        MessageBox.Show("Removido", "Atenção");
                        var buscaCl = from c in contexto.Clientes where c.Nome.Equals(txtNome.Text) select c;
                        dataGridView1.DataSource = buscaCl.ToList();
                        break;
                }
            }
        }

        public void Visivel() {
            label1.Visible = true;
            txtNome.Visible = true;
            btnExcluir.Enabled = true;
            btnEdit.Enabled = true;
        }

        public void Oculto()
        {
            label1.Visible = false;
            txtNome.Visible = false;
            btnExcluir.Enabled = false;
            btnEdit.Enabled = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            DataGridViewRow linhaAtualC = dataGridView1.CurrentRow;
            int indiceC = linhaAtualC.Index;
            ClienteDAO cdao = new ClienteDAO();

            switch (cboTipoCon.SelectedItem)
            {
                case TipoCon.Advogado:
                    frmAlt altA = new frmAlt(true, Convert.ToInt32(dataGridView1.Rows[indiceC].Cells["ID"].Value));
                    altA.ShowDialog();
                    break;
                case TipoCon.Cliente:
                    frmAlt altC = new frmAlt(false, Convert.ToInt32(dataGridView1.Rows[indiceC].Cells["ID"].Value));
                    altC.ShowDialog();
                    break;
            }
        }
    }
}