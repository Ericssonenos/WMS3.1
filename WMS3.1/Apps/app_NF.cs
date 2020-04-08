using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WMS3._1.Models;
using WMS3._1.Models.Serializar;
using WMS3._1.Models.Serializar.NFeProca_pasta;
using WMS3._1.Models.Serializar.NFeProca_pasta.NFe_pasta;
using WMS3._1.Models.Serializar.NFeProca_pasta.NFe_pasta.Detalhes_pasta;
using WMS3._1.Models.Serializar.NFeProca_pasta.NFe_pasta.Detalhes_pasta.Rastro_pasta;
using static WMS3._1.Models.Serializar.NFeProca_pasta.cl_NFe;
using static WMS3._1.Models.Serializar.NFeProca_pasta.cl_NFe.cl_InfNFe;


namespace WMS3._1.Apps
{
    public  class  app_NF 
    {
        SqlCommand comando;
        SqlDataReader Ler;
        public app_NF() 
        {
            comando = new app_SqlConnection().Conecar();
        }

        public  cl_XML AddNFVazia()
        {

            List<cl_Rastro> _rastro()
            {
                var retorno = new List<cl_Rastro>
                {
                    new cl_Rastro
                    {
                        Lote = string.Empty
                    },
                    new cl_Rastro
                    {
                        Lote = string.Empty
                    },
                    new cl_Rastro
                    {
                        Lote = string.Empty
                    },
                    new cl_Rastro
                    {
                        Lote = string.Empty
                    },
                    new cl_Rastro
                    {
                        Lote = string.Empty
                    },
                    new cl_Rastro
                    {
                        Lote = string.Empty
                    },
                    new cl_Rastro
                    {
                        Lote = string.Empty
                    },
                    new cl_Rastro
                    {
                        Lote = string.Empty
                    },
                    new cl_Rastro
                    {
                        Lote = string.Empty
                    },
                    new cl_Rastro
                    {
                        Lote = string.Empty
                    },
                    new cl_Rastro
                    {
                        Lote = string.Empty
                    }
                };
                return retorno;
            }

            cl_Produto _Produto = new cl_Produto()
            {
                CodigoF = string.Empty,
                NomeF = string.Empty,
                Unidade_Medida = string.Empty,
                Quantidade = 0,
                Valor = 0,
                
              
                rastro = _rastro()
            };


            // cl_infNFe
            var _Compra = new cl_Compra { Pedido = string.Empty };
            var _Identificacao = new cl_Identificacao()
            {
                Emisao = DateTime.Now,
                Natureza_Operacao ="Nova Operação",
                Numero = string.Empty,
                serie = 0
            };
            var _Destinatario = new cl_Destinatario()
            {
                CNPJ = string.Empty,
                xNome = string.Empty,
                Codigo = string.Empty,
                CPF = string.Empty

            };
            var _Emitente = new cl_Emitente()
            {
                CNPJ = string.Empty,
                xNome = string.Empty,
                Codigo = string.Empty
                

            };

            List<cl_Detalhes> _Detalhes()
            {
                var retorno = new List<cl_Detalhes>
                {
                    //new cl_Detalhes {Produto = _Produto },
                    //new cl_Detalhes {Produto = _Produto },
                    //new cl_Detalhes {Produto = _Produto },
                    //new cl_Detalhes {Produto = _Produto },
                    //new cl_Detalhes {Produto = _Produto },
                    //new cl_Detalhes {Produto = _Produto },
                    //new cl_Detalhes {Produto = _Produto },
                    //new cl_Detalhes {Produto = _Produto },
                    //new cl_Detalhes {Produto = _Produto },
                    //new cl_Detalhes {Produto = _Produto }
                };
                return retorno;
                
            };


            // cl_NFe
            var _InfNFe = new cl_InfNFe()
            {
                Chave = string.Empty,
                Compra = _Compra,
                Identificacao = _Identificacao,
                Destinatario = _Destinatario,
                Emitente = _Emitente,
                Detalhes = _Detalhes()
                

            };

            var _Nfe = new cl_NFe()
            {
                InfNFe = _InfNFe
            };

            var _Xml = new cl_XML()
            {
                NFe = _Nfe
            };

            return _Xml;
        }

