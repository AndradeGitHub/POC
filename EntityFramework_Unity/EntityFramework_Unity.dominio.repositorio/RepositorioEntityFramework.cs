using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;   

using EntityFramework_Unity.comum.entidade;

namespace EntityFramework_Unity.dominio.repositorio
{
    public class RepositorioEntityFramework : DbContext
    {        
        public RepositorioEntityFramework()
            : base("LocalTeste")
        {
        }     

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }   

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserProfile>().Property(dp => dp.ID).HasColumnName("ID").IsRequired();
            modelBuilder.Entity<UserProfile>().Property(dp => dp.UserName).HasColumnName("UserName").IsRequired();
            modelBuilder.Entity<UserProfile>().Property(dp => dp.IdUserGroup).HasColumnName("IdUserGroup").IsRequired();            
            modelBuilder.Entity<UserProfile>().ToTable("UserProfile");

            modelBuilder.Entity<UserGroup>().Property(dp => dp.ID).HasColumnName("ID").IsRequired();
            modelBuilder.Entity<UserGroup>().Property(dp => dp.GroupName).HasColumnName("GroupName").IsRequired();
            modelBuilder.Entity<UserGroup>().ToTable("UserGroup");   
        }      
    }
}