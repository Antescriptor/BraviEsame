using System;
using System.Globalization;

namespace CA.Utils
{
	internal static class ImmissioneUtility
	{
		public static uint? NumeroNaturale(string nomeVariabile = "numero naturale")
		{
			string? input;
			bool verificaNumeroNaturale;
			uint numeroNaturale;

			bool verificaIdNullo;
			do
			{
				do
				{
					verificaIdNullo = false;
					Console.WriteLine($"Immettere {nomeVariabile}:");
					input = Console.ReadLine();
				
					if (nomeVariabile.ToLower().Trim() != "id" && string.IsNullOrEmpty(input))
					{
						return null;
					}
					else if (string.IsNullOrEmpty(input))
					{
						verificaIdNullo = true;
					}
				}
				while (verificaIdNullo);

				verificaNumeroNaturale = uint.TryParse(input, out numeroNaturale);
			}
			while (!verificaNumeroNaturale);

			return numeroNaturale;
		}
		public static decimal? NumeroRazionale(string nomeVariabile = "numero razionale")
		{
			string? input;
			bool verificaNumeroRazionale;
			decimal numeroRazionale;

			do
			{
				Console.WriteLine($"Immettere {nomeVariabile}:");
				input = Console.ReadLine();
				if (string.IsNullOrEmpty(input))
				{
					return null;
				}

				verificaNumeroRazionale = decimal.TryParse(input, out numeroRazionale);
			}
			while (!verificaNumeroRazionale);

			return numeroRazionale;
		}
		public static string? Stringa(string nomeVariabile = "stringa")
		{
			Console.WriteLine($"Immettere {nomeVariabile}:");
			string? input = Console.ReadLine();
			if (string.IsNullOrEmpty(input)) return null;

			return input;
		}
		public static DateTime? DataOppureDataEOra(uint dataOppureDataEOra)
		{
			//1 = data; altro numero = data e ora

			string? input;
			bool verificaDataEOra;
			CultureInfo localizzazione = new("it-IT");
			DateTime dataEOra;

			do
			{
				Console.WriteLine((dataOppureDataEOra == 0) ? "Immettere data nel formato \"gg/MM/aaaa\":" : "Immettere data e ora nel formato \"gg/MM/aaaa hh:mm\":");
				input = Console.ReadLine();

				if (string.IsNullOrEmpty(input))
				{
					return null;
				}

				verificaDataEOra = DateTime.TryParse(input, localizzazione, out dataEOra);
			}
			while (!verificaDataEOra);

			return dataEOra;
		}
	}
}