using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WMS3._1.Models;
using WMS3._1.Models.Serializar.NFeProca_pasta.NFe_pasta.Detalhes_pasta;

namespace WMS3._1.Apps
{
	public class app_Lote: IDisposable
	{
		// variaveis de banco de dados
        SqlCommand comando;
        SqlDataReader Ler;

		//construtor para conexão com banco de dados
		public app_Lote()
        {
            comando = new app_SqlConnection().Conecar();
        }
       

        public int Inserir(cl_03_Lote _Lote)
        {
       

            //Definir comando de pesquisa(Selecionar o ID Máximo)
            comando.CommandText = "SELECT MAX(ID_Lote) FROM [_04_Lotes] ";

            //Executar comando
            Ler = comando.ExecuteReader();

            //Se não exite dados no banco, o ID  = 1
            try
            {
                //Definir ID
                while (Ler.Read()) { _Lote.ID = int.Parse(Ler[0].ToString()) + 1; }
            }
            catch
            {
                _Lote.ID = 1;
            }
            Ler.Close();
            #region Converter Parcamentros para o formato SQL
            comando.Parameters.AddWithValue("@ID_LOTE", _Lote.ID);
            comando.Parameters.AddWithValue("@Lote", _Lote.Nome);
            comando.Parameters.AddWithValue("@ID_Produto", _Lote.Produto.ID);
            comando.Parameters.AddWithValue("@ID_NF", _Lote.NF.ID);
            comando.Parameters.AddWithValue("@Valor", _Lote.Valor);
            comando.Parameters.AddWithValue("@Fabricacao", _Lote.Fabricacao);
            comando.Parameters.AddWithValue("@Validade", _Lote.Validade);
            comando.Parameters.AddWithValue("@Status", "Pendente");
            #endregion
            #region Definir comando de inserir
            comando.CommandText = "INSERT INTO [_04_Lotes] " +
                "VALUES( " +
                "@ID_LOTE, " +
                "@Lote, " +
                "@ID_Produto, " +
                "@ID_NF, " +
                "@Valor, " +
                "@Fabricacao, " +
                "@Validade, " +
                "@Status )";
            #endregion

            //Realizar comando
            comando.ExecuteNonQuery();

            //Limpar os parametros
            comando.Parameters.Clear();

            return _Lote.ID;
        }

        public List<int> PesquisarPor(string Chave)
        {
            //converter parametros
            comando.Parameters.AddWithValue("@Chave", Chave);

            //Definir comando de pesquisa
            comando.CommandText = "SELECT L.ID_Lote FROM " +
                "[_04_Lotes] AS L " +
                "LEFT JOIN      " +
                "[_03_NF] AS N " +
                "ON " +
                "L.ID_NF = N.ID_NF " +
                "WHERE " +
                "N.Chave = @Chave";
            //Executar comando
            Ler = comando.ExecuteReader();
            //Limpar paramentros
            comando.Parameters.Clear();
            var Lista = new List<int>();
            while(Ler.Read())
            {
                Lista.Add(int.Parse(Ler[0].ToString()));
            }
            return Lista;
        }

        public List<cl_03_Lote> PesquisarPor(int _ID_NF)
        {
            //converter parametros
            comando.Parameters.AddWithValue("@ID_NF", _ID_NF);

            //Definir comando de pesquisa
            comando.CommandText =
                "SELECT " +
                "P.CodigoF, " +
                "P.NomeF, " +
                "L.Lote, " +
                "P.UM, " +
                "SUM(M.Quantidade) AS QTD, " +
                "TRIM(L.Status), " +
                "L.ID_Lote " +
                "FROM _04_Lotes              AS L " +
                "LEFT JOIN _03_NF AS N ON L.ID_NF = N.ID_NF " +
                "LEFT JOIN _02_Produtos AS P ON L.ID_Produto = P.ID " +
                "LEFT JOIN _05_Acao AS A ON L.ID_Lote = A.ID_Lote " +
                "LEFT JOIN _06_Movimentacoes AS M ON A.ID_Acao = M.ID_Acao " +
                "WHERE N.ID_NF = @ID_NF " +
                "GROUP BY " +
                "P.CodigoF, " +
                "P.NomeF, " +
                "L.Lote, " +
                "P.UM, " +
                "L.Status, " +
                "L.ID_Lote ";
            //Executar comando
            Ler = comando.ExecuteReader();
            //Limpar paramentros
            comando.Parameters.Clear();
            var Lista = new List<cl_03_Lote>();
            var Lote = new cl_03_Lote();
            Lote.Produto = new cl_Produto();
            while (Ler.Read())
            {

                Lista.Add(
                    new cl_03_Lote()
                    {
                        Produto = new cl_Produto()
                        {
                            CodigoF = Ler[0].ToString(),
                            NomeF = Ler[1].ToString(),
                            Unidade_Medida = Ler[3].ToString()
                        },
                        Nome = Ler[2].ToString(),
                        Quantidade = Double.Parse(Ler[04].ToString()),
                        Status = Ler[05].ToString(),
                        ID = int.Parse(Ler[06].ToString())

                    });
            }
            return Lista;
        }

