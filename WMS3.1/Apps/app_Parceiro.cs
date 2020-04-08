using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WMS3._1.Models;
using WMS3._1.Apps;
using System.Data.SqlClient;
using static WMS3._1.Models.Serializar.NFeProca_pasta.cl_NFe;
using WMS3._1.Models.Serializar;
using Microsoft.Office.Interop.Excel;
using System.IO;
using WMS3._1.Models.Serializar.NFeProca_pasta.NFe_pasta;

namespace WMS3._1.Apps
{
    public class app_Parceiro
    {
        SqlCommand comando;
        SqlDataReader Ler;

        public app_Parceiro() 
        {
            comando = new app_SqlConnection().Conecar();
        }

        public cl_InfNFe Organizar(cl_XML _NFE)
        {

            _NFE.NFe.InfNFe.Chave = _NFE.Protocolo.InfProtocolo.Chave;

            // verificar se e CPF ou CNPJ
            cl_021_Parceiro Documento = new cl_021_Parceiro();
            if (_NFE.NFe.InfNFe.Destinatario.CNPJ == null)
            {
                Documento.CNPJ_CPF = _NFE.NFe.InfNFe.Destinatario.CPF;
            }
            else
            {
                Documento.CNPJ_CPF = _NFE.NFe.InfNFe.Destinatario.CNPJ;
            }

            // Pesquisar Documento, nome, Código, e ID do Destinatário
            var Destinario = Pesquisar(Documento).FirstOrDefault();
            _NFE.NFe.InfNFe.Destinatario.CNPJ_CPF = Destinario.CNPJ_CPF;
            _NFE.NFe.InfNFe.Destinatario.xNome = Destinario.Nome;
            _NFE.NFe.InfNFe.Destinatario.Codigo = Destinario.Codigo;
            _NFE.NFe.InfNFe.Destinatario.ID = Destinario.ID;

            // Pesquisar Documento, Codigo, e ID do Emitente
            Documento = new cl_021_Parceiro();
            Documento.CNPJ_CPF = _NFE.NFe.InfNFe.Emitente.CNPJ;
            var Emitente = Pesquisar(Documento).FirstOrDefault();
            _NFE.NFe.InfNFe.Emitente.Codigo = Emitente.Codigo;
            _NFE.NFe.InfNFe.Emitente.ID = Emitente.ID;

            //Verificar se ele já foi atualizado
            if (Emitente.Status != "Atualizado")
                Atualizar(_NFE.NFe.InfNFe.Emitente);

            return _NFE.NFe.InfNFe;
        }

        public void Atualizar(cl_Emitente Parceiro)
        {
            // Converter para formato SQL
            comando.Parameters.AddWithValue("@ID", Parceiro.ID);
            comando.Parameters.AddWithValue("@Nome", Parceiro.xNome);
            comando.Parameters.AddWithValue("@Logaritimo", Parceiro.Endereco.xLgr);
            comando.Parameters.AddWithValue("@Codigo_Municipio", Parceiro.Endereco.cMun);
            comando.Parameters.AddWithValue("@Municipio", Parceiro.Endereco.xMun);
            comando.Parameters.AddWithValue("@CEP", Parceiro.Endereco.CEP);
            comando.Parameters.AddWithValue("@Codigo_Pais", Parceiro.Endereco.cPais);
            comando.Parameters.AddWithValue("@Pais", Parceiro.Endereco.xPais);
            comando.Parameters.AddWithValue("@UF", Parceiro.Endereco.UF);
            comando.Parameters.AddWithValue("@Telefone", Parceiro.Endereco.fone);
            comando.Parameters.AddWithValue("@Status", "Atualizado");


            comando.CommandText =
                "UPDATE [_01_Parceiros] " +
                "SET " +
                "Nome = @Nome, " +
                "Logaritimo = @Logaritimo, " +
                "Codigo_Municipio = @Codigo_Municipio, " +
                "Municipio = @Municipio," +
                "CEP = @CEP, " +
                "Codigo_Pais = @Codigo_Pais, " +
                "Pais = @Pais, " +
                "UF = @UF, " +
                "Telefone = @Telefone, " +
                "Status = @Status " +
                "WHERE " +
                "ID = @ID";

            comando.ExecuteNonQuery();
            comando.Clone();
        }

