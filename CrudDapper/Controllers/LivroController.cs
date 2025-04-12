using CrudDapper.Models;
using CrudDapper.Services.LivroService;
using Microsoft.AspNetCore.Mvc;

namespace CrudDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroInterface _livroInterface;
        public LivroController(ILivroInterface livroInterface)
        {
            _livroInterface = livroInterface;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livro>>> GetAllLivros()
        {
            try
            {
                IEnumerable<Livro> livros = await _livroInterface.GetAllLivros();

                if (!livros.Any())
                    return NotFound("Nenhum livro encontrado");

                return Ok(livros);

            }catch
            {
                return BadRequest("Erro ao buscar os livros");
            }
        }

        [HttpGet("{livroId}")]
        public async Task<ActionResult<Livro>> GetLivroById(int livroId)
        {
            try
            {
                Livro livro = await _livroInterface.GetLivroById(livroId);

                if (livro == null)
                    return NotFound("Registro não encontrado");

                return Ok(livro);
            }
            catch
            {
                return BadRequest("Erro ao buscar o livro");
            }
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Livro>>> CreateLivro(Livro livro)
        {
            try
            {
                IEnumerable<Livro> livros = await _livroInterface.CreateLivro(livro);

                return Ok(livros);
            }
            catch
            {
                return BadRequest("Não foi possível criar um novo livro");
            }
        }

        [HttpPut]
        public async Task<ActionResult<IEnumerable<Livro>>> UpdateLivro(Livro livro)
        {
            try
            {
                Livro registro = await _livroInterface.GetLivroById(livro.Id);
                if (registro == null)
                    return NotFound("Registro não localizado");

                IEnumerable<Livro> livros = await _livroInterface.UpdateLivro(livro);
                return Ok(livros);
            }
            catch
            {
                return BadRequest("Não foi possível atualizar este livro");
            }
        }

        [HttpDelete("{livroId}")]
        public async Task<ActionResult<IEnumerable<Livro>>> DeleteLivro(int livroId)
        {
            try
            {
                Livro registro = await _livroInterface.GetLivroById(livroId);
                if (registro == null)
                    return NotFound("Registro não encontrado");

                IEnumerable<Livro> livros = await _livroInterface.DeleteLivro(livroId);
                return Ok(livros);
            }
            catch
            {
                return BadRequest("Não foi possível excluir o livro");
            }
        }
    }
}
