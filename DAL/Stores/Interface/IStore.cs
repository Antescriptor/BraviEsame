using System.Collections.Generic;


namespace DAL.Stores.Interface
{
	public interface IStore<T>
	{
		public bool Add(T item);
		public bool Delete(uint id);
		public List<T> Get();
		public T? Get(uint id);
		public bool Update(T item);
	}
}
