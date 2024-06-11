using DAL.Stores.Interface;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using System;

namespace BLL.Services
{
	public class SpettacoloService(IStore<Spettacolo> spettacoloStore)
	{
		private readonly IStore<Spettacolo> _spettacoloStore = spettacoloStore;
		public bool Add(string titolo, string descrizione, DateTime dataEOra, uint durata, decimal prezzoBase)
		{
			uint id = GetNextId();
			Spettacolo spettacolo = new(id, titolo, descrizione, dataEOra, durata, prezzoBase);
			return Add(spettacolo);
		}
		public bool Add(Spettacolo spettacolo)
		{
			return _spettacoloStore.Add(spettacolo);
		}
		public bool Delete(uint id)
		{
			return _spettacoloStore.Delete(id);
		}
		public List<Spettacolo> Get()
		{
			return _spettacoloStore.Get();
		}
		public Spettacolo? Get(uint id)
		{
			return _spettacoloStore.Get(id);
		}
		public uint GetNextId()
		{
			List<Spettacolo> spettacolo = Get();
			if (spettacolo.Count == 0)
			{
				return 1;
			}
			else
			{
				return spettacolo.Last().Id + 1;
			}
		}
		public List<Spettacolo>? Search(string? titolo, string? descrizione, DateTime? dataEOra, uint? durata, decimal? prezzoBase)
		{
			List<Spettacolo> Spettacoli = _spettacoloStore.Get();

			List<Spettacolo>? clientiTrovati = Spettacoli
				.Where(t =>
					(titolo is not null ? t.Titolo == titolo : true) &&
					(descrizione is not null ? t.Descrizione == descrizione : true) &&
					(dataEOra is not null ? t.DataEOra == dataEOra : true) &&
					(durata is not null ? t.Durata == durata : true) &&
					(prezzoBase is not null ? t.PrezzoBase == prezzoBase : true))
				?.ToList();

			return clientiTrovati;
		}
		public bool Update(Spettacolo spettacolo)
		{
			return _spettacoloStore.Update(spettacolo);
		}
		public bool Update(uint id, string? titolo, string? descrizione, DateTime? dataEOra, uint? durata, decimal? prezzoBase)
		{
			Spettacolo? spettacolo = Get(id);
			if (spettacolo is not null)
			{
				if (titolo is not null) spettacolo.Titolo = titolo;
				if (descrizione is not null) spettacolo.Descrizione = descrizione;
				if (dataEOra is not null) spettacolo.DataEOra = dataEOra.Value;
				if (durata is not null) spettacolo.Durata = durata.Value;
				if (prezzoBase is not null) spettacolo.PrezzoBase = prezzoBase.Value;

				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
