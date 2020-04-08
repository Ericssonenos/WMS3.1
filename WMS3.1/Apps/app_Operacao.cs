using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WMS3._1.Models;
using WMS3._1.Models.Serializar.NFeProca_pasta.NFe_pasta;
using WMS3._1.Models.Serializar.NFeProca_pasta.NFe_pasta.Detalhes_pasta;
using WMS3._1.Models.Serializar.NFeProca_pasta.NFe_pasta.Detalhes_pasta.Rastro_pasta;
using static WMS3._1.Models.Serializar.NFeProca_pasta.cl_NFe;


namespace WMS3._1.Apps
{
    public class app_Operacao 
    {
        // variaveis de banco de dados
        SqlCommand comando;
        SqlDataReader Ler;

        public app_Operacao()
        {
            comando = new app_SqlConnection().Conecar();
        }

        public Boolean RegistrarXML(cl_InfNFe _XML)
        {
            // crio uma variavel Lote para guardar as informações para o castro do mesmo
            var Lote = new cl_03_Lote();
            Lote.NF = new cl_02_Nota_Fiscal();
            Lote.Produto = new cl_Produto();

            //Irá cadastrar a NF e retornar com o ID, se o ID for -1 retornar para o controler como false, ou seja, NF já cadastrada.
            Lote.NF.ID = new app_NF().Inserir(_XML);
            if (Lote.NF.ID == -1)
                return false;

            //Crio uma variavel Movimento para guardar as informações para o cadastro do mesmo
            var Movimento = new cl_05_Movimentacao();
            // id_Posição = 1, pois represanta Registro
            Movimento.ID_Posicao = 1;

            //Crio uma Acao Movimento para guardar as informações para o cadastro do mesmo
            var Acao = new cl_04_Acao();
            //Atribuo o usuario atual 
            Acao.Usuario = _XML.Usuario;

            // Iniciar Loop para obter o ID de cadas produto
            foreach (cl_Detalhes detalhes in _XML.Detalhes)
            {

                //Se ID do produto for igual a 0, procurar o id pelo codigo do Forncedor
                if (detalhes.Produto.CodigoI == null)
                    Lote.Produto.CodigoI = new app_Produto().Atulisar_CodigoF(detalhes.Produto);

                // atribuir o ID do produto na classe do lote
                Lote.Produto.ID = detalhes.Produto.ID;
                Lote.Valor = detalhes.Produto.Valor;

                foreach (cl_Rastro rastro in detalhes.Produto.rastro)
                {
                    Lote.Nome = rastro.Lote;
                    Lote.Fabricacao = rastro.Fabricacao;
                    Lote.Validade = rastro.Validade;
                    Acao.Lote = new cl_03_Lote();
                    Acao.Lote.ID = new app_Lote().Inserir(Lote);
                    Acao.Nome = "Registrar";
                    Acao.Inicio = _XML.Identificacao.Emisao;
                    Acao.Fim = DateTime.Now;
                    Movimento.Acao = new cl_04_Acao();
                    Movimento.Acao.ID = new app_acao().Inserir(Acao);


                    Movimento.Quantidade = rastro.Quantidade;
                    new app_Movimentacao().Inserir(Movimento);
                }
            }




            return true;

        }

        public int EntradaRomaneio(cl_01_Romaneio romaneio)
        {
            //Verificar se a NF exite e se está atrelada a algum romaneio
            romaneio.ID = new app_NF().SeExisteNF_e_Romanio(romaneio.NF.Chave);
            if (romaneio.ID != 0)
                return romaneio.ID;

            //Verificar se o romaneio já exite
            romaneio.ID = new app_Romaneio().ObterIDRomaneio(romaneio.Nome);
            if (romaneio.ID == -1)
                romaneio.ID = new app_Romaneio().Inserir(romaneio);

            //Inserir Romaneio Na NF
            new app_NF().InserirRomaneionaNF(romaneio);

            //Obter lotes na NF trabalhada
            var Lotes = new app_Lote().PesquisarPor(romaneio.NF.Chave);

            var Acao = new cl_04_Acao();
            Acao.Nome = "Entrada";
            Acao.Usuario = romaneio.Usuario;
            Acao.OBS = "";
            Acao.Inicio = DateTime.Now;
            Acao.Fim = DateTime.Now;
            int saída = 1;
            int Entrada = 2;
            var Movimentacao = new cl_05_Movimentacao();
            Movimentacao.ID_PreVenda = 0;


            foreach (int ID_Lote in Lotes)
            {
                Acao.Lote = new cl_03_Lote();
                Acao.Lote.ID = ID_Lote;
                Movimentacao.Acao = new cl_04_Acao();
                Movimentacao.Acao.ID = new app_acao().Inserir(Acao);

                //Movimentacao de Entrada 27/12/2019 11:38
                Movimentacao.ID_Posicao = Entrada;
                Movimentacao.Quantidade = new app_Lote().SomaPorPosicao_Lote(ID_Lote, saída);
                new app_Movimentacao().Inserir(Movimentacao);


                //Movimentacao de Saída 27/12/2019 11:53 - 12:40
                Movimentacao.ID_Posicao = saída;
                Movimentacao.Quantidade = -Movimentacao.Quantidade; // quantidade (-)
                new app_Movimentacao().Inserir(Movimentacao);


            }

            return 0;
        }

