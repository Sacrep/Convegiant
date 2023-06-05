using Convegiant.Infrastructure.RavenDB;
using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;

namespace Convegiant.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
	private const string DefaultDatabaseName = "Convegiant";

	public static IServiceCollection AddRavenDbDatabase(this IServiceCollection services, string[] nodeUrls, string certificatePath, string databaseName = DefaultDatabaseName)
	{
		nodeUrls = nodeUrls.Select(x => x.Replace("localhost", LocalIPAddress)).ToArray();

		services.AddSingleton(sp =>
		{
			IDocumentStore store = new DocumentStore();

			if (nodeUrls.Length > 0)
			{
				store = new DocumentStore()
				{
					Urls = nodeUrls,
					Database = databaseName,
					Certificate = string.IsNullOrEmpty(certificatePath) ? null : new X509Certificate2(certificatePath),

					Conventions =
				{
					MaxNumberOfRequestsPerSession = 10,
					UseOptimisticConcurrency = true,
					IdentityPartsSeparator = '_'
				}
				}.Initialize();

				// Create DB indexes
				new Recipe_ListView().Execute(store);
			}

			return store;
		});

		return services;
	}

	private static string LocalIPAddress
	{
		get
		{
			var host = Dns.GetHostEntry(Dns.GetHostName());
			foreach (var ip in host.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork)
				{
					return ip.ToString();
				}
			}
			throw new Exception("No network adapters with an IPv4 address in the system!");
		}
	}
}
