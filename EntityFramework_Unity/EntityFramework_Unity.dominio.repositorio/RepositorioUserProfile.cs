using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;  

using EntityFramework_Unity.comum.entidade;

namespace EntityFramework_Unity.dominio.repositorio
{
    public class RepositorioUserProfile : Repositorio<UserProfile>
    {         
        private readonly RepositorioEntityFramework db;

        public RepositorioUserProfile(RepositorioEntityFramework repositorio)
        {
            this.db = repositorio;
        }

        public override void Gravar(List<UserProfile> userProfile)
        {
            userProfile.ForEach(item => db.UserProfiles.Add(item));

            //db.UserProfiles.Add(userProfile);

            db.SaveChanges();            
        }

        public override List<UserProfile> RetornarTudo()
        {
            return db.UserProfiles.ToList<UserProfile>();        
        }
    }
}
