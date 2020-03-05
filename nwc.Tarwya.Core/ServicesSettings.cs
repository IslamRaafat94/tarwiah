namespace nwc.Tarwya.Infra.Core
{
	public class ServicesSettings
	{
		public IntegrationSetting CCB_WO_Creation { get; set; }
		public IntegrationSetting CCB_WO_Inquery { get; set; }
		public IntegrationSetting ECM_Upload { get; set; }
	}

	public class IntegrationSetting
	{
		public string Url { get; set; }
		public bool RequierAuthintication { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
	}
}
