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
        [DataType(DataType.Password)]
        public string Password { get; set; }
		[Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]

		public long? RoleId { get; set; }
	}
}
