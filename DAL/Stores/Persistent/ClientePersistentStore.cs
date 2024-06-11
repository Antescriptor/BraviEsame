using DAL.Models;
using DAL.Stores.Interface;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Stores.Persistent
{
	public class ClientePersistentStore(GestionaleDbContext dbContext) : IStore<Cliente>
	{
		private readonly GestionaleDbContext _dbContext = dbContext;
		public bool Add(Cliente cliente)
		{
			_dbContext.Add(cliente);
			_dbContext.SaveChanges();
			return true;
		}
		public List<Cliente> Get()
		{
			return _dbContext.Clienti.ToList();
		}

		public bool Delete(uint id)
		{
			Cliente? cliente = Get(id);
			if (cliente is not null)
			{
				_dbContext.Remove(cliente);
				_dbContext.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
		}

		public Cliente? Get(uint id)
		{
			return _dbContext.Clienti.FirstOrDefault(c => c.Id == id);
		}

		public bool Update(Cliente cliente)
		{
			Cliente? clienteDaAggiornare = _dbContext.Clienti.FirstOrDefault(c => c.Id == cliente.Id);
			if (clienteDaAggiornare is not null)
			{
				if (cliente.Nome is not null) clienteDaAggiornare.Nome = cliente.Nome;
				if (cliente.Cognome is not null) clienteDaAggiornare.Cognome = cliente.Cognome;
				if (cliente.Email is not null) clienteDaAggiornare.Email = cliente.Email;
				if (cliente.Telefono is not null) clienteDaAggiornare.Telefono = cliente.Telefono;

				_dbContext.Clienti.Update(clienteDaAggiornare);
				_dbContext.SaveChanges();

				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
