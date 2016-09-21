using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Globalization;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework_Unity.comum.entidade
{
    [Table("UserProfile")]
    public class UserProfile : Entidade
    {
        public string UserName { get; set; }
        public int IdUserGroup { get; set; }    
    }
}