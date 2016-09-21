using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using EntityFramework_Unity.comum.entidade;
using EntityFramework_Unity.dominio.repositorio;

namespace EntityFramework_Unity.client.consoleapp
{
    class Program
    {
        private static RepositorioEntityFramework dbRepositorio = new RepositorioEntityFramework();

        private static dynamic repositorioFabrica;        

        private static UserGroup userGroup;

        static void Main(string[] args)
        {
            RegistrarUserGroup();
            RegistrarUserProfile();

            ConsultarUserGroupProfile();
        }

        public static void RegistrarUserGroup()
        {
            repositorioFabrica = FabricaDeRepositorioUnity.Criar<UserGroup, RepositorioUserGroup>(dbRepositorio);

            List<UserGroup> lstUserGroup = new List<UserGroup>();

            userGroup = new UserGroup { GroupName = "GroupName" };
            lstUserGroup.Add(userGroup);

            userGroup = new UserGroup { GroupName = "GroupName" };
            lstUserGroup.Add(userGroup);
            
            userGroup = new UserGroup { GroupName = "GroupName" };
            lstUserGroup.Add(userGroup);

            repositorioFabrica.Gravar(lstUserGroup);                        
        }

        public static void RegistrarUserProfile()
        {            
            repositorioFabrica = FabricaDeRepositorioUnity.Criar<UserProfile, RepositorioUserProfile>(dbRepositorio);

            List<UserProfile> lstUserProfile = new List<UserProfile>();

            foreach (var userGroup in dbRepositorio.UserGroups.Local.ToList<UserGroup>())
            {
                UserProfile userProfile = new UserProfile { UserName = "UserName", IdUserGroup = userGroup.ID };
                lstUserProfile.Add(userProfile);
            }

            repositorioFabrica.Gravar(lstUserProfile);            
        }

        public static void ConsultarUserGroupProfile()
        {
            List<UserGroup> retUserGroup = dbRepositorio.UserGroups.Local.ToList<UserGroup>();
            List<UserProfile> retUserProfile = dbRepositorio.UserProfiles.Local.ToList<UserProfile>();
        }
    }
}
