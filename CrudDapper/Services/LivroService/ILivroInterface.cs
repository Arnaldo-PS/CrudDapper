using CrudDapper.Models;

namespace CrudDapper.Services.LivroService
{
    public interface ILivroInterface
    {
        Task<IEnumerable<Livro>> GetAllLivros();
        Task<Livro> GetLivroById(int livroId);
        Task<IEnumerable<Livro>> CreateLivro(Livro livro);
        Task<IEnumerable<Livro>> UpdateLivro(Livro livro);
        Task<IEnumerable<Livro>> DeleteLivro(int livroId);
    }
}
