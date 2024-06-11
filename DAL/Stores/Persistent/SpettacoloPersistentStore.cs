using DAL.Stores.Interface;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;

namespace DAL.Stores.Persistent
{
	public class SpettacoloPersistentStore(GestionaleDbContext dbContext) : IStore<Spettacolo>
	{
		private readonly GestionaleDbContext _dbContext = dbContext;
		public bool Add(Spettacolo spettacolo)
		{
			_dbContext.Spettacoli.Add(spettacolo);
			_dbContext.SaveChanges();
			return true;
		}

		public bool Delete(uint id)
		{
			Spettacolo? spettacolo = Get(id);
			if (spettacolo is not null)
			{
				_dbContext.Spettacoli.Remove(spettacolo);
				_dbContext.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
		}

		public List<Spettacolo> Get()
		{
			return _dbContext.Spettacoli.ToList();
		}

		public Spettacolo? Get(uint id)
		{
			return _dbContext.Spettacoli.FirstOrDefault(t => t.Id == id);
		}

		public bool Update(Spettacolo spettacolo)
		{
			Spettacolo? spettacoloDaAggiornare = _dbContext.Spettacoli.FirstOrDefault(t => t.Id == spettacolo.Id);
			if (spettacoloDaAggiornare is not null)
			{
				spettacoloDaAggiornare.Titolo = spettacolo.Titolo;
				spettacoloDaAggiornare.Descrizione = spettacolo.Descrizione;
				spettacoloDaAggiornare.DataEOra = spettacolo.DataEOra;
				spettacoloDaAggiornare.Durata = spettacolo.Durata;
				spettacoloDaAggiornare.PrezzoBase = spettacolo.PrezzoBase;

				_dbContext.Spettacoli.Update(spettacoloDaAggiornare);
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