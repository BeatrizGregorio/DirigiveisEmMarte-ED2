using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace apCaminhosMarte
{
    public partial class Form1 : Form
    {
        private Arvore<Cidade> cidades = new Arvore<Cidade>();
        int quantosdados = 0;
        private FilaLista<Caminho> caminhos = new FilaLista<Caminho>();
        bool podeMostrar = false;
        Cidade origem, destino;
        int[,] rotasMatriz;
        public Form1()
        {
            InitializeComponent();
        }
        public void lerArquivos()
        {
            StreamReader leitor = new StreamReader("F:\\ED2\\Projeto2_DirigiveisEmMarte\\CidadesMarte.txt", Encoding.UTF7, true);

            string linha = leitor.ReadLine();

            cidades.Raiz = new NoArvore<Cidade>(new Cidade(linha));

            while (!leitor.EndOfStream)
            {
                linha = leitor.ReadLine();
                cidades.Incluir(new Cidade(linha));
            }

            leitor.Close();

            rotasMatriz = new int[cidades.QuantosDados + 1, cidades.QuantosDados + 1];

            leitor = new StreamReader("F:\\ED2\\Projeto2_DirigiveisEmMarte\\CidadesMarteOrdenado.txt", Encoding.UTF7, true);

            int indice = 1;

            rotasMatriz[0, 0] = -1;

            while (!leitor.EndOfStream)
            {
                linha = leitor.ReadLine();
                Cidade nova = new Cidade(linha);
                cidades.Atual = new NoArvore<Cidade>(nova);

                rotasMatriz[indice, 0] = nova.Cod;
                rotasMatriz[0, indice] = nova.Cod;

                lsbOrigem.Items.Add(nova.Cod + " - " + nova.Nome);
                lsbDestino.Items.Add(nova.Cod + " - " + nova.Nome);

                indice++;
            }

            leitor.Close();

            leitor = new StreamReader("F:\\ED2\\Projeto2_DirigiveisEmMarte\\CaminhosEntreCidadesMarte.txt", Encoding.UTF7, true);

            while (!leitor.EndOfStream)
            {
                linha = leitor.ReadLine();
                Caminho novo = new Caminho(linha);
                caminhos.InserirAposFim(novo);

                for (int i = 1; i < cidades.QuantosDados + 1; i++)
                {
                    if (i - 1 == novo.CodOrigem)
                    {
                        for (int a = 1; a < cidades.QuantosDados + 1; a++)
                        {
                            if (a - 1 == novo.CodDestino)
                            {
                                rotasMatriz[i, a] = novo.Distancia;
                            }
                        }
                    }
                }
            }

            leitor.Close();
            quantosdados = cidades.QuantosDados;
            cidades.OndeExibir = tpArvore;
        }

        private void TxtCaminhos_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Buscar caminhos entre cidades selecionadas");
        }
    }
}