        public int SeExisteNF_e_Romanio(string Chave)
        {
            // ID_NF = -1 para caso a Chave já esteja cadastrada
            int ID_Romanio = -1;

            // Converter Parametros para o formato SQL
            comando.Parameters.AddWithValue("@Chave", Chave);
            // Definir comando de pesquida(Selecionar a coluna Chave pela chave)
            comando.CommandText = "SELECT ID_Romaneio FROM [_03_NF] " +
                "WHERE @Chave = Chave";
            //Executar comando
            Ler = comando.ExecuteReader();

            // verificar se já existe no banco de dados

            if (Ler.HasRows)
            {
                while (Ler.Read()) { ID_Romanio = int.Parse(Ler[0].ToString()); }
                Ler.Close();
                return ID_Romanio;
            }

            //fechar leitura 
            Ler.Close();
            return ID_Romanio;
        }
        
        public  int Inserir(cl_InfNFe _XML) 
        {
            // ID_NF = -1 para caso a Chave já esteja cadastrada
            if (SeExisteNF_e_Romanio(_XML.Chave) != -1)
                return -1;

            int ID_NF = 0;

            // Definir comando de pesquida(Selecionar o ID Maximo)
            comando.CommandText = "SELECT MAX(ID_NF) FROM [_03_NF] ";

            //Executar comando
            Ler = comando.ExecuteReader();

            //Se não exite dados no banco, o ID  = 1
            try
            {
                //Definir Id_NF
                while (Ler.Read()) { ID_NF = int.Parse(Ler[0].ToString()) + 1; }
            }
            catch
            {
                ID_NF = 1;
            }

            //Fechar leitura 
            Ler.Close();

            #region Converter + Parametros para o formato SQL
            comando.Parameters.AddWithValue("@ID_NF",ID_NF);
                comando.Parameters.AddWithValue("@Numero", _XML.Identificacao.Numero);
                comando.Parameters.AddWithValue("@serie", _XML.Identificacao.serie);
                comando.Parameters.AddWithValue("@Pedido", _XML.Compra.Pedido);
                comando.Parameters.AddWithValue("@Emisao", _XML.Identificacao.Emisao);
                comando.Parameters.AddWithValue("@Previsao", _XML.Identificacao.Previsao);
                comando.Parameters.AddWithValue("@Natureza_Operacao", _XML.Identificacao.Natureza_Operacao);
                comando.Parameters.AddWithValue("@Destinatario_ID", _XML.Destinatario.ID);
                comando.Parameters.AddWithValue("@Emitente_ID", _XML.Emitente.ID);
                comando.Parameters.AddWithValue("@ID_Romaneio", 0);
            //comando.Parameters.AddWithValue("@Recebimento",null);
            #endregion
            #region Definir comando de inserir
                comando.CommandText = "INSERT INTO [_03_NF] " +
                    "(ID_NF, " +
                    "Chave," +
                    "Numero, " +
                    "Serie, " +
                    "Pedido, " +
                    "Emissao, " +
                    "Previsao, " +
                    "Natureza, " +
                    "ID_Destinatario, " +
                    "ID_Fornecedor, " +
                    "ID_Romaneio ) " +
                    " VALUES( " +
                    "@ID_NF, " +
                    "@Chave, " +
                    "@Numero, " +
                    "@serie, " +
                    "@Pedido, " +
                    "@Emisao, " +
                    "@Previsao, " +
                    "@Natureza_Operacao, " +
                    "@Destinatario_ID, " +
                    "@Emitente_ID, " +
                    "@ID_Romaneio )";

            #endregion

            //Realizar comando
            comando.ExecuteNonQuery();

            //lipar os parametros
            comando.Parameters.Clear();

            return ID_NF;
        }
       
