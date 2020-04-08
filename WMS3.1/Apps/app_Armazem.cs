using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WMS3._1.Models;

namespace WMS3._1.Apps
{
    public class app_Armazem 
    {
        // variaveis de banco de dados
      
        SqlCommand comando;
        SqlDataReader Ler;

        public app_Armazem()
        {
            comando = new app_SqlConnection().Conecar();
        }

        public void CriarPosicoes()
        {


            Inserir(1, "", 0, "", 1, "Compra");
            Inserir(2, "", 0, "", 1, "Doca");
            //Modulo A
            var ID = 10000;
            var Modulo = "A";
            var Ruas = 4;
            var Nivel = 4;
            var Números = 70;
            for (int Rua = 1; Rua <= Ruas; Rua++)
            {
                for(int N = 0; N < Nivel; N++)
                {
                    for(int Numero = 1; Numero <= Números; Numero+=2)
                    {
                        Inserir(ID++, Modulo, Rua, Niveis()[N], Numero);
                    }
                   
                }

                for (int N = 0; N < Nivel; N++)
                {
                    for (int Numero = 2; Numero <= Números; Numero += 2)
                    {
                        Inserir(ID++, Modulo, Rua, Niveis()[N], Numero);
                    }

                }

               
            }

            //Modulo B
             ID = 20000;
             Modulo = "B";
             Ruas = 1;
             Nivel = 4;
             Números = 70;
            for (int Rua = 1; Rua <= Ruas; Rua++)
            {
                for (int N = 0; N < Nivel; N++)
                {
                    for (int Numero = 1; Numero <= Números; Numero += 2)
                    {
                        Inserir(ID++, Modulo, Rua, Niveis()[N], Numero);
                    }

                }

                for (int N = 0; N < Nivel; N++)
                {
                    for (int Numero = 2; Numero <= Números; Numero += 2)
                    {
                        Inserir(ID++, Modulo, Rua, Niveis()[N], Numero);
                    }

                }


            }

            //Modulo C
            ID = 30000;
             Modulo = "C";
             Ruas = 14;
             Nivel = 4;
             Números = 36;
            for (int Rua = 1; Rua <= Ruas; Rua++)
            {
                for (int N = 0; N < Nivel; N++)
                {
                    for (int Numero = 1; Numero <= Números; Numero += 2)
                    {
                        Inserir(ID++, Modulo, Rua, Niveis()[N], Numero);
                    }

                }

                for (int N = 0; N < Nivel; N++)
                {
                    for (int Numero = 2; Numero <= Números; Numero += 2)
                    {
                        Inserir(ID++, Modulo, Rua, Niveis()[N], Numero);
                    }

                }


            }

            //Modulo D
             ID = 40000;
             Modulo = "D";
             Ruas = 10;
             Nivel = 3;
             Números = 20;
            for (int Rua = 1; Rua <= Ruas; Rua++)
            {
                for (int N = 0; N < Nivel; N++)
                {
                    for (int Numero = 1; Numero <= Números; Numero += 2)
                    {
                        Inserir(ID++, Modulo, Rua, Niveis()[N], Numero);
                    }

                }

                for (int N = 0; N < Nivel; N++)
                {
                    for (int Numero = 2; Numero <= Números; Numero += 2)
                    {
                        Inserir(ID++, Modulo, Rua, Niveis()[N], Numero);
                    }

                }


            }
        }

        private void Inserir(int ID,string Modulo,int Rua,string Nivel, int Numero, string Setro = "Armazem")
        {
            var Posicao = Modulo + string.Format("{0,02:D2}", Rua) + Nivel + string.Format("{0,03:D3}", Numero);

            
            comando.Parameters.AddWithValue("@ID", ID);
            comando.Parameters.AddWithValue("@Modulo", Modulo);
            comando.Parameters.AddWithValue("@Rua", Rua);
            comando.Parameters.AddWithValue("@Nivel", Nivel);
            comando.Parameters.AddWithValue("@Numero", Numero);
            comando.Parameters.AddWithValue("@Posicao", Posicao);
            comando.Parameters.AddWithValue("@Setor", Setro);
            comando.Parameters.AddWithValue("@Status", "Ativo");
            comando.Parameters.AddWithValue("@obs", "-");
            comando.Parameters.AddWithValue("@Loja", "1");
            

            comando.CommandText = "INSERT INTO [_07_Posicoes] " +
                "VALUES( " +
                "@ID, " +
                "@Modulo," +
                "@Rua, " +
                "@Nivel, " +
                "@Numero, " +
                "@Posicao, " +
                "@Setor, " +
                "@Status, " +
                "@obs, " +
                "@Loja )";

            comando.ExecuteNonQuery();
            comando.Parameters.Clear();
        }
        private List<string> Niveis()
        {
            var _Niveis = new List<string>();
            _Niveis.Add("A");
            _Niveis.Add("B");
            _Niveis.Add("C");
            _Niveis.Add("D");
            return _Niveis;
        }
       

