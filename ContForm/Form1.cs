using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ContForm
{
    public partial class Form1 : Form
    {
        Contatos contatos = new Contatos();
        List<string> nums = new List<string>();
        List<string> tipo = new List<string>();

        public Form1()
        {
            InitializeComponent();
            cmbTipo.SelectedIndex = 0;
        }

        public void listaDeNumeros()
        {
            lblNum.Items.Clear();
            lblTipo.Items.Clear();

            for (int i = 0; i < this.nums.Count; i++)
            {
                lblNum.Items.Add(this.nums[i]);
                lblTipo.Items.Add(this.tipo[i]);
            }
        }

        public void limparForm()
        {
            txtEmail.Text = "";
            txtName.Text = "";
            txtNum.Text = "";
            cmbTipo.SelectedIndex = 0;
            lblNum.Items.Clear();
            lblTipo.Items.Clear();
            this.nums.Clear();
            this.tipo.Clear();
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            if(txtEmail.Text != "" || txtName.Text != "" || txtNum.Text != "")
            {
                result = MessageBox.Show("Tem certeza que deseja limpar o formulário?","", buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    limparForm();
                }
            }
            
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if(txtEmail.Text != "")
            {
                List<Fone> teladd = new List<Fone>();
                for (int i = 0; i < this.nums.Count; i++)
                {
                    Fone newtel = new Fone(this.nums[i], this.tipo[i]);
                    teladd.Add(newtel);
                }

                contatos.Adicionar(new Contato(txtEmail.Text, txtName.Text, teladd));

                MessageBox.Show("Contato salvo");
            }
            else
            {
                MessageBox.Show("ERRO: Campo E-mail não pode estar em branco");
            }

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            
            string emailpesquisa = txtEmail.Text;
            Contato contatoPesquisa = contatos.Pesquisar(new Contato(emailpesquisa));
            if (contatoPesquisa.Email == emailpesquisa)
            {
                txtEmail.Text = contatoPesquisa.Email;
                txtName.Text = contatoPesquisa.Nome;

                this.nums.Clear();
                this.tipo.Clear();

                if(txtEmail.Text != "")
                {
                    for (int i = 0; i < contatoPesquisa.Telefones.Count; i++)
                    {
                        this.nums.Add(contatoPesquisa.Telefones[i].Numero);
                        this.tipo.Add(contatoPesquisa.Telefones[i].Tipo);
                    }
                }
                

                listaDeNumeros();
            }

            else
                MessageBox.Show("Contato não encontrado");
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            string emailexclui = txtEmail.Text;
            contatos.Remover(new Contato(emailexclui));
            MessageBox.Show("Usuário removido");
            limparForm();
        }

        private void textBoxNum_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            this.nums.Add(txtNum.Text);
            this.tipo.Add(cmbTipo.Text);

            listaDeNumeros();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            this.tipo.RemoveAt(this.nums.IndexOf(txtNum.Text));
            this.nums.Remove(txtNum.Text);
            

            listaDeNumeros();
        }

        private void ListBoxNum_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTipo.SelectedIndex = lblNum.SelectedIndex;
            txtNum.Text = lblNum.SelectedItem.ToString();
            cmbTipo.Text = lblTipo.Items[lblNum.SelectedIndex].ToString();
        }
    }
}
