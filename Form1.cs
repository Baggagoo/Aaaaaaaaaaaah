using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aaaaaaaaaaaah
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ContaCorrente cc = new ContaCorrente();

        Conexao con = new Conexao();

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnExcluirConta_Click(object sender, EventArgs e)
        {
            cc = new ContaCorrente();
            cc.Num_Conta = txtNumConta.Text;

            string sql = $"delete from tb_conta where num_conta='{cc.Num_Conta}';";

            if (con.Conectar())
            {
                if (con.Executa(sql)) MessageBox.Show("Exclusão realizada com sucesso!!");

                else MessageBox.Show("Error ao excluir.");

            }
            else MessageBox.Show("Erro ao acessar o banco de dados.");

            txtNumConta.Text = "";
            txtAgencia.Text = "";
            txtLimite.Text = "";
            txtSaldo.Text = "";
            TxtTitular.Text = "";


            con.Desconectar();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {

            cc = new ContaCorrente();
            cc.Num_Conta = txtNumConta.Text;

            if (con.Conectar())
            {
                string sql = $"select * from tb_conta where num_conta='{cc.Num_Conta}';";
                DataTable dados = con.Retorna(sql);
                
                    txtAgencia.Text = dados.Rows[0]["agencia"].ToString();
                    cc.Saldo =Convert.ToDouble(dados.Rows[0]["saldo"].ToString());
                    TxtTitular.Text = dados.Rows[0]["titular"].ToString();
                    txtLimite.Text = dados.Rows[0]["limite"].ToString();
                    

                con.Desconectar();
            }else
            {
                MessageBox.Show("Erro aoconectar ao banco!!");
            }

        }

        private void btnDeposito_Click(object sender, EventArgs e)
        {
                cc.Num_Conta = txtNumConta.Text;
            
                cc.Depositar(Double.Parse(txtValor.Text));

                txtSaldo.Text = cc.Saldo.ToString();

                string sql = $"update tb_conta set saldo = {cc.Saldo} where num_conta='{cc.Num_Conta}';";

                if (con.Conectar())
                {
                    if (con.Executa(sql)) MessageBox.Show("Deposito realizado com sucesso!!");

                    else MessageBox.Show("Error ao depositar.");

                    txtValor.Text = "";

                }
                else MessageBox.Show("Erro ao acessar o banco de dados.");

                con.Desconectar();
            

        }

        private void btnSacar_Click(object sender, EventArgs e)
        {
            cc.Sacar(Convert.ToDouble(txtValor.Text));
            txtSaldo.Text = cc.Saldo.ToString();

            string sql = $"update tb_conta set saldo = {cc.Saldo} where num_conta='{cc.Num_Conta}';";

            if (con.Conectar())
            {
                if (con.Executa(sql)) MessageBox.Show("Saque realizado com sucesso!!");

                else MessageBox.Show("Error ao sacar.");

            }
            else MessageBox.Show("Erro ao acessar o banco de dados.");

            txtValor.Text = "";
            con.Desconectar();
        }

        private void btnCriarConta_Click(object sender, EventArgs e)
        {
            cc.Agencia = txtAgencia.Text;
            cc.Titular = TxtTitular.Text;
            cc.Num_Conta = txtNumConta.Text;
            cc.Saldo = Double.Parse(txtSaldo.Text);
            cc.Limite = Double.Parse(txtLimite.Text);

            string sql = $"insert into tb_conta values ('{cc.Agencia}', '{cc.Num_Conta}','{cc.Titular}'," +
                $"{cc.Saldo}, {cc.Limite})";
            if (con.Conectar())
            {
                if (con.Executa(sql))
                {
                    MessageBox.Show("Cadastro Criado com Sucesso!!");
                }
                else
                {
                    MessageBox.Show("Error ao cadastrar o Cliente.");
                }
            }
            else { MessageBox.Show("Erro ao acessaro banco de dados."); }

            con.Desconectar();

        }

        private void btnExibirDados_Click(object sender, EventArgs e)
        {
            if (con.Conectar())
            {
                string sql = $"select * from tb_conta where num_conta='{cc.Num_Conta}';";
                DataTable dados = con.Retorna(sql);
                string texto = "";

                for (int i = 0; i < dados.Rows.Count; i++)
                {
                    texto += "Agência: " + dados.Rows[i]["Agencia"].ToString();
                    texto += "Saldo: " + dados.Rows[i]["Saldo"].ToString();
                    texto += "Titular: " + dados.Rows[i]["Titular"].ToString();
                    texto += "Limite: " + dados.Rows[i]["Limite"].ToString();
                    texto += "\n\n";
                }
                MessageBox.Show(texto);
                con.Desconectar();
            }
            else
            {
                MessageBox.Show("Erro aoconectar ao banco!!");
            }

        }
    }
}
