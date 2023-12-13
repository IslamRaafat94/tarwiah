namespace nwc.Tarwya.Infra.Core
{
	public class SystemSettings
	{
		public bool EnableSwagger { get; set; }
		public string DataProtectionKeyPath { get; set; }
		public AppSettings appSettings { get; set; }
		public ServicesSettings IntegratedServices { get; set; }
	}
}
