using BLL.Services;
using CA.Utils;
using System;
using DAL.Models;
using System.Collections.Generic;

namespace CA.Controllers
{
	internal class PrenotazioneController(PrenotazioneService prenotazioneService)
	{
		private readonly PrenotazioneService _prenotazioneService = prenotazioneService;

		public void Add()
		{
			uint? postiMassimi = ImmissioneUtility.NumeroNaturale("posti massimi") ?? 50;
			string? titolo = ImmissioneUtility.Stringa("titolo spettacolo");
			DateTime? dataEOraInizio = ImmissioneUtility.DataOppureDataEOra(1);
			string? posto = ImmissioneUtility.Stringa("posto");


			if (titolo is null || dataEOraInizio is null || posto is null)
			{
				Console.WriteLine("Titolo, data e ora d'inizio dello spettacolo e posto a sedere sono obbligatori");
				return;
			}

			uint? idCliente = ImmissioneUtility.NumeroNaturale("id cliente");

			string? nome = null;
			string? cognome = null;
			string? email = null;
			string? telefono = null;

			if (idCliente is null)
			{
				Console.WriteLine("ID cliente non immesso.\nInserire i dati del cliente da registrare per completare l'inserimento della prenotazione:");
				nome = ImmissioneUtility.Stringa("nome");
				cognome = ImmissioneUtility.Stringa("cognome");
				email = ImmissioneUtility.Stringa("email");
				telefono = ImmissioneUtility.Stringa("telefono");
			}

			if (idCliente is null && (nome is null || cognome is null))
			{
				Console.WriteLine("ID cliente oppure nome e cognome di cliente da registrare sono obbligatori");
				return;
			}

			if (_prenotazioneService.AddFirstAvailable(postiMassimi.Value, out uint postiRimanenti, titolo, dataEOraInizio.Value, posto, idCliente, nome, cognome, email, telefono))
			{
				Console.WriteLine($"Posti rimanenti: {postiRimanenti}\nPrenotazione inserita con successo");
			}
			else
			{
				Console.WriteLine("Posti esauriti oppure errore interno al server");
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

			if (_prenotazioneService.Delete(id.Value))
			{
				Console.WriteLine("Prenotazione eliminata con successo");
			}
			else
			{
				Console.WriteLine("Prenotazione non eliminata perché non esistente o per errore interno al server");
			}
		}
		public void Get()
		{
			List<Prenotazione> prenotazioni = _prenotazioneService.Get();

			if (prenotazioni.Count > 0)
			{
				StampaUtility.Lista<Prenotazione>(prenotazioni);
			}
			else
			{
				Console.WriteLine("Nessuna prenotazione presente oppure errore interno al server");
			}
		}
		public void GetById()
		{
			uint? id = ImmissioneUtility.NumeroNaturale("id");
			if (id is null)
			{
				return;
			}

			Prenotazione? prenotazione = _prenotazioneService.Get(id.Value);
			if (prenotazione is not null)
			{
				StampaUtility.Oggetto<Prenotazione>(prenotazione);
			}
			else
			{
				Console.WriteLine("Prenotazione non presente");
			}
		}
		public void Update() {
			uint? id = ImmissioneUtility.NumeroNaturale("id");
			if (id is null)
			{
				Console.WriteLine("Id non valido");
				return;
			}

			Prenotazione? prenotazione = _prenotazioneService.Get(id.Value);

			if (prenotazione is null)
			{
				Console.WriteLine("Prenotazione non presente oppure errore interno al server.");
				return;
			}

			uint? idSpettacolo = ImmissioneUtility.NumeroNaturale("id spettacolo");
			uint? idCliente = ImmissioneUtility.NumeroNaturale("id cliente");
			string? posto = ImmissioneUtility.Stringa("posto");
			decimal? prezzo = ImmissioneUtility.NumeroRazionale("prezzo");
			DateTime? dataEOraArrivo = ImmissioneUtility.DataOppureDataEOra(1);

			if (idSpettacolo is null && idCliente is null && dataEOraArrivo is null)
			{
				return;
			}

			if (_prenotazioneService.Update(id.Value, idSpettacolo, idCliente, posto, prezzo, dataEOraArrivo))
			{
				Console.WriteLine("Prenotazione aggiornata con successo");
			}
			else
			{
				Console.WriteLine("Errore interno al server");
			}
		}
		public void Search()
		{
			uint? idSpettacolo = ImmissioneUtility.NumeroNaturale("id spettacolo");
			uint? idCliente = ImmissioneUtility.NumeroNaturale("id cliente");
			string? posto = ImmissioneUtility.Stringa("posto");
			decimal? prezzo = ImmissioneUtility.NumeroRazionale("prezzo");
			DateTime? dataEOraArrivo = ImmissioneUtility.DataOppureDataEOra(1);

			List<Prenotazione>? prenotazioniTrovate = _prenotazioneService.Search(idSpettacolo, idCliente, posto, prezzo, dataEOraArrivo);

			if (prenotazioniTrovate is not null && prenotazioniTrovate.Count > 0)
			{
				StampaUtility.Lista<Prenotazione>(prenotazioniTrovate);
			}
			else
			{
				Console.WriteLine("Nessuna prenotazione trovata oppure errore interno al server");
			}
		}
		public void Menu()
		{
			string input;
			bool verificaNumeroNaturale;
			uint scelta;

			do
			{
				Console.WriteLine("\nMenu prenotazione\n\n1. Aggiungi\n2. Cancella\n3. Visualizza lista completa\n4. Visualizza per id\n5. Aggiorna\n6. Cerca\n0. Indietro\n");
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
