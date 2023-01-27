using AppointmentSchedular.Data.Context;
using AppointmentSchedular.Entity.DTOs.Users;
using AppointmentSchedular.Entity.Entities;
using AppointmentSchedular.Service.Abstractions;
using AppointmentSchedular.Service.Concretes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Security.Claims;
using static Org.BouncyCastle.Math.EC.ECCurve;
using System.Security.Policy;
using FluentAssertions.Equivalency;

namespace AppointmentSchedular.MVC.Controllers
{
    
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IMailService mailService;
        private readonly IAccountService accountService;
        private readonly IConfiguration config;
        
        public AuthController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,IMailService mailService,IAccountService accountService,IConfiguration config)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.mailService = mailService;
            this.accountService = accountService;
            this.config = config;
            
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if(ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(userLoginDto.EMail);
               // var emailConfirmed = await userManager.IsEmailConfirmedAsync(user);
                if(user != null) 
                {
                    var result = await signInManager.PasswordSignInAsync(user, userLoginDto.Password, userLoginDto.RememberMe, false);
                    

                    if (!await userManager.IsEmailConfirmedAsync(user))
                    {
                        ModelState.AddModelError("", "Your account is not approved, please check your email account to confirm your account.");
                                                                    
                    }
                    
                    if (result.Succeeded)
                    {

                        return RedirectToAction("Index","Home", new {Area = "Home"});
                    }
                    else
                    {
                        ModelState.AddModelError("", "our email address or password is incorrect");
                        return View();
                    }
                    
                }
                else
                {
                    ModelState.AddModelError("", "our email address or password is incorrect");
                    return View();
                }
            }
            else
            {
                return View();
            }
            
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Auth",new {Area = ""});
        }

        public IActionResult ChangePasswordAsync()
        {
            return View();

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ResetPasswordDto resetPasswordDto)
        {
            if (ModelState.IsValid)
            {
                if(resetPasswordDto.NewPassword == resetPasswordDto.ConfirmNewPassword) 
                { 
                
                var result = await accountService.ChangePasswordAsync(resetPasswordDto);
                if (result.Succeeded)
                {
                    //ViewBag.IsSuccess = true;
                    ModelState.Clear();
                    TempData["PasswordChanged"] = "Password changed successfully";
                    return View();

                }

                foreach (var error in result.Errors)
                {
                    //ModelState.AddModelError("", error.Description);
                    TempData["PasswordChangeError"] =error.Description;
                }

                }
                else
                {
                    TempData["PasswordChangeError"] = "The new password fields do not match";
                    return View();
                }
            }

            return View(resetPasswordDto);
        }
        [HttpGet]
        public async Task<IActionResult> ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            if(ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(forgotPasswordDto.Email);
                if(user == null) 
                {
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }
                else
                {
                    var token=await userManager.GeneratePasswordResetTokenAsync(user);
                    var url = Url.Action("CreateNewPassword","Auth",
                         new
                         {
                             token,email=user.Email
                         },Request.Scheme);
                    await mailService.SendMessageAsync(user.Email, "Password reset link: ", $"<a href='{url}'>Please click the link to reset your password..</a>");
                    return RedirectToAction(nameof(ForgotPasswordConfirmation));
                }

                
            }
            return View(forgotPasswordDto);
        }
        public IActionResult ForgotPasswordConfirmation()
        {

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> CreateNewPassword(string token,string email)
        {
            if ((token == null || email == null)) return RedirectToAction("InvalidRequest", "Home");

            
                var model = new CreateNewPasswordDto { Token = token, Email = email };
                return View(model);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNewPassword(CreateNewPasswordDto createNewPasswordDto)
        {
            if (!ModelState.IsValid)
                return View(createNewPasswordDto);
            var user = await userManager.FindByEmailAsync(createNewPasswordDto.Email);
            if(user == null) { RedirectToAction(nameof(CreateNewPasswordConfirmation)); }
            else { 
            var resetPassResult = await userManager.ResetPasswordAsync(user, createNewPasswordDto.Token, createNewPasswordDto.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View();
            }
            }
            return RedirectToAction(nameof(CreateNewPasswordConfirmation));
        }
        [HttpGet]
        public IActionResult CreateNewPasswordConfirmation()
        {
            return View();
        }







    }
}
