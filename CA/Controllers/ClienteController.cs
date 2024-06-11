using BLL.Services;
using System;
using DAL.Models;
using System.Collections.Generic;
using CA.Utils;

namespace CA.Controllers
{
	internal class ClienteController(ClienteService clienteService)
	{
		private readonly ClienteService _clienteService = clienteService;

		public void Add()
		{
			string? nome = ImmissioneUtility.Stringa("nome");
			string? cognome = ImmissioneUtility.Stringa("cognome");
			string? email = ImmissioneUtility.Stringa("email");
			string? telefono = ImmissioneUtility.Stringa("telefono");

			if (nome is null || cognome is null)
			{
				Console.WriteLine("Nome e cognome sono obbligatori");
				return;
			}

			if (_clienteService.Add(nome, cognome, email, telefono))
			{
				Console.WriteLine("Cliente inserito con successo");
			}
            else
            {
				Console.WriteLine("Errore interno al server");
			}
        }
		public void Delete()
		{
			uint? id = ImmissioneUtility.NumeroNaturale("id");
			if (id is null)
			{
				Console.WriteLine("Id non valido");
				return;
			}

			if (_clienteService.Delete(id.Value))
			{
				Console.WriteLine("Cliente eliminato con successo");
			}
			else
			{
				Console.WriteLine("Cliente non eliminato perché non esistente o per errore interno al server");
			}
		}
		public void Get()
		{
			List<Cliente> clienti = _clienteService.Get();

			if (clienti.Count > 0)
			{
				StampaUtility.Lista<Cliente>(clienti);
			}
			else
			{
				Console.WriteLine("Nessun cliente presente oppure errore interno al server");
			}
		}
		public void GetById()
		{
			uint? id = ImmissioneUtility.NumeroNaturale("id");
			if (id is null)
			{
				Console.WriteLine("Id non valido");
				return;
			}

			Cliente? cliente = _clienteService.Get(id.Value);

			if (cliente is not null)
			{
				StampaUtility.Oggetto<Cliente>(cliente);
			}
			else
			{
				Console.WriteLine("Cliente non presente oppure errore interno al server");
			}
		}
		public void Update()
		{
			uint? id = ImmissioneUtility.NumeroNaturale("id");
			if (id is null)
			{
				Console.WriteLine("Id non valido");
				return;
			}

			Cliente? cliente = _clienteService.Get(id.Value);
            if (cliente is null)
            {
				Console.WriteLine("Cliente non presente oppure errore interno al server");
				return;
            }

            string? nome = ImmissioneUtility.Stringa("nome");
			string? cognome = ImmissioneUtility.Stringa("cognome");
			string? email = ImmissioneUtility.Stringa("email");
			string? telefono = ImmissioneUtility.Stringa("telefono");

			if (nome is null && cognome is null && email is null && telefono is null)
			{
				return;
			}
			if (_clienteService.Update(id.Value, nome, cognome, email, telefono))
			{
				Console.WriteLine("Cliente aggiornato con successo");
			}
			else
			{
				Console.WriteLine("Errore interno al server");
			}
		}
		public void Search()
		{
			string? nome = ImmissioneUtility.Stringa("nome");
			string? cognome = ImmissioneUtility.Stringa("cognome");
			string? email = ImmissioneUtility.Stringa("email");
			string? telefono = ImmissioneUtility.Stringa("telefono");

			List<Cliente>? clientiTrovati = _clienteService.Search(nome, cognome, email, telefono);

			if (clientiTrovati is not null && clientiTrovati.Count > 0)
			{
				StampaUtility.Lista<Cliente>(clientiTrovati);
			}
			else
			{
				Console.WriteLine("Nessun cliente trovato oppure errore interno al server");
			}
		}
		public void Menu()
		{
			string input;
			bool verificaNumeroNaturale;
			uint scelta;

			do
			{
				Console.WriteLine("\nMenu Cliente\n\n1. Aggiungi\n2. Cancella\n3. Visualizza lista completa\n4. Visualizza per id\n5. Aggiorna\n6. Cerca\n0. Indietro");
				input = Console.ReadLine() ?? "";
				verificaNumeroNaturale = uint.TryParse(input, out scelta);
				if (!verificaNumeroNaturale) scelta = uint.MaxValue;

				switch (scelta)
				{
					case 1:
						Add();
						break;
					case 2:
						Delete();
						break;
					case 3:
						Get();
						break;
					case 4:
						GetById();
						break;
					case 5:
						Update();
						break;
					case 6:
						Search();
						break;
					case 0:
						Console.WriteLine("Scelta invalida");
						break;
				}
			} while (scelta != 0);
		}
	}
}