using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;  

using EntityFramework_Unity.comum.entidade;

namespace EntityFramework_Unity.dominio.repositorio
{
    public class RepositorioUserGroup : Repositorio<UserGroup>
    {
        private readonly RepositorioEntityFramework db;

        public RepositorioUserGroup(RepositorioEntityFramework repositorio)
        {
            this.db = repositorio;
        }

        public override void Gravar(List<UserGroup> userGroup)
        {            
            userGroup.ForEach(item => db.UserGroups.Add(item));

            //db.UserGroups.Add(userGroup);

            db.SaveChanges();            
        }

        public override List<UserGroup> RetornarTudo()
        {
            return db.UserGroups.ToList<UserGroup>();
        }
    }
}
