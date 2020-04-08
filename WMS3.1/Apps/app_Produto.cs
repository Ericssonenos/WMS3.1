using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using WMS3._1.Models;

using System.IO;
using static WMS3._1.Models.Serializar.NFeProca_pasta.cl_NFe;
using WMS3._1.Models.Serializar;
using WMS3._1.Models.Serializar.NFeProca_pasta.NFe_pasta.Detalhes_pasta;
using WMS3._1.Models.Serializar.NFeProca_pasta.NFe_pasta;
   
namespace WMS3._1.Apps
{
    public class app_Produto : IDisposable    
    {
        SqlCommand comando;
        SqlDataReader Ler;


        public app_Produto()
        {
            comando = new app_SqlConnection().Conecar();
        }

    

        public cl_XML Organizar(cl_XML _NFE)
        {
            //Primeiro, buscar por CodigoF
            foreach (cl_Detalhes detalhes in _NFE.NFe.InfNFe.Detalhes)
            {
                detalhes.Produto.CodigoI = PesquisarCodigoF(detalhes.Produto);
                detalhes.Produto.ID = PesquisarID(detalhes.Produto);
            }
            return _NFE;

        }

        public List<cl_Produto> Pesquisa(cl_Produto _produto)
        {
            var SinalID = "=";
            if (_produto.ID == 0)
                SinalID = " > ";

            //Verficar se os valores foram preenchidos, se não, considerar qualqer caracter
            var SinalCodigoI = " = ";
            if (String.IsNullOrEmpty(_produto.CodigoI))
            {
                _produto.CodigoI = "%";
                SinalCodigoI = " LIKE ";
            }
            else
            {
                _produto.CodigoI = string.Format("{0:D6 }", _produto.CodigoI);
            }
             
            var SinalCodigoF = " = ";
            if (String.IsNullOrEmpty(_produto.CodigoF))
            {
                _produto.CodigoF = "%";
                SinalCodigoF = " LIKE ";

            }

            var SinalNomeI = " = ";
            if (String.IsNullOrEmpty(_produto.NomeI))
            {
                _produto.NomeI = "%";
                SinalNomeI = " LIKE ";
            }
            var SinalNomeF = " = ";
            if (String.IsNullOrEmpty(_produto.NomeF))
            {
                _produto.NomeF = "% ";
                SinalNomeF = " LIKE ";
            }

            var SinalParceiro =  "=";
            if (String.IsNullOrEmpty(_produto.Parceiro))
            {
                _produto.Parceiro = "%";
                SinalParceiro = " LIKE ";
            }


            // Converter para formato SQL
            comando.Parameters.AddWithValue("@ID", _produto.ID);
            comando.Parameters.AddWithValue("@CodigoI",_produto.CodigoI);
            comando.Parameters.AddWithValue("@CodigoF", _produto.CodigoF);
            comando.Parameters.AddWithValue("@NomeI", _produto.NomeI);
            comando.Parameters.AddWithValue("@NomeF", _produto.NomeF);
            comando.Parameters.AddWithValue("@Parceiro", _produto.Parceiro);

            // Definir comando de pesquisa
            comando.CommandText = "SELECT * FROM [_02_Produtos] " +
            "WHERE " +
            "ID " + SinalID + " @ID " +
            "AND " +
            "CodigoI " + SinalCodigoI + " @CodigoI " + 
            "AND " +
            "CodigoF " + SinalCodigoF + " @CodigoF " +
            "AND " +
            "NomeI " + SinalNomeI + " @NomeI " +
            "AND " +
            "NomeF " + SinalNomeF + " @NomeF " +
            "AND " +
            "Parceiro " + SinalParceiro + " @Parceiro ";

            // Executar Comando
            Ler = comando.ExecuteReader();
            comando.Parameters.Clear();
            // Trasformar os dados para a classe Produtos
            var ListaProdutos = new List<cl_Produto>();
            while (Ler.Read()) 
            {
                ListaProdutos.Add(
                    new cl_Produto()
                    {
                        ID = int.Parse(Ler[0].ToString()),
                        CodigoI = Ler[1].ToString(),
                        CodigoF = Ler[2].ToString(),
                        NomeI = Ler[3].ToString(),
                        NomeF = Ler[4].ToString(),
                        Unidade_Medida = Ler[5].ToString(),
                        Parceiro = Ler[6].ToString(),
                        EstoqueMinimo = float.Parse(Ler[7].ToString()),
                        Altura = float.Parse(Ler[8].ToString()),
                        Largura = float.Parse(Ler[9].ToString()),
                        Comprimento = float.Parse(Ler[10].ToString())
                    });
            }
            Ler.Close();
            return ListaProdutos;

        }

        public string Atulisar_CodigoF(cl_Produto _Produto)
        {
            string GodigoI = "-1";

            //converter paramentros para o formato SQL
            comando.Parameters.AddWithValue("@ID", _Produto.ID);
            comando.Parameters.AddWithValue("@CodigoF", _Produto.CodigoF);
            comando.Parameters.AddWithValue("@NomeF", _Produto.NomeF);

            //Definir comando de pesquisa(Selecionar ID pelo CodigoI)
            comando.CommandText = "SELECT CodigoI FROM [_02_Produtos] " +
                "WHERE @ID = ID ";

            //Executar comando
            Ler = comando.ExecuteReader();

            //Definir o ID do produto
            while (Ler.Read()) { GodigoI = Ler[0].ToString(); }
            Ler.Close();
            //Definir comando de atualização(Update)
            comando.CommandText = "UPDATE [_02_Produtos] " +
                "SET CodigoF = @CodigoF, " +
                "NomeF = @NomeF " +
                 "WHERE @ID = ID ";
            //Executar comando
            comando.ExecuteNonQuery();
            comando.Parameters.Clear();

    

            return GodigoI;
        }