        public void Atualizar(cl_021_Parceiro Parceiro)
        {
            // Converter para formato SQL
            comando.Parameters.AddWithValue("@ID", Parceiro.ID);
            comando.Parameters.AddWithValue("@Nome", Parceiro.Nome);
            comando.Parameters.AddWithValue("@Logaritimo", Parceiro.Logaritimo);
            comando.Parameters.AddWithValue("@Codigo_Municipio", Parceiro.Codigo_Municipio);
            comando.Parameters.AddWithValue("@Municipio", Parceiro.Municipio);
            comando.Parameters.AddWithValue("@CEP", Parceiro.CEP);
            comando.Parameters.AddWithValue("@Codigo_Pais", Parceiro.Codigo_Pais);
            comando.Parameters.AddWithValue("@Pais", Parceiro.Pais);
            comando.Parameters.AddWithValue("@UF", Parceiro.UF);
            comando.Parameters.AddWithValue("@Telefone", Parceiro.Telefone);
            comando.Parameters.AddWithValue("@Status", Parceiro.Status);


            comando.CommandText =
                "UPDATE [_01_Parceiros] " +
                "SET " +
                "Nome = @Nome, " +
                "Logaritimo = @Logaritimo, " +
                "Codigo_Municipio = @Codigo_Municipio, " +
                "Municipio = @Municipio," +
                "CEP = @CEP, " +
                "Codigo_Pais = @Codigo_Pais, " +
                "Pais = @Pais, " +
                "UF = @UF, " +
                "Telefone = @Telefone, " +
                "Status = @Status " +
                "WHERE " +
                "ID = @ID";

            comando.ExecuteNonQuery();
            comando.Clone();
        }

        public void Salvar(cl_021_Parceiro Parceiro)
        {

            comando.CommandText =
                "SELECT MAX(ID) " +
                "FROM [_01_Parceiros]";
            Ler = comando.ExecuteReader();
            Ler.Read();
            Parceiro.ID = int.Parse(Ler[0].ToString()) +1;
            Ler.Close();


            // Converter para formato SQL
            comando.Parameters.AddWithValue("@ID", Parceiro.ID);
            comando.Parameters.AddWithValue("@Nome", Parceiro.Nome);
            comando.Parameters.AddWithValue("@CNPJ_CPF", Parceiro.CNPJ_CPF);
            comando.Parameters.AddWithValue("@Logaritimo", Parceiro.Logaritimo);
            comando.Parameters.AddWithValue("@Codigo_Municipio", Parceiro.Codigo_Municipio);
            comando.Parameters.AddWithValue("@Municipio", Parceiro.Municipio);
            comando.Parameters.AddWithValue("@CEP", Parceiro.CEP);
            comando.Parameters.AddWithValue("@Codigo_Pais", Parceiro.Codigo_Pais);
            comando.Parameters.AddWithValue("@Pais", Parceiro.Pais);
            comando.Parameters.AddWithValue("@UF", Parceiro.UF);
            comando.Parameters.AddWithValue("@Telefone", Parceiro.Telefone);
            comando.Parameters.AddWithValue("@Status", Parceiro.Status);


            comando.CommandText =
                "INSERT INTO [_01_Parceiros] " +
                "VALUES " +
                "@ID, " +
                "@Nome, " +
                "@CNPJ_CPF " +
                "@Logaritimo, " +
                "@Codigo_Municipio, " +
                "@Municipio," +
                "@CEP, " +
                "@Codigo_Pais, " +
                "@Pais, " +
                "@UF, " +
                "@Telefone, " +
                "@Status ";
            

            comando.ExecuteNonQuery();
            comando.Clone();
        }

        public List<cl_021_Parceiro> Pesquisar(cl_021_Parceiro _parceiro)
        {
            //Verificar se os valores foram preenchidos, se não, considerar qualquer caracter
            var SinalID = " = ";
            if (_parceiro.ID ==0)
                SinalID = "> ";
           

            var SinalCodigo = " = ";
            if (String.IsNullOrEmpty(_parceiro.Codigo))
            {
                _parceiro.Codigo = "%";
                SinalCodigo = " LIKE ";
            }

            if (String.IsNullOrEmpty(_parceiro.Nome)) 
            {
                _parceiro.Nome = "";
            }

            var SinalCNPJ_CPF = " = ";
            if (String.IsNullOrEmpty(_parceiro.CNPJ_CPF))
            {
                _parceiro.CNPJ_CPF = "%";
                SinalCNPJ_CPF = " LIKE ";
            }

            // Converter para formato SQL
            comando.Parameters.AddWithValue("@ID", _parceiro.ID);
            comando.Parameters.AddWithValue("@Codigo", _parceiro.Codigo);
            comando.Parameters.AddWithValue("@Nome", _parceiro.Nome);
            comando.Parameters.AddWithValue("@CNPJ_CPF", _parceiro.CNPJ_CPF);

            //Definir comando de pesquisa
            comando.CommandText = "SELECT * FROM [_01_Parceiros] WHERE " +
           "ID " + SinalID + "@ID " +
            "AND " +
            "Codigo " + SinalCodigo + "@Codigo " +
            "AND " +
            "Nome LIKE '%' + @Nome + '%' " +
            "AND " +
            "CNPJ_CPF " + SinalCNPJ_CPF + "@CNPJ_CPF " +
            "order by Nome";

            // Executar Comando
            Ler = comando.ExecuteReader();
            comando.Parameters.Clear();

            var listaParceiro = new List<cl_021_Parceiro>();

            if (Ler.HasRows)
            {
                // Trasformar os dados para a classe Produtos
                
                while (Ler.Read())
                {
                    listaParceiro.Add(
                        new cl_021_Parceiro()
                        {
                            ID = int.Parse(Ler[0].ToString()),
                            Codigo = Ler[1].ToString(),
                            Nome = Ler[2].ToString(),
                            CNPJ_CPF = Ler[3].ToString(),
                            Logaritimo = Ler[4].ToString(),
                            Codigo_Municipio = Ler[5].ToString(),
                            Municipio = Ler[6].ToString(),
                            CEP = Ler[7].ToString(),
                            Codigo_Pais = Ler[8].ToString(),
                            Pais = Ler[9].ToString(),
                            UF = Ler[10].ToString(),
                            Telefone = Ler[11].ToString(),
                            Status = Ler[12].ToString()
                        }); ;
                }
               

            }
            else
            {
                listaParceiro.Add(new cl_021_Parceiro());
            }
            Ler.Close();
            return listaParceiro;

        }