        public List<cl_03_Lote> PesquisarPor(
            string Status ="%", int ID_Posicao = 0 ,int _ID_NF = 0,
            int ID_Romaneio = 0, string Produto = "%",int ID_Lote = 0 )
        {


            //Verificar se os caracter já foram preenchidos 
            //se não considerar qualquer caracter
            var S_Status = " = ";
            if(Status == "%")
                S_Status = " LIKE ";

            var S_ID_Posicao = " = ";
            if (ID_Posicao == 0)
                S_ID_Posicao = " >= ";

            var S_ID_NF = " = ";
            if (_ID_NF == 0)
                S_ID_NF = " >= ";

            var S_ID_Romaneio = " = ";
            if (ID_Romaneio == 0)
                S_ID_Romaneio = " >= ";

            var S_ID_Lote = " = ";
            if (ID_Lote == 0)
                S_ID_Lote = " >= ";


            //converter parametros
            comando.Parameters.AddWithValue("@Status", Status);
            comando.Parameters.AddWithValue("@ID_Posicao", ID_Posicao);
            comando.Parameters.AddWithValue("@ID_NF", _ID_NF);
            comando.Parameters.AddWithValue("@Romaneio", ID_Romaneio);
            comando.Parameters.AddWithValue("@Produto", Produto);
            comando.Parameters.AddWithValue("@ID_Lote", ID_Lote);

            //Definir comando de pesquisa
            comando.CommandText =
                "SELECT " +
                "P.ID, " +
                "P.CodigoF, " +
                "P.NomeF, " +
                "P.CodigoI, " +
                "P.NomeI, " +
                "P.UM, " +
                "TRIM(L.Lote), " +
                "SUM(M.Quantidade) AS QTD, " +
                "TRIM(L.Status), " +
                "L.ID_Lote, " +
                "L.Validade " +
                "FROM _04_Lotes              AS L " +
                "LEFT JOIN _03_NF            AS N ON L.ID_NF      = N.ID_NF " +
                "LEFT JOIN _02_Produtos      AS P ON L.ID_Produto = P.ID " +
                "LEFT JOIN _05_Acao          AS A ON L.ID_Lote    = A.ID_Lote " +
                "LEFT JOIN _06_Movimentacoes AS M ON A.ID_Acao    = M.ID_Acao " +
                "LEFT JOIN _08_Romaneio      AS R ON N.ID_Romaneio = R.ID_Romaneio " +
                "WHERE " +
                "L.ID_Lote " + S_ID_Lote + " @ID_Lote " +
                "AND " +
                "L.Status " + S_Status + " @Status " +
                "AND " +
                "M.ID_Posicao " + S_ID_Posicao + " @ID_Posicao " +
                 "AND " +
                "N.ID_NF " + S_ID_NF + " @ID_NF " +
                 "AND " +
                "R.ID_Romaneio " + S_ID_Romaneio + " @Romaneio " +
                 "AND " +
                "(P.NomeF LIKE '%'+@Produto+'%'  OR P.NomeI LIKE '%'+@Produto+'%') " +
                "GROUP BY " +
                "P.ID, " +
                "P.CodigoF, " +
                "P.NomeF, " +
                 "P.CodigoI, " +
                "P.NomeI, " +
                "P.UM, " +
                 "TRIM(L.Lote), " +
                "L.Status, " +
                "L.ID_Lote, " +
                "L.Validade " +
                "HAVING " +
                "SUM(M.Quantidade) > 0 ";
            //Executar comando
            Ler = comando.ExecuteReader();
            //Limpar paramentros
            comando.Parameters.Clear();
            var Lista = new List<cl_03_Lote>();
            //var Lote = new cl_03_Lote();
            //Lote.Produto = new cl_Produto();
            while (Ler.Read())
            {

                Lista.Add(
                    new cl_03_Lote()
                    {
                        Produto = new cl_Produto()
                        {
                            ID = int.Parse(Ler[0].ToString()),
                            CodigoF = Ler[1].ToString(),
                            NomeF = Ler[2].ToString(),
                            CodigoI = Ler[3].ToString(),
                            NomeI = Ler[4].ToString(),
                            Unidade_Medida = Ler[5].ToString()
                        },
                        Nome = Ler[6].ToString(),
                        Quantidade = Double.Parse(Ler[07].ToString()),
                        Status = Ler[08].ToString(),
                        ID = int.Parse(Ler[09].ToString()),
                        Validade = DateTime.Parse(Ler[10].ToString())

                    });
            }
            Ler.Close();
            return Lista;
        }

