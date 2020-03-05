using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace nwc.Tarwya.Infra.Identity.Managers
{
	public class ApplicationUserManager : UserManager<IdentityUser<long>>
	{
		public ApplicationUserManager(IUserStore<IdentityUser<long>> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<IdentityUser<long>> passwordHasher, IEnumerable<IUserValidator<IdentityUser<long>>> userValidators, IEnumerable<IPasswordValidator<IdentityUser<long>>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<IdentityUser<long>>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
		{
		}
	}
}
