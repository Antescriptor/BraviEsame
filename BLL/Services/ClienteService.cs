using DAL.Models;
using DAL.Stores.Interface;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
	public class ClienteService(IStore<Cliente> clienteStore)
	{
		private readonly IStore<Cliente> _clienteStore = clienteStore;
		public bool Add(string nome, string cognome, string? email = null, string? telefono = null)
		{
			uint id = GetNextId();
			Cliente cliente = new(id, nome, cognome, email, telefono);
			return Add(cliente);
		}

		public bool Add(Cliente clienteDaAggiungere)
		{
			return _clienteStore.Add(clienteDaAggiungere);
		}
		public bool Delete(uint id)
		{
			return _clienteStore.Delete(id);
		}
		public List<Cliente> Get()
		{
			return _clienteStore.Get();
		}
		public Cliente? Get(uint id)
		{
			return _clienteStore.Get(id);
		}
		public uint GetNextId()
		{
			List<Cliente> clienti = _clienteStore.Get();
			if (clienti.Count == 0)
			{
				return 1;
			}
			else
			{
				return clienti.Last().Id + 1;
			}
		}
		public List<Cliente>? Search(string? nome, string? cognome, string? email, string? telefono)
		{
			List<Cliente> clienti = _clienteStore.Get();

			List<Cliente>? clientiTrovati = clienti
				.Where(c =>
					(nome is not null ? c.Nome == nome : true) &&
					(cognome is not null ? c.Cognome == cognome : true) &&
					(c.Email is not null && email is not null ? c.Email == email : true) &&
					(c.Telefono is not null && telefono is not null ? c.Telefono == email : true))
				?.ToList();

			return clientiTrovati;
		}
		public bool Update(Cliente cliente)
		{
			return _clienteStore.Update(cliente);
		}
		public bool Update(uint id, string? nome = null, string? cognome = null, string? email = null, string? telefono = null)
		{
			Cliente? clienteDaAggiornare = _clienteStore.Get(id);

			if (clienteDaAggiornare is not null)
			{
				if (nome is not null) clienteDaAggiornare.Nome = nome;
				if (cognome is not null) clienteDaAggiornare.Cognome = cognome;
				if (email is not null) clienteDaAggiornare.Email = email;
				if (telefono is not null) clienteDaAggiornare.Telefono = telefono;

				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