        public void InserirRomaneionaNF(cl_01_Romaneio romaneio)
        {
            // Converter Parcamentros para o formato SQL
            comando.Parameters.AddWithValue("@Chave", romaneio.NF.Chave);
            comando.Parameters.AddWithValue("@ID_Romaneio", romaneio.ID);
            comando.Parameters.AddWithValue("@Data", DateTime.Now);


           // Definir comando de inserir
            comando.CommandText = "UPDATE [_03_NF]" +
                "SET  " +
                "ID_Romaneio = @ID_Romaneio, " +
                "Recebimento = @Data " +
                "WHERE " +
                "Chave = @Chave";
            
            //Realizar Comando
            comando.ExecuteNonQuery();
            //Limpar os paramentros
            comando.Parameters.Clear();
            comando.Dispose();

            //fechar leitura 
          


        }

        public List<cl_02_Nota_Fiscal> PendetesDeEntrega()
        {
            

            comando.CommandText =
                "SELECT N.ID_NF, N.Numero, PS.NOME,N.Emissao, N.Previsao, " +
                "COUNT(DISTINCT l.ID_Produto) as Produtos, " + 
                "COUNT(DISTINCT l.Lote) as Lotes, " + 
                "SUM(M.Quantidade) as Itens " +
                "FROM _03_NF AS N " +
                "LEFT JOIN _01_Parceiros AS PS ON N.ID_Fornecedor = PS.ID " +
                "LEFT JOIN _04_Lotes AS L ON N.ID_NF = L.ID_NF " +
                "LEFT JOIN _05_Acao AS A ON L.ID_Lote = A.ID_Lote " +
                "LEFT JOIN _06_Movimentacoes AS M ON A.ID_Acao = M.ID_Acao " +
                "LEFT JOIN _07_Posicoes P ON M.ID_Posicao = P.ID_Posicao " +
                "WHERE P.ID_Posicao = 1 " +
                "group by N.Numero, PS.NOME, N.Emissao, N.Previsao, N.ID_NF " +
                "HAVING " +
                "SUM(M.Quantidade) > 0 " ;
            Ler = comando.ExecuteReader();
        
            var Lista = new List<cl_02_Nota_Fiscal>();
       
            
            while (Ler.Read())
            {
                Lista.Add(
                    new cl_02_Nota_Fiscal() {
                        ID = int.Parse(Ler[0].ToString()),
                        Numero = Ler[1].ToString(),
                        Fornecedor = new cl_021_Parceiro() 
                            { Nome = Ler[2].ToString() },
                        Emissao = DateTime.Parse( Ler[3].ToString()),
                        Previsao = DateTime.Parse( Ler[4].ToString()),
                        Qtd_Produtos = int.Parse(Ler[5].ToString()),
                        Qtd_Lotes = int.Parse( Ler[6].ToString()),
                        Total = int.Parse(Ler[7].ToString())
                    });

            }
            Ler.Close();
            return Lista;
        }

