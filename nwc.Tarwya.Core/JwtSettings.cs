namespace nwc.Tarwya.Infra.Core
{
	public class JwtSettings
	{
		public string JwtSecretKey { get; set; }
		public string JwtIssuer { get; set; }
		public string JwtAudience { get; set; }
	}
}
