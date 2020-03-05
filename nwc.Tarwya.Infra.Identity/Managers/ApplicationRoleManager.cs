using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace nwc.Tarwya.Infra.Identity.Managers
{
	public class ApplicationRoleManager : RoleManager<IdentityRole<long>>
	{
		public ApplicationRoleManager(IRoleStore<IdentityRole<long>> store, IEnumerable<IRoleValidator<IdentityRole<long>>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<RoleManager<IdentityRole<long>>> logger) : base(store, roleValidators, keyNormalizer, errors, logger)
		{
		}
	}
}
