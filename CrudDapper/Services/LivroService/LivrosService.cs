using CrudDapper.Models;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;

namespace CrudDapper.Services.LivroService
{
    public class LivrosService : ILivroInterface
    {
        private readonly IConfiguration _configuration;
        private readonly string getConnection;
        public LivrosService(IConfiguration configuration)
        {
            _configuration = configuration;
            getConnection = _configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Livro>> CreateLivro(Livro livro)
        {
            using (SqlConnection con = new SqlConnection(getConnection))
            {
                var sql = "insert into livros (titulo,autor) values (@titulo, @autor)";
                await con.ExecuteAsync(sql, livro);

                return await con.QueryAsync<Livro>("SELECT * FROM LIVROS");
            }
        }

        public async Task<IEnumerable<Livro>> DeleteLivro(int livroId)
        {
            using (SqlConnection con = new SqlConnection(getConnection))
            {
                var sql = " DELETE FROM LIVROS WHERE ID = @id";
                await con.ExecuteAsync(sql, new { id = livroId });

                return await con.QueryAsync<Livro>("SELECT * FROM LIVROS");
            }
        }

        public async Task<IEnumerable<Livro>> GetAllLivros()
        {
            using (SqlConnection con = new SqlConnection(getConnection))
            {
                var sql = "SELECT * FROM Livros";
                return await con.QueryAsync<Livro>(sql);
            }
        }
        
        public async Task<Livro> GetLivroById(int livroId)
        {
            using(SqlConnection con = new SqlConnection(getConnection))
            {
                var sql = "SELECT * FROM LIVROS WHERE ID = @Id";
                return await con.QueryFirstOrDefaultAsync<Livro>(sql, new { Id = livroId});
            }
        }

        public async Task<IEnumerable<Livro>> UpdateLivro(Livro livro)
        {
            using(SqlConnection con = new SqlConnection(getConnection))
            {
                var sql = "update livros set titulo = @titulo, autor = @autor where id = @id";
                await con.ExecuteAsync(sql, livro);
                return await con.QueryAsync<Livro>("SELECT * FROM LIVROS");
            }
        }
    }
}
