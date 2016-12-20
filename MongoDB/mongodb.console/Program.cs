using System;
using System.Configuration;
using System.Threading.Tasks;

using mongodb.infrastructure.persistence;
using mongodb.domain.model;
using mongodb.domain.repository;

namespace mongodb.console
{
    class Program
    {
        private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["ConnectionMongo"].ConnectionString;        
        private static readonly string _databaseName = ConfigurationManager.AppSettings["DataBaseName"];

        private static dynamic _repositoryBase;
        private static dynamic _repository;

        private static dynamic _repositoryCustom;

        static void Main(string[] args)
        {
            _repositoryBase = new RepositoryBase(_connectionString, _databaseName);

            _repository = RepositoryFactory.CreateRepository<EntityTeste, Repository<EntityTeste>>(_repositoryBase.CreateDataBase(), "Col_Teste");
            _repositoryCustom = RepositoryFactory.CreateRepositoryCustom<EntityTeste, RepositoryColTeste<EntityTeste>>(_repositoryBase.CreateDataBase(), "Col_Teste");

            GetAll();
            InsertData();
            UpdateData();
            GetById();            

            GetCustom();

            DeleteData();
        }

        private static void GetAll()
        {
            var result = _repository.GetAll();
        }

        private static void GetById()
        {
            var result = _repository.GetById(3);
        }

        private static void InsertData()
        {
            EntityTeste entity = new EntityTeste();
            entity.Id = 3;
            entity.Log = "Teste Log";
            entity.Date = DateTime.Now;

            Task.WaitAll(_repository.Insert(entity));
        }

        private static void UpdateData()
        {
            EntityTeste entity = new EntityTeste();
            entity.Id = 3;
            entity.Log = "Teste Log1";
            entity.Date = DateTime.Now;

            Task.WaitAll(_repository.Update(entity));
        }

        private static void DeleteData()
        {
            EntityTeste entity = new EntityTeste();
            entity.Id = 3;

            Task.WaitAll(_repository.Delete(entity));
        }

        private static void GetCustom()
        {
            EntityTeste entity = new EntityTeste();
            entity.Log = "Teste Log1";

            var result = _repositoryCustom.GetWhere(entity);
        }
    }
}
