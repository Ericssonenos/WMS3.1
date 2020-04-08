using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WMS3._1.Models;

namespace WMS3._1.Apps
{
    public class app_Prevenda
    {
        // variaveis de banco de dados
        SqlCommand comando;
        SqlDataReader Ler;

        //construtor para conexão com banco de dados
        public app_Prevenda()
        {
            comando = new app_SqlConnection().Conecar();
        }

        public int Inserir(int _ID_Cliente)
        {

            var _PreVenda = new cl_07_PreVenda();

            //Definir comando de pesquisa(selecionar o ID Máximo)
            comando.CommandText = "SELECT MAX(ID) FROM _10_Prevenda ";

            //Executar comanod
            Ler = comando.ExecuteReader();

            //Verificar se existe aomenos um registro cadastrado
            try
            {
                Ler.Read();
                _PreVenda.ID = int.Parse(Ler[0].ToString()) +1;

            }
            catch
            {
                _PreVenda.ID = 1;
            }
            Ler.Close();

            // deinfir nome da Pré venda
            _PreVenda.Nome = "PV" + string.Format("{0:00000}", _PreVenda.ID);

            //Definir o ID do 
            _PreVenda.Parceiro = new cl_021_Parceiro();
            _PreVenda.Parceiro.ID = _ID_Cliente;
            _PreVenda.Status = "Pendente";

            // converter paramentros
            comando.Parameters.AddWithValue("@ID", _PreVenda.ID);
            comando.Parameters.AddWithValue("@Nome", _PreVenda.Nome);
            comando.Parameters.AddWithValue("@ID_Parceiro", _PreVenda.Parceiro.ID);
            comando.Parameters.AddWithValue("@Status", _PreVenda.Status);

            //Definir comando 
            comando.CommandText = "INSERT INTO [_10_Prevenda] " +
                "VALUES( " +
                "@ID, " +
                "@Nome, " +
                "@ID_Parceiro, " +
                "@Status )";

            //Realizar comando
            comando.ExecuteNonQuery();

            //Limpar os parametros
            comando.Parameters.Clear();

            return _PreVenda.ID;
        }

        public List<cl_07_PreVenda> ListaPendentes()
        {
            comando.CommandText =
                " SELECT  " +
                " pv.ID " +
                ",trim(pv.PreVenda) " +
                " ,trim(pv.Status) " +
                ",pr.ID " + 
                ",pr.Nome " +
                " , count(distinct Lo.Lote)  itens " +
                " , coalesce(sum(mo.Quantidade), 0) Volume " +
                "  FROM[_10_Prevenda] as PV " +
                " left join _01_Parceiros as pr on PV.ID_Cliente = pr.ID " +
                " left join _06_Movimentacoes as Mo on pv.ID = mo.ID_Prevenda " +
                " left join _05_Acao as Ac on mo.ID_Acao = ac.ID_Acao " +
                " left join _04_Lotes as Lo on ac.ID_Lote = Lo.ID_Lote " +
                " where pv.Status = 'Pendente' " +
                " group by " +
                " pv.ID " +
                ",pv.PreVenda " +
                ",pv.Status " +
                ",pr.ID " +
                " ,pr.Nome ";

            Ler = comando.ExecuteReader();

            var lista = new List<cl_07_PreVenda>();
            while (Ler.Read())
            {
                lista.Add(
                    new cl_07_PreVenda
                    {
                        ID = int.Parse(Ler[0].ToString()),
                        Nome = Ler[1].ToString(),
                        Status = Ler[2].ToString(),
                        Parceiro = new cl_021_Parceiro()
                        {
                            ID = int.Parse(Ler[3].ToString()),
                            Nome = Ler[4].ToString(),
                        },
                        Itens = int.Parse(Ler[5].ToString()),
                        Volume = Double.Parse(Ler[6].ToString())
                    }
                    );
            }


            return lista;
        }


        public List<cl_07_PreVenda> ListaEmprocesso()
        {
            comando.CommandText =
                " SELECT  " +
                " pv.ID " +
                ",trim(pv.PreVenda) " +
                " ,trim(pv.Status) " +
                ",pr.ID " +
                ",pr.Nome " +
                " , count(distinct Lo.Lote)  itens " +
                " , coalesce(sum(mo.Quantidade), 0) Volume " +
                "  FROM[_10_Prevenda] as PV " +
                " left join _01_Parceiros as pr on PV.ID_Cliente = pr.ID " +
                " left join _06_Movimentacoes as Mo on pv.ID = mo.ID_Prevenda " +
                " left join _05_Acao as Ac on mo.ID_Acao = ac.ID_Acao " +
                " left join _04_Lotes as Lo on ac.ID_Lote = Lo.ID_Lote " +
                " where pv.Status <> 'Faturado' " +
                " group by " +
                " pv.ID " +
                ",pv.PreVenda " +
                ",pv.Status " +
                ",pr.ID " +
                " ,pr.Nome ";

            Ler = comando.ExecuteReader();

            var lista = new List<cl_07_PreVenda>();
            while (Ler.Read())
            {
                lista.Add(
                    new cl_07_PreVenda
                    {
                        ID = int.Parse(Ler[0].ToString()),
                        Nome = Ler[1].ToString(),
                        Status = Ler[2].ToString(),
                        Parceiro = new cl_021_Parceiro()
                        {
                            ID = int.Parse(Ler[3].ToString()),
                            Nome = Ler[4].ToString(),
                        },
                        Itens = int.Parse(Ler[5].ToString()),
                        Volume = Double.Parse(Ler[6].ToString())
                    }
                    );
            }


            return lista;
        }


    }
}