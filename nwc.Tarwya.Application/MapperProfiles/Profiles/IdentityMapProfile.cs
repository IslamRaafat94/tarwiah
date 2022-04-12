using nwc.Tarwya.Application.ViewModels;
using nwc.Tarwya.Application.ViewModels.Shared;
using nwc.Tarwya.Domain.Models.Models;
using System.Linq;

namespace nwc.Tarwya.Application.MapperProfiles.Profiles
{
	public class IdentityMapProfile : BaseMappingProfile
	{
		public IdentityMapProfile()
		{
			CreateMap<User, UserVm>()
				.ForMember(i => i.Id, s => s.MapFrom(d => d.Id))
				.ForMember(i => i.UserName, s => s.MapFrom(d => d.UserName))
				.ForMember(i => i.Email, s => s.MapFrom(d => d.Email))
				.ForMember(i => i.PhoneNumber, s => s.MapFrom(d => d.PhoneNumber))
				.ForMember(i => i.Role, s => s.MapFrom(d => string.Join(",", d.Roles.Select(i => i.Name))))
				;

			CreateMap<Role, LookUpVm>()
				.ForMember(i => i.Id, s => s.MapFrom(d => (int)d.Id))
				.ForMember(i => i.Name, s => s.MapFrom(d => d.Name))
				;

		}
	}
}
