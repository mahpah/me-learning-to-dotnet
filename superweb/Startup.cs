using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using superweb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Formatters;
using superweb.Filters;
using superweb.Models.Postgres;

namespace superweb
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var configBuilder = new ConfigurationBuilder()
					.SetBasePath(env.ContentRootPath)
					.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
					.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
					.AddEnvironmentVariables();
			Configuration = configBuilder.Build();
		}

		public IConfigurationRoot Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<PostgresDatabaseContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("Postgres")));

			services.AddMvc(options =>
			{
				options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
				options.Filters.Add(new ExecuteTimeFilter());
			});
			services.AddScoped<ITodoRepository, TodoRepository>();
		}

		public void ConfigureDevelopmentServices(IServiceCollection services)
		{
			services.AddDbContext<PostgresDatabaseContext>(opt => opt.UseInMemoryDatabase());
			services.AddMvc(options =>
			{
				options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
				options.Filters.Add(new ExecuteTimeFilter());
			});
			services.AddScoped<ITodoRepository, TodoRepository>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
			{
                Authority = "http://localhost:5500",
                RequireHttpsMetadata = false,
                ApiName = "superweb",
			});

			app.UseMvc();
		}
	}
}
