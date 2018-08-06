using System.Collections.Generic;

namespace ProjetoModeloDDD.Domain.Interfaces.Repositories
{
	public interface IRepositoyBase<TEntity> where TEntity : class
	{
		/*CRUD*/
		void add(TEntity obj);
		TEntity GetById(int id);
		IEnumerable<TEntity> GetAll();
		void Update(TEntity obj);
		void Remove(TEntity obj);
		void Dispose();
	}
}