        public List<cl_0321_BI_Lote> Lista_BI(int _ID_Produto)
        {
            //converter parametro
            comando.Parameters.AddWithValue("@ID_Produto", _ID_Produto);
            comando.CommandText =
            "SELECT " +
            " l.ID_Lote " +
            " ,L.lote " +
            " ,COALESCE(SUM(M.Quantidade), 0) AS Quantidade " +
            ", MAX(CASE  WHEN A.Acao = 'Registrar' THEN A.Inicio END) AS Compras " +
            " , MAX(CASE  WHEN A.Acao = 'Entrada' THEN A.Inicio END) as Entrada " +
            ", sum(CASE  WHEN po.ID_Posicao = '2'  THEN m.Quantidade else 0 END) AS QTD " +
            " , MAX(CASE  WHEN A.Acao = 'Conferir' and A.OBS = '' THEN A.Inicio END) AS Confererência " +
            " , MAX(CASE  WHEN A.Acao = 'Armazenar' THEN A.Inicio END) AS Armazem " +
            " , sum(CASE  WHEN po.ID_Posicao > 10000  THEN m.Quantidade else 0 END) AS QTD " +
            " , MAX(CASE  WHEN A.Acao = 'Inventário'  THEN A.Inicio END) AS Inventário " +
            ", MAX(Validade) " +
            ",COUNT (distinct CASE  WHEN  po.ID_Posicao > 10000 THEN  po.Posicao  END) AS posiçoes " +
            " FROM _04_Lotes AS L " +
            " LEFT JOIN _02_Produtos AS Pr ON L.ID_Produto = Pr.ID " +
            " LEFT JOIN _05_Acao AS A ON L.ID_Lote = A.ID_Lote " +
            " LEFT JOIN _06_Movimentacoes AS M ON A.ID_Acao = M.ID_Acao " +
            " LEFT JOIN _07_Posicoes AS Po ON M.ID_Posicao = PO.ID_Posicao " +
            " WHERE pr.ID = @ID_Produto " +
            "GROUP BY " +
            "l.ID_Lote, " +
            "L.lote";

            Ler = comando.ExecuteReader();
            var Lista = new List<cl_0321_BI_Lote>();
            while (Ler.Read())
            {
                Lista.Add(new cl_0321_BI_Lote()
                {
                    ID = int.Parse(Ler[0].ToString()),
                    Lote = Ler[1].ToString(),
                    Quantidade = Double.Parse(Ler[2].ToString()),
                    Compras = avaliardata(Ler[3].ToString()),
                    Entrada = avaliardata(Ler[4].ToString()),
                    QTD_Entrada = int.Parse(Ler[5].ToString()),
                    Conferencia = avaliardata(Ler[6].ToString()),
                    Armazenar = avaliardata(Ler[7].ToString()),
                    QTD_Armazenar = int.Parse(Ler[8].ToString()),
                    Inventario = avaliardata(Ler[9].ToString()),
                    Validade = avaliardata(Ler[10].ToString()),
                    Posicoes = int.Parse(Ler[11].ToString())
                }
                ) ;
            }
            return Lista;

        }

