using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using DAL.Stores.Interface;

namespace DAL.Stores
{
	public class SpettacoloStore : IStore<Spettacolo>
	{
		private readonly List<Spettacolo> _spettacoli = new();
		public bool Add(Spettacolo spettacolo)
		{
			_spettacoli.Add(spettacolo);
			return true;
		}

		public bool Delete(uint id)
		{
			Spettacolo? spettacolo = Get(id);
			if (spettacolo is not null)
			{
				_spettacoli.Remove(spettacolo);
				return true;
			}
			else
			{
				return false;
			}
		}

		public List<Spettacolo> Get()
		{
			return _spettacoli;
		}

		public Spettacolo? Get(uint id)
		{
			return _spettacoli.FirstOrDefault(t => t.Id == id);
		}

		public bool Update(Spettacolo spettacolo)
		{
			Spettacolo? spettacoloDaAggiornare = _spettacoli.FirstOrDefault(t => t.Id == spettacolo.Id);
			if (spettacoloDaAggiornare is not null)
			{
				spettacoloDaAggiornare.Titolo = spettacolo.Titolo;
				spettacoloDaAggiornare.Descrizione = spettacolo.Descrizione;
				spettacoloDaAggiornare.DataEOra = spettacolo.DataEOra;
				spettacoloDaAggiornare.Durata = spettacolo.Durata;
				spettacoloDaAggiornare.PrezzoBase = spettacolo.PrezzoBase;

				return true;
			}
			else
			{
				return false;
			}
		}
	}
}