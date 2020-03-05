namespace nwc.Tarwya.Application.ViewModels
{
	public class UserVm
	{
		public virtual string Email { get; set; }

		public string PhoneNumber { get; set; }
		public long Id { get; set; }
		public string UserName { get; set; }
		public string Role { get; set; }
		public long? RoleId { get; set; }
	}
}
