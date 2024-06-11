
using BLL.Services;
using DAL.Models;
using DAL.Stores.Interface;
using DAL.Stores.Persistent;
using DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using DAL.Stores;

namespace API
{
	/// <summary>
	/// Configurazione dell'applicazione
	/// </summary>
	public class Program
	{
		/// <summary>
		/// Punto d'ingresso dell'applicazione
		/// </summary>
		/// <param name="args"></param>
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo
				{
					Version = "v1",
					Title = "Gestionale biglietteria teatro"
				});
				// using System.Reflection;
				var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
			});

			builder.Services.AddScoped<ClienteService>();
			builder.Services.AddScoped<PrenotazioneService>();
			builder.Services.AddScoped<SpettacoloService>();

			// Selettore store persistente o in memoria in base alla configurazione
			bool usePersistentStore = builder.Configuration.GetValue<bool>("UsePersistentStore");
			if (usePersistentStore)
			{
				builder.Services.AddScoped<IStore<Cliente>, ClientePersistentStore>();
				builder.Services.AddScoped<IStore<Spettacolo>, SpettacoloPersistentStore>();
				builder.Services.AddScoped<IStore<Prenotazione>, PrenotazionePersistentStore>();
			}
			else
			{
				builder.Services.AddScoped<IStore<Spettacolo>, SpettacoloStore>();
				builder.Services.AddScoped<IStore<Cliente>, ClienteStore>();
				builder.Services.AddScoped<IStore<Prenotazione>, PrenotazioneStore>();
			}

			// Stringa per la connessione al database e versione del server
			var connectionString = builder.Configuration.GetConnectionString("nexus");
			var serverVersion = new MySqlServerVersion(new Version(8, 0, 37));

			// Aggiunta del contesto del database come dipendenza
			builder.Services.AddDbContext<GestionaleDbContext>(
				dbContextOptions => dbContextOptions
				.UseMySql(connectionString, serverVersion)
				// The following three options help with debugging, but should
				// be changed or removed for production.
				.LogTo(Console.WriteLine, LogLevel.Information)
				.EnableSensitiveDataLogging()
				.EnableDetailedErrors()
				);

			//Non è necessario aggiungere le dipendenze di controller poiché
			//sono già state aggiunte con AddControllers

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
