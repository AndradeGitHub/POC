using System;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Collections.Generic;
using System.Globalization;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework_Unity.comum.entidade
{
    public abstract class Entidade
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]        
        public int ID { get; set; }
    }
}