        public List<cl_03_Lote> Lista_BI_Posicao(int _ID_Produto)
        {
            //converter parametro
            comando.Parameters.AddWithValue("@ID_Produto", _ID_Produto);
            comando.CommandText =
            "  select " +
            "  l.ID_Lote " +
            " ,l.Lote " +
            " ,Po.ID_Posicao " +
            " ,trim(po.Posicao) " +
            " ,L.Validade " +
            " ,coalesce(trim(pv.PreVenda), 'Disponivel') as Prevenda " +
            " ,coalesce(trim(pv.Status),'Vazio') as status " +
            " , sum(m.Quantidade) TOTAL " +
            " from " +
            " _06_Movimentacoes AS M " +
            " left join _07_Posicoes AS Po ON M.ID_Posicao = Po.ID_Posicao " +
            " left join _05_Acao AS A ON M.ID_Acao = a.ID_Acao " +
            " left join _04_Lotes AS L ON A.ID_Lote = L.ID_Lote " +
            " left join _02_Produtos AS Pr ON L.ID_Produto = Pr.ID " +
            " left join _10_Prevenda AS PV on pv.ID = m.ID_Prevenda " +
            " WHERE " +
            " pr.ID = @ID_Produto " +
            " group by " +
            " l.ID_Lote " +
            " ,l.Lote " +
            " ,Po.ID_Posicao " +
            " ,po.Posicao " +
            " ,L.Validade " +
            " ,pv.PreVenda " +
            " ,pv.Status " +
            " HAVING " +
            " sum(m.Quantidade) > 0 " +
            " order by pv.PreVenda , Validade,Lote,Posicao ";

            Ler = comando.ExecuteReader();
            var Lista = new List<cl_03_Lote>();
            while (Ler.Read())
            {
                Lista.Add(new cl_03_Lote()
                {
                    ID = int.Parse(Ler[0].ToString()),
                    Nome = Ler[1].ToString(),
                    Posicao = new cl_051_Posicao()
                    {
                        ID = int.Parse(Ler[2].ToString()),
                        Nome = Ler[3].ToString(),
                    },
                    Validade = DateTime.Parse(Ler[4].ToString()),
                    PreVenda = Ler[5].ToString(),
                    Status = Ler[6].ToString(),
                    Quantidade = Double.Parse(Ler[7].ToString())
                }
                );
            }
            return Lista;
           
        }

        public List<cl_03_Lote> Lista_PreVendas(string _PreVenda)
        {
            //definir parametros
            comando.Parameters.AddWithValue("@PreVenda", _PreVenda);
            //Excrupt do comando
            comando.CommandText =
            " SELECT " +
            " PR.ID " +
            " ,PR.CodigoI " +
            " ,pr.NomeI " +
            " ,LT.ID_Lote " +
            " ,LT.Lote " +
            " ,LT.Validade " +
            " ,ps.ID_Posicao " +
            " ,PS.Posicao " +
            " ,SUM(MV.Quantidade) " +
            " FROM " +
            " _10_Prevenda AS PV " +
            " LEFT JOIN _06_Movimentacoes AS MV ON PV.ID = MV.ID_Prevenda " +
            " LEFT JOIN _07_Posicoes AS PS ON MV.ID_Posicao = PS.ID_Posicao " +
            " LEFT JOIN _05_Acao AS AC ON MV.ID_Acao = AC.ID_Acao " +
            " LEFT JOIN _04_Lotes AS LT ON AC.ID_Lote = LT.ID_Lote " +
            " LEFT JOIN _02_Produtos AS PR ON LT.ID_Produto = PR.ID " +
            " WHERE PV.PreVenda = @PreVenda " +
            " GROUP BY " +
            " PR.ID " +
            " ,PR.CodigoI " +
            " ,pr.NomeI " +
            " ,LT.ID_Lote " +
            " ,LT.Lote " +
            " ,LT.Validade " +
            " ,ps.ID_Posicao " +
            " ,PS.Posicao " ;

            // executar o comando, obtendo os dados
            Ler = comando.ExecuteReader();
            var Lista = new List<cl_03_Lote>();
            while (Ler.Read())
            {
                Lista.Add(new cl_03_Lote()
                {
                    Produto = new cl_Produto()
                    {
                        ID = int.Parse(Ler[0].ToString()),
                        CodigoI = Ler[1].ToString(),
                        NomeI = Ler[2].ToString()
                    },
                    ID = int.Parse(Ler[3].ToString()),
                    Nome = Ler[4].ToString(),
                    Validade = DateTime.Parse(Ler[5].ToString()),
                    Posicao = new cl_051_Posicao()
                    {
                        ID = int.Parse(Ler[6].ToString()),
                        Nome = Ler[7].ToString()
                    },
                    Quantidade = Double.Parse(Ler[8].ToString())
                });
            }

            return Lista;
        }

