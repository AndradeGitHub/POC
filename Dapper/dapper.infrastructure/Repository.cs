using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

using Dapper;

using dapper.domain.model;

namespace dapper.domain
{
    public class Repository
    {
        private readonly string _strConexao;

        private readonly SqlConnection _conexaoBD;

        public Repository(string strConexao)
        {
            _strConexao = strConexao;            
        }

        public IEnumerable ConsultaSimples()
        {
            SqlConnection _conexaoBD = new SqlConnection(_strConexao);

            _conexaoBD.Open();

            var resultado = _conexaoBD.Query("Select * from Usuario");

            _conexaoBD.Close();

            return resultado;
        }

        public IEnumerable<Usuario> ConsultaSimplesTipada()
        {
            using (var _conexaoBD = new SqlConnection(_strConexao))
            {
                var resultado = _conexaoBD.Query<Usuario>("Select * from Usuario");

                return resultado;
            }
        }

        public IEnumerable<Usuario> ConsultaSimplesTipada1()
        {
            using (var conexaoBD = new SqlConnection(_strConexao))
            {
                var resultado = conexaoBD.Query<Usuario>
                    (
                      "Select CPF, Nome from Usuario Where Nome = @Nome", new { Nome = "Teste Nome" }
                    );

                return resultado;
            }
        }

        public IEnumerable ConsultaMultipla()
        {
            using (var conexaoBD = new SqlConnection(_strConexao))
            {
                var consulta = @"SELECT * FROM Pedido_Status WHERE Confirmacao = @Nome
                                 SELECT * FROM Usuario WHERE Nome = @Nome";

                using (var resultado = conexaoBD.QueryMultiple(consulta, new { Nome = "Teste Nome" }))
                {
                    var pedido = resultado.Read().Single();
                    var usuario = resultado.Read().ToList();

                    return usuario;
                }                
            }
        }

        public int Add()
        {
            using (var conexaoBD = new SqlConnection(_strConexao))
            {
                var usuario = new Usuario()
                {
                    CPF = "22222222223",
                    Nome = "Teste Nome 1"
                };

                var result = conexaoBD.Execute(@"Insert Usuario(CPF, Nome)
                                                 Values (@CPF, @Nome)", usuario);

                return result;
            }
        }

        public int Update()
        {
            using (var conexaoBD = new SqlConnection(_strConexao))
            {
                var atualizarBD = @"Update Usuario Set CPF = @CPF
                                    Where Nome = @Nome";

                var result = conexaoBD.Execute(atualizarBD, new
                {
                    CPF = "22222222224",
                    Nome = "Teste Nome"
                });

                return result;
            }
        }
    }
}
