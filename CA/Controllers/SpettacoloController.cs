using BLL.Services;
using CA.Utils;
using System;
using System.Collections.Generic;
using DAL.Models;

namespace CA.Controllers
{
	internal class SpettacoloController(SpettacoloService spettacoloService)
	{
		private readonly SpettacoloService _spettacoloService = spettacoloService;

		public void Add()
		{
			string? titolo = ImmissioneUtility.Stringa("titolo");
			string? descrizione = ImmissioneUtility.Stringa("descrizione");
			DateTime? dataEOra = ImmissioneUtility.DataOppureDataEOra(1);
			uint? durata = ImmissioneUtility.NumeroNaturale("durata (in minuti)");
			decimal? prezzoBase = ImmissioneUtility.NumeroRazionale("prezzo base");

			if (titolo is null || descrizione is null || dataEOra is null || durata is null || prezzoBase is null)
			{
				Console.WriteLine("Tutti i campi sono obbligatori");
				return;
			}

			if (_spettacoloService.Add(titolo, descrizione, dataEOra.Value, durata.Value, prezzoBase.Value))
			{
				Console.WriteLine("Spettacolo inserito con successo");
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

			if (_spettacoloService.Delete(id.Value))
			{
				Console.WriteLine("Spettacolo eliminato con successo");
			}
			else
			{
				Console.WriteLine("Spettacolo non eliminato perché non esistente o per errore interno al server");
			}
		}
		public void Get()
		{
			List<Spettacolo> spettacoli = _spettacoloService.Get();

			if (spettacoli.Count > 0)
			{
				StampaUtility.Lista<Spettacolo>(spettacoli);
			}
			else
			{
				Console.WriteLine("Nessuno spettacolo presente oppure errore interno al server");
			}
		}
		public void GetById()
		{
			uint? id = ImmissioneUtility.NumeroNaturale("id");
			if (id is null)
			{
				return;
			}

			Spettacolo? spettacolo = _spettacoloService.Get(id.Value);
			if (spettacolo is not null)
			{
				StampaUtility.Oggetto<Spettacolo>(spettacolo);
			}
			else
			{
				Console.WriteLine("Spettacolo non presente opppure errore interno al server");
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

		Spettacolo? spettacolo = _spettacoloService.Get(id.Value);

			if (spettacolo is null)
			{
				Console.WriteLine("Spettacolo non presente opppure errore interno al server");
				return;
			}

			string? titolo = ImmissioneUtility.Stringa("titolo");
			string? descrizione = ImmissioneUtility.Stringa("descrizione");
			DateTime? dataEOra = ImmissioneUtility.DataOppureDataEOra(1);
			uint? durata = ImmissioneUtility.NumeroNaturale("durata (in minuti)");
			decimal? prezzoBase = ImmissioneUtility.NumeroRazionale("prezzo base");

			if (titolo is null && descrizione is null && dataEOra is null && durata is null && prezzoBase is null)
			{
				return;
			}

			if (_spettacoloService.Update(id.Value, titolo, descrizione, dataEOra, durata, prezzoBase))
			{
				Console.WriteLine("Spettacolo aggiornato con successo");
			}
			else
			{
				Console.WriteLine("Errore interno al server");
			}
		}
		public void Search()
		{
			string? titolo = ImmissioneUtility.Stringa("titolo");
			string? descrizione = ImmissioneUtility.Stringa("descrizione");
			DateTime? dataEOra = ImmissioneUtility.DataOppureDataEOra(1);
			uint? durata = ImmissioneUtility.NumeroNaturale("durata (in minuti)");
			decimal? prezzoBase = ImmissioneUtility.NumeroRazionale("prezzo base");

			List<Spettacolo>? spettacoliTrovati = _spettacoloService.Search(titolo, descrizione, dataEOra, durata, prezzoBase);

			if (spettacoliTrovati is not null && spettacoliTrovati.Count > 0)
			{
				StampaUtility.Lista<Spettacolo>(spettacoliTrovati);
			}
			else
			{
				Console.WriteLine("Nessuno spettacolo trovato oppure errore interno al server");
			}
		}
		public void Menu()
		{
			string input;
			bool verificaNumeroNaturale;
			uint scelta;

			do
			{
				Console.WriteLine("\nMenu spettacolo\n\n1. Aggiungi\n2. Cancella\n3. Visualizza lista completa\n4. Visualizza per id\n5. Modifica\n6. Cerca\n0. Indietro");
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
						break;
					default:
						Console.WriteLine("Scelta invalida");
						break;
				}

			}
			while (scelta != 0);
		}
	}
}