        public void ConferirLote(List<cl_03_Lote> _Lotes, string _Usuario)
        {
            var Acao = new cl_04_Acao();
            Acao.Lote = new cl_03_Lote();
            Acao.Nome = "Conferir";
            Acao.Inicio = DateTime.Now;
            Acao.Fim = DateTime.Now;
            Acao.Usuario = _Usuario;
            Acao.OBS = "";


            //Pecorrer cada lote
            foreach (cl_03_Lote lote in _Lotes)
            {
                if (lote.Status != "OK" && lote.Conferir != 0)
                {

                    //Acao.Lote = new cl_03_Lote();

                    Acao.Lote.ID = lote.ID;
                    if (Igual(lote.Quantidade, lote.Conferir))
                    {
                        //Atualizar Status
                        new app_Lote().AtulizarStatus(lote.ID);
                        //Ação
                        Acao.ID = new app_acao().Inserir(Acao);


                    }
                    else
                    {
                        Acao.OBS = "(" + lote.Conferir + "/" + lote.Quantidade + ") Error";

                        Acao.ID = new app_acao().Inserir(Acao);

                    }
                }
            }

        }

        public void ArmazenarLote(cl_0511_Armazem Armazem, string _Usuario)
        {
            var Acao = new cl_04_Acao();
            Acao.Nome = "Armazenar";
            Acao.Usuario = _Usuario;
            Acao.OBS = "";
            Acao.Inicio = DateTime.Now;
            Acao.Fim = DateTime.Now;
            Acao.Lote = new cl_03_Lote();
            Acao.Lote.ID = Armazem.Lote.ID;
            var Movimentacao = new cl_05_Movimentacao();
            Movimentacao.Acao = new cl_04_Acao();
            //Gerar Ação
            Movimentacao.Acao.ID = new app_acao().Inserir(Acao);
            Movimentacao.ID_PreVenda = 0;

            //Movimentacao de Entrada 27/12/2019 11:38
            int Entrada = pesquisarposicao(Armazem.Posicao.Nome);
            Movimentacao.ID_Posicao = Entrada;
            Movimentacao.Quantidade = Armazem.Lote.Conferir;
            new app_Movimentacao().Inserir(Movimentacao);

            //Movimentacao de Saída 27/12/2019 11:53 - 12:40
            int saída = Armazem.ID_Saida;
            Movimentacao.ID_Posicao = saída;
            Movimentacao.Quantidade = -Movimentacao.Quantidade; // quantidade (-)
            new app_Movimentacao().Inserir(Movimentacao);
        }


        public void SepararPreVenda(int _ID_Produto, int _ID_Prevenda, float _Conferir, string _Usuario)
        {
            //Crio uma Acao Movimento para guardar as informações para o cadastro do mesmo
            var Acao = new cl_04_Acao();
            Acao.Usuario = _Usuario;
            Acao.Nome = "Pre-Nota";
            Acao.Inicio = DateTime.Now;
            Acao.Fim = DateTime.Now;
            Acao.OBS = "";
            //Crio uma variavel Movimento para guardar as informações para o cadastro do mesmo
            var Movimento = new cl_05_Movimentacao();


            var Lista = new app_Lote().Lista_disponivel_Posicao(_ID_Produto);

            double separado = 0.0;

            for (int x =0; separado < _Conferir;x++)
            {
                //obter o lote a ser seperado
                Acao.Lote = new cl_03_Lote();
                Acao.Lote.ID = Lista[x].ID;
                Movimento.Acao = new cl_04_Acao();
                Movimento.Acao.ID = new app_acao().Inserir(Acao);

                if ((separado + Lista[x].Disponível) > _Conferir )
                {
                    Movimento.Quantidade = _Conferir - separado ;
                    separado = _Conferir;
                }
                else
                {
                    Movimento.Quantidade = Lista[x].Disponível;
                    separado += Lista[x].Disponível;
                }
                Movimento.ID_PreVenda = _ID_Prevenda;
                Movimento.ID_Posicao = Lista[x].Posicao.ID;
                new app_Movimentacao().Inserir(Movimento);

                Movimento.Quantidade = -Movimento.Quantidade;
                Movimento.ID_PreVenda = 0;
                Movimento.ID_Posicao = Lista[x].Posicao.ID;
                new app_Movimentacao().Inserir(Movimento);



            }
        }


        // métodos privados
        private Boolean Igual(Double _Quantidade, Double _Conferir)
        {
            if (_Quantidade == _Conferir)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int pesquisarposicao(string posicao)
        {
            comando.Parameters.Clear();
            comando.Parameters.AddWithValue("@posicao", posicao);
            comando.CommandText =
                "SELECT ID_Posicao "+
                "FROM [_07_Posicoes] "+
                "WHERE Posicao = @posicao";
           
            Ler = comando.ExecuteReader();
            Ler.Read();
            int  Retorno = int.Parse(Ler[0].ToString());
            Ler.Close();
            return Retorno;

        }


    }
}