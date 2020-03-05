using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Application.ViewModels;
using nwc.Tarwya.Application.ViewModels.Identity;
using nwc.Tarwya.Application.ViewModels.Shared;
using nwc.Tarwya.Domain.Models.Models;
using nwc.Tarwya.Domain.Repositories;
using nwc.Tarwya.Infra.Core;
using nwc.Tarwya.Infra.Identity.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nwc.Tarwya.Application.Services
{
	public class IdentityService : ServiceBase, IIdentityService
	{
		private readonly ApplicationUserManager usermanager;
		private readonly ApplicationRoleManager rolemanager;
		private readonly IRepository<User> usersrepository;
		private readonly IRepository<Role> rolepository;
		public IdentityService(
			IOptions<SystemSettings> settings,
			IMapper mapper,
			ApplicationUserManager _usermanager,
			ApplicationRoleManager _rolemanager,
			IRepository<User> _usersrepository,
			IRepository<Role> _rolerepository
			)
			: base(settings, mapper)
		{
			this.usermanager = _usermanager;
			this.rolemanager = _rolemanager;
			this.usersrepository = _usersrepository;
			this.rolepository = _rolerepository;
		}

		public async Task<bool> CreateUser(UserEditableVm model)
		{
			bool isFound = usersrepository.Get()
				.Any(i => i.UserName == model.UserName.Trim() || i.Email == model.Email.Trim());

			if (isFound)
				throw new Exception("UserFound! username/Email must be Unique");

			await usermanager.CreateAsync(new IdentityUser<long>()
			{
				UserName = model.UserName.Trim(),
				Email = model.Email.Trim(),
				PhoneNumber = model.PhoneNumber.Trim(),

			});

			var user = await usermanager.FindByNameAsync(model.UserName.Trim());
			var role = await rolemanager.FindByIdAsync(model.RoleId.Value.ToString());
			await usermanager.AddToRoleAsync(user, role.Name);
			return true;

		}

		public IQueryable<UserVm> GetIdentityUsers()
		{
			return usersrepository.Get()
				.ProjectTo<UserVm>(mapper.ConfigurationProvider);
		}
		public async Task<List<LookUpVm>> GetRolesLookUp()
		{
			return await rolepository.Get()
				.ProjectTo<LookUpVm>(mapper.ConfigurationProvider)
				.ToListAsync();
		}
	}
}
