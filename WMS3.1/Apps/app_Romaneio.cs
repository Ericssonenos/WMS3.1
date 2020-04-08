using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WMS3._1.Models;

namespace WMS3._1.Apps
{
    public class app_Romaneio : IDisposable
    {
        // variaveis de banco de dados
        SqlCommand comando;
        SqlDataReader Ler;

        public app_Romaneio()
        {
            comando = new app_SqlConnection().Conecar();
        }

        public int ObterIDRomaneio(string Romaneio)
        {
            //Verificar se o romaneio já está cadastrado

            // Converter Parametros para o formato SQL
            comando.Parameters.AddWithValue("@Romaneio",Romaneio);
            //Definir comando de pesquisa
            comando.CommandText = "SELECT * FROM _08_Romaneio " +
                "WHERE @Romaneio = Romaneio";
            //Executar comando
            Ler = comando.ExecuteReader();

            // 
            int ID = -1;
            if (Ler.HasRows)
            {
                while (Ler.Read()) { ID = int.Parse(Ler[0].ToString()); }
                Ler.Close();
                return ID;
            }
            else
            {
                Ler.Close();
                return ID;
            }
        }

        private int IDMáximo()
        {

            // Definir comando de pesquisa para obter o maior ID ate o momento
            comando.CommandText = "SELECT MAX(ID_Romaneio) FROM [_08_Romaneio]";

            //Executar comando 
            Ler = comando.ExecuteReader();
            int ID = 0;
            //Se não exite dados no banco, o ID  = 0 (Primeriro cadastro no banco de dados)
            try
            {
                //Definir ID
                while (Ler.Read()) { ID =  int.Parse(Ler[0].ToString()); }
            }
            catch
            {
                ID =  0;
            }
            Ler.Close();

            return ID;
        }

        public string Gerar()
        {

            // Definir comando de pesquisa para obter o maior ID ate o momento
            comando.CommandText = "select coalesce(max(id),0) from  [ESousa_Ericsson].[_001_Romaneio]";

            //Executar comando 
            Ler = comando.ExecuteReader();
            int ID = 0;
            var Romaneio = "";
            //Definir ID
            Ler.Read();
            ID = int.Parse(Ler[0].ToString()) +1;
            Romaneio = "RO" + String.Format("{0:000000}", ID);
            Ler.Close();

            comando.Parameters.AddWithValue("@ID", ID);
            comando.CommandText = "UPDATE [ESousa_Ericsson].[_001_Romaneio] " +
            " SET ID = @ID";
            comando.ExecuteNonQuery();

            comando.Parameters.Clear();
            comando.Dispose();

            return Romaneio;
        }

        public int Inserir(cl_01_Romaneio romaneio)
        {


            // Obter o próximo ID de cadastro
            romaneio.ID = IDMáximo() + 1;

            #region Converter Parcamentros para o formato SQL
            comando.Parameters.AddWithValue("@ID_Romaneio", romaneio.ID);
            comando.Parameters.AddWithValue("@Romaneio", romaneio.Nome);
            comando.Parameters.AddWithValue("@Placa", romaneio.Placa);
            comando.Parameters.AddWithValue("@Motorista", romaneio.Motorista);
            comando.Parameters.AddWithValue("@Usuario", romaneio.Usuario);
            comando.Parameters.AddWithValue("@Data", DateTime.Now);
            #endregion

            #region Definir comando de inserir
            comando.CommandText = "INSERT INTO [_08_Romaneio]" +
                "VALUES( " +
                "@ID_Romaneio, " +
                "@Placa, " +
                "@Romaneio, " +
                "@Motorista, " +
                "@Usuario, " +
                "@Data )";
            #endregion
            //Realizar Comando
            comando.ExecuteNonQuery();
            //Limpar os paramentros
            comando.Parameters.Clear();
            comando.Dispose();


            return romaneio.ID;


        }

        public List<cl_01_Romaneio> PendeteDeConferir()
        {
            comando.CommandText =
                "SELECT  R.ID_Romaneio, R.Romaneio,R.Placa, r.Motorista, " +
                "COUNT(DISTINCT n.Numero) as NFs , " +
                "COUNT(DISTINCT l.ID_Produto) as Produtos,  " +
                "COUNT(DISTINCT l.Lote) as Lotes,  " +
                "SUM(M.Quantidade) as Itens " +
                "FROM _08_Romaneio as R " +
                "LEFT JOIN   _03_NF AS N ON R.ID_Romaneio = N.ID_Romaneio " +
                "LEFT JOIN _04_Lotes AS L ON N.ID_NF = L.ID_NF " +
                "LEFT JOIN _05_Acao AS A ON L.ID_Lote = A.ID_Lote " +
                "LEFT JOIN _06_Movimentacoes AS M ON A.ID_Acao = M.ID_Acao " +
                "LEFT JOIN _07_Posicoes P ON M.ID_Posicao = P.ID_Posicao " +
                "WHERE P.ID_Posicao = 2 " +
                "group by R.ID_Romaneio, R.Romaneio,R.Placa, r.Motorista " +
                "HAVING " +
                "SUM(M.Quantidade) > 0";

            Ler = comando.ExecuteReader();
            comando.Parameters.Clear();
            var Lista = new List<cl_01_Romaneio>();
            while (Ler.Read())
            {
                Lista.Add(
                    new cl_01_Romaneio()
                    {
                        ID = int.Parse(Ler[0].ToString()),
                        Nome = Ler[1].ToString(),
                        Placa = Ler[2].ToString(),
                        Motorista = Ler[3].ToString(),
                        Qtd_NF = int.Parse(Ler[4].ToString()),
                        Qtd_Produtos = int.Parse(Ler[5].ToString()),
                        Qtd_Lotes = int.Parse(Ler[6].ToString()),
                        Total = double.Parse(Ler[7].ToString())
                        
                    } );
            }
            Ler.Close();
            return Lista;

        }

        public cl_01_Romaneio PesquisarRomaneio(string _Romaneio_Nome)
        {
            var Resultado = new cl_01_Romaneio();
            //criar paracetros
            comando.Parameters.AddWithValue("@Romaneio", _Romaneio_Nome);
            //Criar comando
            comando.CommandText =
                "SELECT * FROM [_08_Romaneio] " +
                "WHERE " +
                "Romaneio = @Romaneio";
            //Executar comando
            Ler = comando.ExecuteReader();
            // Extrair informações do comando
            if (Ler.HasRows)
            {
                Ler.Read();
                
                Resultado.ID = int.Parse(Ler[0].ToString());
                Resultado.Placa = Ler[1].ToString();
                Resultado.Nome = Ler[2].ToString();
                Resultado.Motorista = Ler[3].ToString();
                Resultado.Usuario = Ler[4].ToString();
                Resultado.Data = DateTime.Parse(Ler[5].ToString());
            }
            else
            {
                Resultado.Nome = _Romaneio_Nome;
            }
      
            
            //fechar parametros e campo de leitura
            comando.Parameters.Clear();
            Ler.Close();
            // retornar valor
            return Resultado;
        }
        public void Dispose()
        {
            comando.Dispose();
            Ler.Close();
        }
    }
}