        public List<cl_0212_BI_Parceiros> ListasBIFornecedor(
           int _ID = -1, string _Codigo = "%",
           string _Nome = "%", string _CNPJ_CPF = "%")
        {
            //Verificar se os caracter já forma prenchidos
            //se não considerar qualquer caracter

            var S_ID = "=";
            if (_ID == -1)
                S_ID = ">";

            var S_Codigo = "=";
            if (_Codigo == "%")
                S_Codigo = " LIKE ";

            var S_CNPJ_CPF = "=";
            if (_CNPJ_CPF == "%")
                S_CNPJ_CPF = " LIKE ";

            //converter parâmetros
            comando.Parameters.AddWithValue("@ID", _ID);
            comando.Parameters.AddWithValue("@Codigo", _Codigo);
            comando.Parameters.AddWithValue("@Nome", _Nome);
            comando.Parameters.AddWithValue("@CNPJ_CPF", _CNPJ_CPF);

            //Definir comando de pesquisa
            comando.CommandText =
                " SELECT " +
                "  [ID] " +
                ", [Nome] " +
                ", [CNPJ_CPF] " +
                ", COUNT(DISTINCT L.ID_Produto) AS Produtos " +
                ", COALESCE(SUM( M.Quantidade),0) AS Quantidade " +
                ", MIN( n.Emissao) As 'Primeira Compra' " +
                ", MAx(n.Emissao) As 'Última Compra' " +
                "FROM _03_NF AS N " +
                "LEFT JOIN _01_Parceiros AS P ON P.ID = N.ID_Fornecedor " +
                "LEFT JOIN _04_Lotes AS L ON N.ID_NF = L.ID_NF " +
                "LEFT JOIN _05_Acao AS A ON L.ID_Lote = A.ID_Lote " +
                "LEFT JOIN _06_Movimentacoes AS M ON A.ID_Acao = M.ID_Acao " +
                "WHERE " +
                "P.ID " + S_ID + " @ID " +
                "AND " +
                "P.ID " + S_Codigo + " @Codigo " +
                "AND " +
                "P.Nome LIKE '%'+@Nome+'%' " +
                "AND " +
                "P.CNPJ_CPF " + S_CNPJ_CPF + "@CNPJ_CPF " +
                "GROUP BY " +
                "[ID] " +
                ", [Nome] " +
                ", [CNPJ_CPF] " +
                "ORDER BY Quantidade DESC";
            // Executar comando
            Ler = comando.ExecuteReader();
            //Limapar parametros
            comando.Parameters.Clear();
            var lista = new List<cl_0212_BI_Parceiros>();
            while (Ler.Read())
            {
                lista.Add(
                    new cl_0212_BI_Parceiros
                    {
                        ID = int.Parse(Ler[0].ToString()),
                        Nome = Ler[1].ToString(),
                        CNPJ_CPF = Ler[2].ToString(),
                        Produtos = int.Parse(Ler[3].ToString()),
                        Quantidade = Double.Parse(Ler[4].ToString()),
                        Primeira_Compra = DateTime.Parse(Ler[5].ToString()),
                        Ultima_Compra = DateTime.Parse(Ler[6].ToString())
                    }
                    );
            }
            Ler.Close();
            return lista;
        }
    }
}