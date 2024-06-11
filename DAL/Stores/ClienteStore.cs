using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using DAL.Stores.Interface;

namespace DAL.Stores
{
	public class ClienteStore : IStore<Cliente>
	{
		private readonly List<Cliente> _clienti = new();
		public bool Add(Cliente cliente)
		{
			_clienti.Add(cliente);
			return true;
		}

		public bool Delete(uint id)
		{
			Cliente? cliente = Get(id);
			if (cliente is not null)
			{
				_clienti.Remove(cliente);
				return true;
			}
			else
			{
				return false;
			}
		}

		public List<Cliente> Get()
		{
			return _clienti;
		}

		public Cliente? Get(uint id)
		{
			return _clienti.FirstOrDefault(c => c.Id == id);
		}

		public bool Update(Cliente cliente)
		{
			Cliente? clienteDaAggiornare = _clienti.FirstOrDefault(c => c.Id == cliente.Id);
			if (clienteDaAggiornare is not null)
			{
				if (cliente.Nome is not null) clienteDaAggiornare.Nome = cliente.Nome;
				if (cliente.Cognome is not null) clienteDaAggiornare.Cognome = cliente.Cognome;
				if (cliente.Email is not null) clienteDaAggiornare.Email = cliente.Email;
				if (cliente.Telefono is not null) clienteDaAggiornare.Telefono = cliente.Telefono;
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
