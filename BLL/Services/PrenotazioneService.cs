using DAL.Models;
using DAL.Stores.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
	public class PrenotazioneService(IStore<Prenotazione> prenotazioneStore, ClienteService clienteService, SpettacoloService spettacoloService)
	{
		private readonly IStore<Prenotazione> _prenotazioneStore = prenotazioneStore;
		private readonly ClienteService _clienteService = clienteService;
		private readonly SpettacoloService _SpettacoloService = spettacoloService;

		public List<Spettacolo>? GetAvailable(uint postiMassimi, string titolo, DateTime dataEOraInizio, out uint postiRimanenti)
		{
			if (postiMassimi < 1)
			{
				postiRimanenti = 0;
				return null;
			}

			List<Spettacolo> spettacoli = _SpettacoloService.Get();
			if (spettacoli.Count < 1)
			{
				postiRimanenti = 0;
				return null;
			}

			List<Cliente> clienti = _clienteService.Get();
			if (clienti.Count < 1)
			{
				postiRimanenti = postiMassimi;
				return spettacoli;
			}

			List<Prenotazione> prenotazioni = _prenotazioneStore.Get();

			var spettacoliFiltratiConPrenotazioni =
				from spettacolo in spettacoli
				join prenotazione in prenotazioni
				on spettacolo.Id equals prenotazione.IdSpettacolo
				where
					spettacolo.Titolo.ToLower().Trim().Contains(titolo.ToLower().Trim()) &&
					spettacolo.DataEOra == dataEOraInizio &&
					dataEOraInizio >= DateTime.Now
				
				select spettacolo;

			int numeroSpettacoliFiltratiConPrenotazioni = spettacoliFiltratiConPrenotazioni.Count();

			int postiRimanentiZ = (int)postiMassimi - numeroSpettacoliFiltratiConPrenotazioni;

			if (postiRimanentiZ < 1)
			{
				postiRimanenti = 0;
				return null;
			}

			postiRimanenti = (uint)postiRimanentiZ;

			List<Spettacolo> spettacoliDisponibili;

			if (prenotazioni is null || prenotazioni.Count < 1)
			{
				postiRimanenti = postiMassimi;
				return spettacoli;
			}
			else
			{
				var spettacoliFiltratiSenzaPrenotazioni =
					from spettacolo in spettacoli
					where
						!prenotazioni.Any(p => p.IdSpettacolo == spettacolo.Id) &&
						spettacolo.Titolo.ToLower().Trim().Contains(titolo.ToLower().Trim()) &&
						spettacolo.DataEOra == dataEOraInizio &&
						dataEOraInizio >= DateTime.Now

					select spettacolo;

				spettacoliDisponibili = spettacoliFiltratiSenzaPrenotazioni.Union(spettacoliFiltratiConPrenotazioni).ToList();
			}


			if (spettacoliDisponibili.Count < 1)
			{
				postiRimanenti = 0;
				return null;
			}
			else
			{
				return spettacoliDisponibili;
			}
		}
		public bool AddFirstAvailable(uint postiMassimi, out uint postiRimanenti, string titolo, DateTime dataEOraInizio, string posto, uint? idCliente = null, string? nome = null, string? cognome = null, string? email = null, string? telefono = null)
		{
			List<Spettacolo>? spettacoliDisponibili = GetAvailable(postiMassimi, titolo, dataEOraInizio, out postiRimanenti);
			if (spettacoliDisponibili is null || spettacoliDisponibili.Count < 1)
			{
				postiRimanenti = 0;
				return false;
			}
			Spettacolo spettacolo = spettacoliDisponibili.First();
			uint idSpettacolo = spettacolo.Id;
			
			decimal prezzo = spettacolo.PrezzoBase;

			List<Cliente> clienti = _clienteService.Get();
			if (clienti.Count < 1)
			{
				return false;
			}
			List<uint> idClienti = clienti.Select(clienti => clienti.Id).ToList();

			if (idCliente is null)
			{
				if (nome is null || cognome is null) return false;
				uint idClienteDaAggiungere = _clienteService.GetNextId();
				Cliente clienteDaAggiungere = new(idClienteDaAggiungere, nome, cognome, email, telefono);
				if (_clienteService.Add(clienteDaAggiungere))
				{
					idCliente = idClienteDaAggiungere;
				}
				else
				{
					return false;
				}
			}
			else if (!idClienti.Contains((uint)idCliente))
			{
				return false;
			}

			uint id = GetNextId();

			Prenotazione prenotazioneDaAggiungere = new(id, idCliente.Value, idSpettacolo, posto, prezzo, DateTime.Now);

			if (Add(prenotazioneDaAggiungere))
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		public bool Add(Prenotazione prenotazione)
		{
			return _prenotazioneStore.Add(prenotazione);
		}
		public bool Delete(uint id)
		{
			return _prenotazioneStore.Delete(id);
		}
		public List<Prenotazione> Get()
		{
			return _prenotazioneStore.Get();
		}
		public Prenotazione? Get(uint id)
		{
			return _prenotazioneStore.Get(id);
		}
		public uint GetNextId()
		{
			List<Prenotazione> prenotazioni = Get();
			if (prenotazioni.Count == 0)
			{
				return 1;
			}
			else
			{
				return prenotazioni.Last().Id + 1;
			}
		}
		public List<Prenotazione>? Search(uint? idSpettacolo, uint? idCliente, string? posto, decimal? prezzo, DateTime? dataEOraPrenotazione)
		{
			List<Prenotazione> prenotazioni = _prenotazioneStore.Get();

			List<Prenotazione>? prenotazioniTrovate = prenotazioni
				.Where(p =>
					(idCliente is not null ? p.IdCliente == idCliente : true) &&
					(idSpettacolo is not null ? p.IdSpettacolo == idSpettacolo : true) &&
					(posto is not null ? p.Posto == posto : true) &&
					(prezzo is not null ? p.Prezzo == prezzo : true) &&
					(dataEOraPrenotazione is not null ? p.DataEOraPrenotazione == dataEOraPrenotazione : true))
				?.ToList();

			return prenotazioniTrovate;
		}
		public bool Update(Prenotazione prenotazione)
		{
			return _prenotazioneStore.Update(prenotazione);
		}
		public bool Update(uint id, uint? idSpettacolo, uint? idCliente, string? posto, decimal? prezzo, DateTime? dataEOraPrenotazione)
		{
			Prenotazione? prenotazione = _prenotazioneStore.Get(id);
			if (prenotazione is not null)
			{
				if (idSpettacolo is not null) prenotazione.IdSpettacolo = idSpettacolo.Value;
				if (idCliente is not null) prenotazione.IdCliente = idCliente.Value;
				if (posto is not null) prenotazione.Posto = posto;
				if (prezzo is not null) prenotazione.Prezzo = prezzo.Value;
				if (dataEOraPrenotazione is not null) prenotazione.DataEOraPrenotazione = dataEOraPrenotazione.Value;

				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
