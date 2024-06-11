using Microsoft.Extensions.DependencyInjection;
using CA.Controllers;
using Microsoft.Extensions.Configuration;
using DAL.Stores.Interface;
using DAL.Stores;
using DAL.Models;
using BLL.Services;
using DAL;
using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
using System;
using DAL.Stores.Persistent;

namespace CA
{
	internal class Program
	{
		static void Main(string[] args)
		{

			IConfiguration config = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.Build();

			bool usePersistentStore = config.GetValue<bool>("UsePersistentStore");

			ServiceCollection serviceCollection = new();

			if (usePersistentStore)
			{
				serviceCollection.AddScoped<IStore<Spettacolo>, SpettacoloPersistentStore>();
				serviceCollection.AddScoped<IStore<Cliente>, ClientePersistentStore>();
				serviceCollection.AddScoped<IStore<Prenotazione>, PrenotazionePersistentStore>();
			}
			else
			{
				serviceCollection.AddScoped<IStore<Spettacolo>, SpettacoloStore>();
				serviceCollection.AddScoped<IStore<Cliente>, ClienteStore>();
				serviceCollection.AddScoped<IStore<Prenotazione>, PrenotazioneStore>();
			}

			serviceCollection.AddScoped<SpettacoloService>();
			serviceCollection.AddScoped<ClienteService>();
			serviceCollection.AddScoped<PrenotazioneService>();

			serviceCollection.AddScoped<SpettacoloController>();
			serviceCollection.AddScoped<ClienteController>();
			serviceCollection.AddScoped<PrenotazioneController>();
			serviceCollection.AddScoped<GestionaleController>();

			// Stringa per la connessione al database e versione del server
			var connectionString = config.GetConnectionString("nexus");
			var serverVersion = new MySqlServerVersion(new Version(8, 0, 37));

			// Aggiunta del contesto del database come dipendenza
			serviceCollection.AddDbContext<GestionaleDbContext>(
				dbContextOptions => dbContextOptions
				.UseMySql(connectionString, serverVersion)
				// The following three options help with debugging, but should
				// be changed or removed for production.
/*				.LogTo(Console.WriteLine, LogLevel.Information)
				.EnableSensitiveDataLogging()
				.EnableDetailedErrors()*/
				);

			ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

			GestionaleController? gestionaleController = serviceProvider.GetService<GestionaleController>();

			gestionaleController?.Menu();

		}
	}
}