        public List<cl_Produto> Idsdisponiveis()
        {
            var lista = new List<cl_Produto>();
            // comando
            comando.CommandText =
                "SELECT ID,CodigoI FROM [_02_Produtos] WHERE CodigoF = '0000' order by ID  ";
            Ler = comando.ExecuteReader();
            while (Ler.Read())
            {
                lista.Add(
                    new cl_Produto
                    {
                        ID = int.Parse(Ler[0].ToString()),
                        CodigoI = Ler[1].ToString()
                        
                    });
            }
            return lista;

        }

        public List<cl_0311_BI_Produto> Lista_BI(string _Status = "%", int _ID_Produto =0)
        {

            var S_Status = "=";
            if(_Status == "%")
                S_Status = " LIKE ";

            var S_Produto = "=";
            if (_ID_Produto == 0)
                S_Produto = " > ";

            //converter parametros
            comando.Parameters.AddWithValue("@Status", _Status);
            comando.Parameters.AddWithValue("@ID", _ID_Produto);
            comando.CommandText=
                "SELECT " +
                "pr.ID, " +
                "Pr.NomeI " +
                ",pr.CodigoI" +
                ",COUNT(DISTINCT L.ID_Lote) AS Lotes, " +
                "COUNT(DISTINCT PO.ID_Posicao) AS Posições, " +
                "COALESCE(SUM(M.Quantidade), 0) AS Total, " +
                "COALESCE( SUM(CASE WHEN M.ID_Prevenda > 0 then m.Quantidade end), 0) as vendido, " +
                "COALESCE(SUM(CASE WHEN M.ID_Prevenda = 0 then m.quantidade end), 0) as Disponivel, " +
                "MIN(L.Validade) AS ValidadeMinima, " +
                "CASE " +
                "WHEN MIN(L.Validade) > GETDATE() then " +
                "'OK' " +
                "ELSE " +
                "'VENCIDO' " +
                "end as [Resuldado] " +
                "FROM _04_Lotes AS L " +
                "LEFT JOIN _02_Produtos AS Pr ON L.ID_Produto = Pr.ID " +
                "LEFT JOIN _05_Acao AS A ON L.ID_Lote = A.ID_Lote " +
                "LEFT JOIN _06_Movimentacoes AS M ON A.ID_Acao = M.ID_Acao " +
                "LEFT JOIN _07_Posicoes AS Po ON M.ID_Posicao = PO.ID_Posicao " +
                "WHERE " +
                "pr.ID " +
                S_Produto +
                "@ID " +
                "GROUP BY " +
                "pr.ID, " +
                "Pr.NomeI " +
                ",pr.CodigoI " +
                "having " +
                "(CASE " +
                "WHEN MIN(L.Validade) > GETDATE() then " +
                "'OK' " +
                "ELSE " +
                "'VENCIDO' " +
                "end " +
                S_Status +
                " @Status) " +
                "ORDER BY " +
                "Total desc, ID ";

            Ler = comando.ExecuteReader();
            var Lista = new List<cl_0311_BI_Produto>();
            while (Ler.Read())
            {
                Lista.Add(
                    new cl_0311_BI_Produto()
                    {
                        ID = int.Parse(Ler[0].ToString()),
                        Nome = Ler[1].ToString(),
                        C_Interno = Ler[2].ToString(),
                        Lotes = int.Parse(Ler[3].ToString()),
                        Posicoes = int.Parse(Ler[4].ToString()),
                        Quantidade = Double.Parse(Ler[5].ToString()),
                        Vendido = Double.Parse(Ler[6].ToString()),
                        Disponivel = Double.Parse(Ler[7].ToString()),
                        ValidadeMinima = DateTime.Parse(Ler[8].ToString()),
                        Status = Ler[9].ToString(),
                        
                    }
                    );
            }
            return Lista;


        }

        

        //privados
        private string PesquisarCodigoF(cl_Produto _Produto)
        {
            if (String.IsNullOrEmpty(_Produto.CodigoF))
            {
                return "";
            }
            else
            {
                var Produto = new cl_Produto();
                Produto.CodigoF = _Produto.CodigoF;
                Produto = Pesquisa(Produto).FirstOrDefault();
                if (Produto != null)
                    return Produto.CodigoI;

                return "";
            }

        }

        private int PesquisarID(cl_Produto _Produto)
        {
            if (String.IsNullOrEmpty(_Produto.CodigoF))
            {
                return 0;
            }
            else
            {
                var Produto = new cl_Produto();
                Produto.CodigoF = _Produto.CodigoF;
                Produto = Pesquisa(Produto).FirstOrDefault();
                if (Produto != null)
                    return Produto.ID;

                return 0;
            }

        }

         

        public void Dispose()
        {

            comando.Dispose();
            Ler.Close();
        }
    }
}