        public List<cl_03_Lote> Lista_disponivel_Posicao(int _ID_Produto)
        {
            //converter parametro
            comando.Parameters.AddWithValue("@ID_Produto", _ID_Produto);
            comando.CommandText =
             " select " +
             "  l.ID_Lote " +
             " ,l.Lote " +
             " ,Po.ID_Posicao " +
             " ,po.Posicao " +
             " ,L.Validade " +
             " ,coalesce(sum(case when m.ID_Prevenda > 0 then m.Quantidade end), 0) /1 vendido " +
             " , sum(m.Quantidade) /1 as Quantidade " +
             " ,(sum(m.Quantidade) - " +
             " (coalesce(sum(case when m.ID_Prevenda > 0 then m.Quantidade end), 0))) as disponivel " +
             " from " +
             " _06_Movimentacoes AS M " +
             " left join _07_Posicoes AS Po ON M.ID_Posicao = Po.ID_Posicao " +
             " left join _05_Acao AS A ON M.ID_Acao = a.ID_Acao " +
             " left join _04_Lotes AS L ON A.ID_Lote = L.ID_Lote " +
             " left join _02_Produtos AS Pr ON L.ID_Produto = Pr.ID " +
             " WHERE " +
             " pr.ID = @ID_Produto " +
             " group by " +
             " l.ID_Lote " +
             " ,l.Lote " +
             " ,Po.ID_Posicao " +
             " ,po.Posicao " +
             " ,L.Validade " +
             " HAVING " +
             "(sum(m.Quantidade) - " +
             " (coalesce(sum(case when m.ID_Prevenda >0 then m.Quantidade end),0))/1)  > 0 " +
             " order by Validade,Lote,Posicao";

            Ler = comando.ExecuteReader();
            var Lista = new List<cl_03_Lote>();
            while (Ler.Read())
            {
                Lista.Add(new cl_03_Lote()
                {
                    ID = int.Parse(Ler[0].ToString()),
                    Nome = Ler[1].ToString(),
                    Posicao = new cl_051_Posicao()
                    {
                        ID = int.Parse(Ler[2].ToString()),
                        Nome = Ler[3].ToString(),
                    },
                    Validade = DateTime.Parse(Ler[4].ToString()),
                    Vendido = Double.Parse(Ler[5].ToString()),
                    Quantidade = Double.Parse(Ler[6].ToString()),
                    Disponível = Double.Parse(Ler[7].ToString())

                }
                );
            }
            return Lista;

        }

        private DateTime? avaliardata(string _Data)
        {
            DateTime Data;
            try
            {
                Data = DateTime.Parse(_Data);
                return Data;
            }
            catch
            {
                return null;
            }
        }

        public float SomaPorPosicao_Lote(int ID_Lote , int ID_Posicao)
        {
            //Definir parametros
            comando.Parameters.AddWithValue("@ID_LOTE", ID_Lote);
            comando.Parameters.AddWithValue("@ID_Posicao", ID_Posicao);

            //Definir comando de pesquisa
            comando.CommandText = "SELECT SUM([Quantidade]) " +
                "FROM _06_Movimentacoes AS M " +
                "LEFT JOIN _05_Acao AS A " +
                "ON A.ID_Acao = M.ID_Acao " +
                "WHERE A.ID_Lote = @ID_LOTE " +
                "AND  M.ID_Posicao = @ID_Posicao ";

            Ler = comando.ExecuteReader();
            float Total = 0;

            try
            {
                while (Ler.Read()) { Total = float.Parse(Ler[0].ToString()); }
            }
            catch
            {
                Total = 0;
            }

            return Total;
        } 

        public void AtulizarStatus(int ID_Lote)
        {
            //Definir parametros
            comando.Parameters.AddWithValue("@ID_Lote", ID_Lote);
            //Definir comando 
            comando.CommandText =
                "UPDATE [_04_Lotes] " +
                "SET " +
                "Status = 'OK' " +
                "WHERE " +
                "ID_Lote = @ID_Lote";
            //Realizar comando
            comando.ExecuteNonQuery();
            //Limpar os parametros
            comando.Parameters.Clear();
            comando.Dispose();
        }

        public void Dispose()
        {
           
            comando.Dispose();
            Ler.Close();
        }
    }
}