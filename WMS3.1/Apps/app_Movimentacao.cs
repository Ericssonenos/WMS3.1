using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WMS3._1.Models;

namespace WMS3._1.Apps
{
    public class app_Movimentacao : IDisposable
    {
        SqlConnection conexao;
        SqlCommand comando;
        SqlDataReader Ler;

        public app_Movimentacao()
        {
            comando = new app_SqlConnection().Conecar();
        }

        public void Inserir(cl_05_Movimentacao _Movimentacao)
        {
            int ID_Movimentacao = -1;

            //Definir comando de pesquisa para obter o maior ID ate o momento
            comando.CommandText = "SELECT MAX(ID_Movimentacao) FROM [_06_Movimentacoes]";

            //Executar Comando
            Ler = comando.ExecuteReader();

            //Se não exite dados no banco, o ID  = 1
            try
            {
                //Definir ID
                while (Ler.Read()) { ID_Movimentacao = int.Parse(Ler[0].ToString()) + 1; }
            }
            catch
            {
                ID_Movimentacao = 1;
            }
            Ler.Close();

            #region Converter Parcamentros para o formato SQL
            comando.Parameters.AddWithValue("@ID_Movimentacao", ID_Movimentacao);
            comando.Parameters.AddWithValue("@ID_Acao", _Movimentacao.Acao.ID);
            comando.Parameters.AddWithValue("@ID_Posicao", _Movimentacao.ID_Posicao);
            comando.Parameters.AddWithValue("@ID_PreVenda", _Movimentacao.ID_PreVenda);

            comando.Parameters.AddWithValue("@Quantidade", _Movimentacao.Quantidade);
            #endregion
            #region Definir comando de inserir
            comando.CommandText = "INSERT INTO [_06_Movimentacoes] " +
                "VALUES( " +
                "@ID_Movimentacao, " +
                "@ID_Acao, " +
                "@ID_Posicao, " +
                "@ID_PreVenda, " +
                "@Quantidade )";
            #endregion

            //Realizar Comando
            comando.ExecuteNonQuery();

            //Limpar os paramentros
            comando.Parameters.Clear();

            
        }

       
        public void Dispose()
        {
            conexao.Dispose();
            comando.Dispose();
            Ler.Close();
        }
    }
}