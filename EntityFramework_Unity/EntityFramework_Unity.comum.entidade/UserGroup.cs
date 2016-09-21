using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Globalization;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework_Unity.comum.entidade
{
    [Table("UserGroup")]
    public class UserGroup : Entidade
    {
        public string GroupName { get; set; }        
    }
}