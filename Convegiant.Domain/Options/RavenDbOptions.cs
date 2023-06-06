namespace Convegiant.Domain.Options;

public class RavenDbOptions
{
	public const string ConfigSection = "RavenDB";

	public string? DatabaseName { get; set; }

	public string? CertificateThumbprint { get; set; }

	/// <summary>
	/// List of RavenDB database cluster node urls. Separate multiple endpoints with ;
	/// </summary>
	public string NodeEndpoints { get; set; } = "";
}
