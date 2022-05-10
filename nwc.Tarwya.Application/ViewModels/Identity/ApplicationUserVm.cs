using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nwc.Tarwya.Application.ViewModels.Identity
{
    public class ApplicationUserVm
    {
		public long Id { get; set; }
		public string UserName { get; set; }
		public string FullName { get; set; }
		public string Email { get; set; }
		public int? CbuId { get; set; }
		public int? BranchId { get; set; }
		public string PlainPassword { get; set; }
		public int UserType { get; set; }
		public List<int> Permissions { get; set; }
		public List<int> Roles { get; set; }
		public List<string> AllowedFaTypes { get; set; }
	}
}
