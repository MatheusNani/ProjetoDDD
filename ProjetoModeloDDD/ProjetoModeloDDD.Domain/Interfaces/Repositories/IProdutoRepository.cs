using ProjetoModeloDDD.Domain.Entities;
using System.Collections.Generic;

namespace ProjetoModeloDDD.Domain.Interfaces.Repositories
{
	public interface IProdutoRepository : IRepositoyBase<Produto>
	{
		IEnumerable<Produto> BuscarPorNome(string nome);
	}
}
