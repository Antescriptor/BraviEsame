using DAL.Models;
using DAL.Stores.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Stores.Persistent
{
	public class PrenotazionePersistentStore(GestionaleDbContext dbContext) : IStore<Prenotazione>
	{
		private readonly GestionaleDbContext _dbContext = dbContext;
		public bool Add(Prenotazione prenotazioni)
		{
			_dbContext.Prenotazioni.Add(prenotazioni);
			_dbContext.SaveChanges();
			return true;
		}

		public bool Delete(uint id)
		{
			Prenotazione? prenotazione = Get(id);
			if (prenotazione is not null)
			{
				_dbContext.Prenotazioni.Remove(prenotazione);
				_dbContext.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
		}

		public List<Prenotazione> Get()
		{
			return _dbContext.Prenotazioni.ToList();
		}

		public Prenotazione? Get(uint id)
		{
			return _dbContext.Prenotazioni.FirstOrDefault(p => p.Id == id);
		}

		public bool Update(Prenotazione prenotazione)
		{
			Prenotazione? prenotazioneDaAggiornare = _dbContext.Prenotazioni.FirstOrDefault(p => p.Id == prenotazione.Id);
			if (prenotazioneDaAggiornare is not null)
			{
				prenotazioneDaAggiornare.IdCliente = prenotazione.IdCliente;
				prenotazioneDaAggiornare.IdSpettacolo = prenotazione.IdSpettacolo;
				prenotazioneDaAggiornare.Posto = prenotazione.Posto;
				prenotazioneDaAggiornare.Prezzo = prenotazione.Prezzo;
				prenotazioneDaAggiornare.DataEOraPrenotazione = prenotazione.DataEOraPrenotazione;

				_dbContext.Prenotazioni.Update(prenotazioneDaAggiornare);
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
