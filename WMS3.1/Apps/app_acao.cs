using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WMS3._1.Models;
using WMS3._1.Models.Serializar.NFeProca_pasta.NFe_pasta;
using WMS3._1.Models.Serializar.NFeProca_pasta.NFe_pasta.Detalhes_pasta.Rastro_pasta;
using static WMS3._1.Models.Serializar.NFeProca_pasta.cl_NFe;

namespace WMS3._1.Apps
{
    public class app_acao:IDisposable
    {
        SqlCommand comando;
        SqlDataReader Ler;

        public app_acao()
        {
            comando = new app_SqlConnection().Conecar();
        }

        public int Inserir(cl_04_Acao _Acao)
        {
           
            if(_Acao.Nome != "Registrar"|| _Acao.Nome != "Alterar" || _Acao.Nome != "Pre-Nota")
            {
                AtulizarFim(_Acao.Lote.ID);
            }

            // Definir comando de pesquisa para obter o maior ID ate o momento
            comando.CommandText = "SELECT MAX(ID_Acao) FROM [_05_Acao]";
           
            //Executar comando 
            Ler = comando.ExecuteReader();

            //Se não exite dados no banco, o ID  = 1
            try
            {
                //Definir ID
                while (Ler.Read()) { _Acao.ID = int.Parse(Ler[0].ToString()) + 1; }
            }
            catch
            {
                _Acao.ID = 1;
            }

            // se não exiteri obs deixar = vazio
            if(_Acao.OBS == null)
            {
                _Acao.OBS = "";
            }

            Ler.Close();
            #region Converter Parcamentros para o formato SQL
            comando.Parameters.AddWithValue("@ID_Acao", _Acao.ID);
            comando.Parameters.AddWithValue("@ID_Lote", _Acao.Lote.ID);
            comando.Parameters.AddWithValue("@Acao", _Acao.Nome);
            comando.Parameters.AddWithValue("@Inicio", _Acao.Inicio);
            comando.Parameters.AddWithValue("@Fim",_Acao.Fim);
            comando.Parameters.AddWithValue("@Total", 0);
            comando.Parameters.AddWithValue("@Status", "Aberto");
            comando.Parameters.AddWithValue("@Usuário", _Acao.Usuario);
            comando.Parameters.AddWithValue("@OBS", _Acao.OBS);
            #endregion
            #region Definir comando de inserir
            comando.CommandText = "INSERT INTO [_05_Acao] " +
                "VALUES( " +
                "@ID_Acao, " +
                "@ID_Lote, " +
                "@Acao, " +
                "@Inicio, " +
                "@Fim, " +
                "@Total, " +
                "@Status, " +
                "@Usuário, " +
                "@OBS )";
            #endregion

            //Realizar Comando
            comando.ExecuteNonQuery();

            //Limpar os paramentros
            comando.Parameters.Clear();

            return _Acao.ID;
        }

        public void AtulizarFim (int id_lote)
        {
            // Definir parmaretros
            comando.Parameters.AddWithValue("@Lote", id_lote);

            //Definir comando 
            comando.CommandText = 
            "UPDATE _05_Acao " +
            "SET Fim = GETDATE(), " +
            "Status = 'Fechado', " +
            "Total = DATEDIFF(second, Inicio, GETDATE()) " +
            "WHERE ID_Acao = " +
            "(SELECT MAX(A.ID_Acao) " +
            "FROM _05_Acao AS A " +
            "WHERE A.ID_Lote = @Lote) ";

            //Executar comando
            comando.ExecuteNonQuery();
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