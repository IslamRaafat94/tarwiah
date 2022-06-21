namespace nwc.Tarwya.Infra.Core
{
	public class SystemSettings
	{
		public string DataProtectionKeyPath { get; set; }
		public AppSettings appSettings { get; set; }
		public ServicesSettings IntegratedServices { get; set; }
	}
}
