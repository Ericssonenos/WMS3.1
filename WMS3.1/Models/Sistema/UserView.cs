using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WMS3._1.Models.Sistema
{
    public class UserView
    {
        public string UserId { get; set; }


        public string Nome { get; set; }


        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public RoleView Role { get; set; }

        public List<RoleView>Roles { get; set; }


    }
}