using DAL.Models;
using DAL.Stores.Interface;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Stores
{
	public class PrenotazioneStore : IStore<Prenotazione>
	{
		private readonly List<Prenotazione> _prenotazioni = new();
		public bool Add(Prenotazione prenotazioni)
		{
			_prenotazioni.Add(prenotazioni);
			return true;
		}

		public bool Delete(uint id)
		{
			Prenotazione? prenotazione = Get(id);
			if (prenotazione is not null)
			{
				_prenotazioni.Remove(prenotazione);
				return true;
			}
			else
			{
				return false;
			}
		}

		public List<Prenotazione> Get()
		{
			return _prenotazioni;
		}

		public Prenotazione? Get(uint id)
		{
			return _prenotazioni.FirstOrDefault(p => p.Id == id);
		}

		public bool Update(Prenotazione prenotazione)
		{
			Prenotazione? prenotazioneDaAggiornare = _prenotazioni.FirstOrDefault(p => p.Id == prenotazione.Id);
			if (prenotazioneDaAggiornare is not null)
			{
				prenotazioneDaAggiornare.IdCliente = prenotazione.IdCliente;
				prenotazioneDaAggiornare.IdSpettacolo = prenotazione.IdSpettacolo;
				prenotazioneDaAggiornare.Posto = prenotazione.Posto;
				prenotazioneDaAggiornare.Prezzo = prenotazione.Prezzo;
				prenotazioneDaAggiornare.DataEOraPrenotazione = prenotazione.DataEOraPrenotazione;
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