        public int PesquisarRuas(string _Modulo)
        {
            // converter os parametros
            comando.Parameters.AddWithValue("@Modulo", _Modulo);
            //Definir comando de pesquisa
            comando.CommandText = "SELECT  COUNT(DISTINCT Rua) FROM [_07_Posicoes] " +
                "WHERE " +
                "Modulo = @Modulo ";
            //Executar comando
            Ler = comando.ExecuteReader();
            //Limpar paramentros
            comando.Parameters.Clear();
            int Ruas =0;
            while (Ler.Read())
            {
                 Ruas = int.Parse(Ler[0].ToString());
            }
            Ler.Close();
            return Ruas;
            
        }

        public int PesquisarNiveis(string _Modulo)
        {
            // converter os parametros
            comando.Parameters.AddWithValue("@Modulo", _Modulo);
            //Definir comando de pesquisa
            comando.CommandText = "SELECT  COUNT(DISTINCT Nivel) FROM [_07_Posicoes] " +
                "WHERE " +
                "Modulo = @Modulo ";
            //Executar comando
            Ler.Close();
            Ler = comando.ExecuteReader();
            
            //Limpar paramentros
            comando.Parameters.Clear();
            int Niveis = 0;
            while (Ler.Read())
            {
                Niveis = int.Parse(Ler[0].ToString());
            }
            Ler.Close();
            return Niveis;
        }

        public int PesquisarNumeros(string _Modulo)
        {
            // converter os parametros
            comando.Parameters.AddWithValue("@Modulo", _Modulo);
            //Definir comando de pesquisa
            comando.CommandText = "SELECT   COUNT(DISTINCT Número) FROM [_07_Posicoes] " +
                "WHERE " +
                "Modulo = @Modulo ";

            //Executar comando
            Ler.Close();
            Ler = comando.ExecuteReader();
            //Limpar paramentros
            comando.Parameters.Clear();
            int Numeros = 0;
            while (Ler.Read())
            {
                Numeros = int.Parse(Ler[0].ToString());
            }
            Ler.Close();
            return Numeros;
        }

        public List<cl_051_Posicao> PesquisarPosicoes(string Modulo ="A", int _ID_Produto = 0) 
        {


            var Posicoes = new List<cl_051_Posicao>();

            
            //Definir parametros
            comando.Parameters.AddWithValue("@Modulo", Modulo);
            comando.Parameters.AddWithValue("@ID_Produto", _ID_Produto);

            //Definir comando
            comando.CommandText =
                "SELECT "+
                "p.ID_Posicao, "+
                "p.Posicao, "+
                "TRIM(p.Status), "+
                "coalesce(sum(m.Quantidade), 0) AS QUANTIDADE, "+
                " coalesce(SUM(CASE Pr.ID "+
                " WHEN @ID_Produto " +
                " THEN m.Quantidade "+
                " END), 0) AS ESPECIE, " +
                "coalesce(MIN(l.Validade),GETDATE())" +
                "FROM _07_Posicoes        AS P "+
                "LEFT JOIN  _06_Movimentacoes AS M ON P.ID_Posicao = M.ID_Posicao "+
                "left join _05_Acao as A on m.ID_Acao = a.ID_Acao "+
                "left join _04_Lotes as L  ON A.ID_Lote = L.ID_Lote "+
                "LEFT JOIN _02_Produtos AS Pr ON L.ID_Produto = PR.ID "+
                "WHERE "+
                "P.Modulo = @Modulo " +
                "and "+
                "p.Rua > 0 "+
                "AND " +
                "p.Nivel like '%' " +
                "and " +
                "p.Número > 0 " +
                "group by " +
                "p.ID_Posicao, " +
                "p.Posicao, " +
                "p.Status " +
                "ORDER BY P.ID_Posicao ";

            //Executar comando
            //Executar comando
            Ler.Close();
            Ler = comando.ExecuteReader();
            //Limpar paramentros
            comando.Parameters.Clear();



            while (Ler.Read())
            {
                Posicoes.Add(
                    new cl_051_Posicao
                    {
                        ID = int.Parse(Ler[0].ToString()),
                        Nome = Ler[1].ToString(),
                        Status = Ler[2].ToString(),
                        Quantidade = double.Parse(Ler[3].ToString()),
                        QuantidadeP = double.Parse(Ler[4].ToString()),
                        Class_Posicao = Class_Posicao(
                            Ler[2].ToString(), 
                            double.Parse(Ler[3].ToString()),
                            double.Parse(Ler[4].ToString()),
                            DateTime.Parse(Ler[5].ToString())
                            )
                    }
                            );
            }
            Ler.Close();
            return Posicoes;
           
        }    