        public List<cl_02_Nota_Fiscal> PendetesDeConferir(int _ID_Romaneio)
        {
            comando.Parameters.AddWithValue("@ID_Romaneio", _ID_Romaneio);

            comando.CommandText =
                "SELECT N.ID_NF, N.Numero, PS.NOME,N.Emissao, N.Previsao, " +
                "COUNT(DISTINCT l.ID_Produto) as Produtos, " +
                "COUNT(DISTINCT l.Lote) as Lotes, " +
                "SUM(M.Quantidade) as Itens " +
                "FROM _03_NF AS N " +
                "LEFT JOIN _01_Parceiros AS PS ON N.ID_Fornecedor = PS.ID " +
                "LEFT JOIN _04_Lotes AS L ON N.ID_NF = L.ID_NF " +
                "LEFT JOIN _05_Acao AS A ON L.ID_Lote = A.ID_Lote " +
                "LEFT JOIN _06_Movimentacoes AS M ON A.ID_Acao = M.ID_Acao " +
                "LEFT JOIN _07_Posicoes P ON M.ID_Posicao = P.ID_Posicao " +
                "WHERE P.ID_Posicao = 2 " +
                "AND N.ID_Romaneio = @ID_Romaneio " +
                "group by N.Numero, PS.NOME, N.Emissao, N.Previsao, N.ID_NF " +
                "HAVING " +
                "SUM(M.Quantidade) > 0 ";
            Ler = comando.ExecuteReader();
            comando.Parameters.Clear();
            var Lista = new List<cl_02_Nota_Fiscal>();


            while (Ler.Read())
            {
                Lista.Add(
                    new cl_02_Nota_Fiscal()
                    {
                        ID = int.Parse(Ler[0].ToString()),
                        Numero = Ler[1].ToString(),
                        Fornecedor = new cl_021_Parceiro()
                        { Nome = Ler[2].ToString() },
                        Emissao = DateTime.Parse(Ler[3].ToString()),
                        Previsao = DateTime.Parse(Ler[4].ToString()),
                        Qtd_Produtos = int.Parse(Ler[5].ToString()),
                        Qtd_Lotes = int.Parse(Ler[6].ToString()),
                        Total = int.Parse(Ler[7].ToString())
                    });

            }
            Ler.Close();
            return Lista;
        }

        public List<cl_02_Nota_Fiscal> PendetesDeConferir()
        {
    

            comando.CommandText =
                "SELECT N.ID_NF, N.Numero, PS.NOME,N.Emissao, N.Previsao, " +
                "COUNT(DISTINCT l.ID_Produto) as Produtos, " +
                "COUNT(DISTINCT l.Lote) as Lotes, " +
                "SUM(M.Quantidade) as Itens " +
                "FROM _03_NF AS N " +
                "LEFT JOIN _01_Parceiros AS PS ON N.ID_Fornecedor = PS.ID " +
                "LEFT JOIN _04_Lotes AS L ON N.ID_NF = L.ID_NF " +
                "LEFT JOIN _05_Acao AS A ON L.ID_Lote = A.ID_Lote " +
                "LEFT JOIN _06_Movimentacoes AS M ON A.ID_Acao = M.ID_Acao " +
                "LEFT JOIN _07_Posicoes P ON M.ID_Posicao = P.ID_Posicao " +
                "WHERE P.ID_Posicao = 2 " +
                "group by N.Numero, PS.NOME, N.Emissao, N.Previsao, N.ID_NF " +
                "HAVING " +
                "SUM(M.Quantidade) > 0 ";
            Ler = comando.ExecuteReader();
            comando.Parameters.Clear();
            var Lista = new List<cl_02_Nota_Fiscal>();


            while (Ler.Read())
            {
                Lista.Add(
                    new cl_02_Nota_Fiscal()
                    {
                        ID = int.Parse(Ler[0].ToString()),
                        Numero = Ler[1].ToString(),
                        Fornecedor = new cl_021_Parceiro()
                        { Nome = Ler[2].ToString() },
                        Emissao = DateTime.Parse(Ler[3].ToString()),
                        Previsao = DateTime.Parse(Ler[4].ToString()),
                        Qtd_Produtos = int.Parse(Ler[5].ToString()),
                        Qtd_Lotes = int.Parse(Ler[6].ToString()),
                        Total = int.Parse(Ler[7].ToString())
                    });

            }
            Ler.Close();
            return Lista;
        }

