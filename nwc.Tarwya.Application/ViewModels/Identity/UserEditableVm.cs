using System.ComponentModel.DataAnnotations;

namespace nwc.Tarwya.Application.ViewModels.Identity
{
	public class UserEditableVm
	{
		[Required]
		public virtual string Email { get; set; }
		[Required]

		public string PhoneNumber { get; set; }
		[Required]

		public long Id { get; set; }
		[Required]

		public string UserName { get; set; }
		[Required]

		public long? RoleId { get; set; }
	}
}