        private string Class_Posicao(string _Status, Double _Quantidade, Double _QuantidadeP, DateTime _Validade)
        {
            var alerta = "";
            if (_Validade < DateTime.Now.AddDays(10) && _Quantidade >0)
                alerta = " ALERTA";

            if(_Status == "Bloqueado" + alerta)
            {
                if (_QuantidadeP > 0)
                    return "VVerde"+ alerta;

                if (_Quantidade > 0)
                    return "VAzul" +alerta;
               
                    return "VBranco" +alerta;
            }

            if (_QuantidadeP > 0)
                return "Verde" + alerta; ;

            if (_Quantidade > 0)
                return "Azul" + alerta; ;

            return "Branco" + alerta; ;
        }

        public cl_051_Posicao PesquisarPosicao(int ID_Posicao)
        {
            var Posicao = new cl_051_Posicao();
            //Definir Parâmetros
            comando.Parameters.AddWithValue("@ID_Posicao", ID_Posicao);
            //Denifr comando
            comando.CommandText =
                "SELECT * FROM _07_Posicoes" +
                " WHERE ID_Posicao = @ID_Posicao";
            Ler = comando.ExecuteReader();
            comando.Parameters.Clear();
            if (!Ler.HasRows)
                return Posicao;

            Ler.Read();
                Posicao.ID = int.Parse(Ler[0].ToString());
                Posicao.Modulo = Ler[1].ToString();
                Posicao.Rua = int.Parse(Ler[2].ToString());
                Posicao.Nivel = Ler[3].ToString();
                Posicao.Numero = int.Parse(Ler[4].ToString());
                Posicao.Nome = Ler[5].ToString();
                Posicao.Setor = Ler[6].ToString();
                Posicao.Status = Ler[7].ToString();
            
            Ler.Close();
            return Posicao;
        }

        public cl_0512_Modulo GerarArmazem(string _Modulo , int _ID_Produto)
        {
            var Modulo = new cl_0512_Modulo();
            Modulo.Modulo = _Modulo;
            Modulo.QTD_Ruas = PesquisarRuas(_Modulo);
            var Qtd_Niveis = PesquisarNiveis(_Modulo);
            
            Modulo.Niveis = new List<string>();
            

            for (int x = 0; x< Qtd_Niveis; x++)
            {
                Modulo.Niveis.Add(Niveis()[x]);
            }


            Modulo.QTD_Numeros = PesquisarNumeros(_Modulo)/2 ;
            Modulo.Posicoes = PesquisarPosicoes(_Modulo, _ID_Produto);
            return Modulo;
        }

        public void Bloqueio(int _ID_Posicao) 
        {
            string Status;
            //converter paramentros
            comando.Parameters.AddWithValue("@ID", _ID_Posicao);
            if (SeBloquado(_ID_Posicao))
            {
                Status = "Desbloquado";
            }
            else
            {
                Status = "Desbloquado";
            }

            comando.CommandText =
                "UPDATE [_07_Posicoes] " +
                "SET " +
                "Status = "+ Status;

        }

        public Boolean SeBloquado(int _ID_Posicao)
        {
            //converter paramentros
            comando.Parameters.AddWithValue("@ID", _ID_Posicao);
            //comando
            comando.CommandText =
                "Select Status FROM _07_Posicoes " +
                "WHERE ID_Posicao = @ID";
            Ler.Read();
            string status = Ler[0].ToString();
            Ler.Close();
            comando.Parameters.Clear();
            if(status == "Bloqueado")
            {
                return true;
            }
            else
            {
                return true;
            }
        }

      
    }
}