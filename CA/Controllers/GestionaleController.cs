using System;

namespace CA.Controllers
{
	internal class GestionaleController(SpettacoloController spettacoloController, ClienteController clienteController, PrenotazioneController prenotazioneController)
	{
		private readonly SpettacoloController _spettacoloController = spettacoloController;
		private readonly ClienteController _clienteController = clienteController;
		private readonly PrenotazioneController _prenotazioneController = prenotazioneController;

		public void Menu()
		{
			string input;
			bool verificaNumeroNaturale;
			uint scelta;

			do
			{
				Console.WriteLine("\nMenu gestionale\n\n1. Menu prenotazioni\n2. Menu clienti\n3. Menu spettacoli\n0. Esci\n");
				input = Console.ReadLine() ?? "";
				verificaNumeroNaturale = uint.TryParse(input, out scelta);
				if (!verificaNumeroNaturale) scelta = uint.MaxValue;

				switch (scelta)
				{
					case 1:
						_prenotazioneController.Menu();
						break;
					case 2:
						_clienteController.Menu();
						break;
					case 3:
						_spettacoloController.Menu();
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
