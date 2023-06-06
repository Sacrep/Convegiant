using Convegiant.Domain.Options;
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

	public static IServiceCollection AddRavenDbDatabase(this IServiceCollection services, RavenDbOptions options)
	{
		var nodeUrls = options.NodeEndpoints.Replace("localhost", LocalIPAddress);

		services.AddSingleton(sp =>
		{
			var store = new DocumentStore()
			{
				Urls = nodeUrls.Split(';'),
				Database = options.DatabaseName ?? DefaultDatabaseName,
				Certificate = string.IsNullOrEmpty(options.CertificateThumbprint) ? null : GetCertificateFromThumbprint(options.CertificateThumbprint),
				Conventions =
				{
					MaxNumberOfRequestsPerSession = 10,
					UseOptimisticConcurrency = true,
					IdentityPartsSeparator = '_'
				}
			}.Initialize();

			// Create DB indexes
			new Recipe_ListView().Execute(store);

			return store;
		});

		return services;
	}

	private static X509Certificate2 GetCertificateFromThumbprint(string thumbprint)
	{
		using X509Store certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
		certStore.Open(OpenFlags.ReadOnly);
		Console.WriteLine("Certificate issuers:" + string.Join(", ", certStore.Certificates.Select(x => x.IssuerName)));

		var validOnly = false;
		var certCollection = certStore.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, validOnly);

		var cert = certCollection.OfType<X509Certificate2>().FirstOrDefault() ??
			throw new Exception($"Certificate with thumbprint {thumbprint} was not found");

		Console.WriteLine(cert.IssuerName);
		return cert;
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