        public cl_02_Nota_Fiscal PesquisarDetalhesDaNF(int _ID)
        {
            //definir parametros
            comando.Parameters.AddWithValue("@ID_NF", _ID);

            //definr comando
            comando.CommandText =
                "SELECT " +
                "N.ID_NF, "+
                "N.Chave, " +
                "N.Numero, " +
                "N.Serie, " +
                "N.Pedido, " +
                "N.Emissao, " +
                "N.Previsao, " +
                "N.Natureza, " +
                "D.Nome, " +
                "F.Nome " +
                "FROM " +
                "_03_NF AS N " +
                "LEFT JOIN _01_Parceiros AS D ON N.ID_Fornecedor = D.ID " +
                "LEFT JOIN _01_Parceiros AS F ON N.ID_Fornecedor = F.ID  " +
                "WHERE ID_NF = @ID_NF";

            //Executar e receber comando comando
            Ler = comando.ExecuteReader();
            //Limpo os paramentros
            comando.Parameters.Clear();
            //Defino uma variável para armazenas os dados recebidos do SQl
            cl_02_Nota_Fiscal Lista = new cl_02_Nota_Fiscal();
            Lista.Fornecedor = new cl_021_Parceiro();
            Lista.Destinatario = new cl_021_Parceiro();
            //Armazenar resultado na lista de classe cl_Nota_Fiscal

            if (!Ler.HasRows)
            {
                Ler.Close();
                return Lista;
            }
            Ler.Read();

            Lista.ID = int.Parse(Ler[0].ToString());
            Lista.Chave = Ler[1].ToString();
            Lista.Numero = Ler[2].ToString();
            Lista.Serie = Ler[3].ToString();
            Lista.Pedido = Ler[4].ToString();
            Lista.Emissao = DateTime.Parse(Ler[5].ToString());
            Lista.Previsao = DateTime.Parse(Ler[6].ToString());
            Lista.Natureza = Ler[7].ToString();
            Lista.Destinatario.Nome = Ler[8].ToString();
            Lista.Fornecedor.Nome = Ler[9].ToString();

               
           
            
            Ler.Close();
            return Lista;
            
        }

        public int PendetesDeArmazenar(int _ID_LOTE)
        {
            int ID_NF = -1;
            //definir parâmetros
            comando.Parameters.AddWithValue("@ID_LOTE", _ID_LOTE);
            // Definir comando
            comando.CommandText =
                " SELECT n.ID_NF, " +
                " SUM(M.Quantidade) " +
                " FROM _03_NF AS N " +
                " LEFT JOIN  _04_Lotes AS L ON N.ID_NF = L.ID_NF " +
                " LEFT JOIN _05_Acao AS A ON L.ID_Lote = A.ID_Lote " +
                " LEFT JOIN _06_Movimentacoes AS M ON A.ID_Acao = M.ID_Acao " +
                " LEFT JOIN _07_Posicoes AS P ON M.ID_Posicao = P.ID_Posicao " +
                " WHERE " +
                " M.ID_Posicao = 2 " +
                " and " +
                " n.ID_NF = " +
                " (SELECT DISTINCT N2.ID_NF FROM _03_NF AS N2 " +
                " LEFT JOIN _04_Lotes AS L2 ON N2.ID_NF = L2.ID_NF " +
                " WHERE " +
                " L2.ID_Lote = @ID_LOTE " +
                " ) " +
                " group by N.ID_NF " +
                " HAVING " +
                " SUM(M.Quantidade) > 0 ";
            Ler = comando.ExecuteReader();
            comando.Parameters.Clear();
            if (!Ler.HasRows)
            {
                Ler.Close();
                return ID_NF;
            }
            Ler.Read();
            ID_NF = int.Parse(Ler[0].ToString());
            return ID_NF;
        }

        public string GerarPedido()
        {

            // Definir comando de pesquisa para obter o maior ID ate o momento
            comando.CommandText = "select coalesce(max(id),0) from  [ESousa_Ericsson].[_002_Pedido_Entrada]";

            //Executar comando 
            Ler = comando.ExecuteReader();
            int ID = 0;
            var Romaneio = "";
            //Definir ID
            Ler.Read();
            ID = int.Parse(Ler[0].ToString()) + 1;
            Romaneio = "PE" + String.Format("{0:000000}", ID);
            Ler.Close();

            comando.Parameters.AddWithValue("@ID", ID);
            comando.CommandText = "UPDATE [ESousa_Ericsson].[_002_Pedido_Entrada] " +
            " SET ID = @ID";
            comando.ExecuteNonQuery();

            comando.Parameters.Clear();
            comando.Dispose();

            return Romaneio;
        }

    }

   
}