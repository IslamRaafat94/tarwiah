using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nwc.Logger;
using nwc.Tarwya.Application.Services.Contracts;
using nwc.Tarwya.Application.ViewModels.Identity;
using nwc.Tarwya.Infra.Identity.Managers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace nwc.Tarwya.Portal.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IIdentityService identityService;
        private readonly ApplicationUserManager applicationUserManager;
        public AccountController(
            ApplicationUserManager _applicationUserManager,
            IIdentityService _identityService
            )
        {
            this.applicationUserManager = _applicationUserManager;
            this.identityService = _identityService;
        }
        public IActionResult Users()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Login(Uri ReturnUrl)
        {
            TempData["ReturnUrl"] = ReturnUrl;
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm model, Uri ReturnUrl)
        {
            try
            {
                var user = await applicationUserManager.FindByEmailAsync(model?.UserName).ConfigureAwait(false)
                       ?? await applicationUserManager.FindByNameAsync(model.UserName).ConfigureAwait(false);

                if (user != null)
                {
                    var passwordIsCorrect = await applicationUserManager.CheckPasswordAsync(user, model.Password).ConfigureAwait(false);
                    if (passwordIsCorrect)
                    {
                        var customClaims = new List<Claim>
                            {
                                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(CultureInfo.CurrentCulture), ClaimValueTypes.String),
                                new Claim(ClaimTypes.Name, user.UserName.ToString(CultureInfo.CurrentCulture), ClaimValueTypes.String),
                                new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.String),
                            };

                        var userRoles = await applicationUserManager.GetRolesAsync(user).ConfigureAwait(false);
                        foreach (var role in userRoles)
                            customClaims.Add(new Claim(ClaimTypes.Role, role, ClaimValueTypes.String));


                        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                        identity.AddClaims(customClaims);
                        var principal = new ClaimsPrincipal(identity);
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = true }).ConfigureAwait(false);

                        if (Url.IsLocalUrl(ReturnUrl?.OriginalString))
                            return Redirect(ReturnUrl.OriginalString);
                        return RedirectToAction("Index", "Home");
                    }

                    return RedirectToAction("Login", new
                    {
                        ReturnUrl = ReturnUrl
                    });

                }

                return View(model);
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                throw;
            }
        }
        public async Task<IActionResult> GetUsers([DataSourceRequest] DataSourceRequest request)
        {
            try
            {
                var list = identityService.GetIdentityUsers();
                return Json(await list.ToDataSourceResultAsync(request));
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                throw;
            }
        }
        public async Task<JsonResult> GetRoles()
        {
            try
            {
                var list = await identityService.GetRolesLookUp();
                return Json(list);
            }
            catch (Exception ex)
            {
                nwcLogger.Error(ex.Message, ex);
                throw;
            }
        }
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserEditableVm user)
        {
            try
            {
                bool result = await identityService.CreateUser(user);
                if (result)
                    return RedirectToAction(nameof(Users));
                return View(user);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(user);
            }

        }
    }
}