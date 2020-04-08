using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WMS3._1.Models;

namespace WMS3._1.Apps
{
    public class app_Usuario
    {
        SqlCommand comando;
        SqlDataReader Ler;
        public app_Usuario()
        {
            comando = new app_SqlConnection().Conecar();
        }

        public List<cl_06_Usuario> Permições(string _ID_Usuario)
        {
            var Lista = new List<cl_06_Usuario>();
            //converter parâmetros
            comando.Parameters.AddWithValue("@ID_Usuario", _ID_Usuario);
            comando.CommandText =
                "select " +
                "r.Name , " +
                "r.Id  " +
                "from " +
                "AspNetRoles   as R " +
                "left join AspNetUserRoles as UR on UR.RoleId = r.Id " +
                "left join AspNetUsers as U on UR.UserId = U.Id " +
                "group by " +
                "r.Name , " +
                "r.Id " +
                "having " +
                "sum(case U.Id  when @ID_Usuario then 1 else 0 end) = 0 "; 
            Ler = comando.ExecuteReader();
            while (Ler.Read())
            {
                Lista.Add( 
                    new cl_06_Usuario(){
                    Name = Ler[0].ToString() ,
                    Id = Ler[1].ToString()
                
                });
            }

            return Lista;
        }

    }
}