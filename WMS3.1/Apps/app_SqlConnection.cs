using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WMS3._1.Apps
{
    public class app_SqlConnection
    {
        public SqlCommand Conecar()
        {
            //var conexao = new SqlConnection(
            //    @"data source= DESKTOP-BTMP0PO\MODULO1; 
            //    Integrated Security=SSPI; 
            //    Initial Catalog = WMS02;
            //    Min Pool Size=5;
            //    Max Pool Size=250; 
            //    Connect Timeout=3");

            var conexao = new SqlConnection(
                    @"data source = 162.241.137.54; " +  
                    "initial catalog = ESousa_Pantanal; " +
                    "user id = ESousa_Ericsson; " +
                    "pwd = Tailor@01; " +
                    "packet size = 4096; " +
                    "persist security info = False; " +
                    "initial catalog = ESousa_Pantanal;  " );

            // conexao = new SqlConnection(@"data source = SmartWMS.mssql.somee.com;"+
            //"packet size = 4096 ;" +
            //"user id = ENDS_SQLLogin_1; " +
            //"pwd = kaxnwce7vn; " +
            //" persist security info = False;" +
            //" initial catalog = SmartWMS");

            if (conexao.State == System.Data.ConnectionState.Open)
                conexao.Dispose();

            conexao.Open();
            
            return   new SqlCommand() { Connection = conexao };
        }
    }
}