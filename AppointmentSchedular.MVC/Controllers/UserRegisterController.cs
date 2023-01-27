using AppointmentSchedular.Entity.DTOs.Users;
using AppointmentSchedular.Entity.Entities;
using AppointmentSchedular.Entity.DTOs;
using AppointmentSchedular.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace AppointmentSchedular.MVC.Controllers
{
    [AllowAnonymous]
    public class UserRegisterController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IMailService mailService;
        private readonly IConfiguration config;

        public UserRegisterController(UserManager<AppUser> userManager,IMailService mailService,IConfiguration config)
        {
            this.userManager = userManager;
            this.mailService = mailService;
            this.config = config;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(UserRegisterDto userRegisterDto)
        {
            if(ModelState.IsValid)
            {
                var user = new AppUser
                {

                    Email = userRegisterDto.Email,
                    NormalizedEmail = userRegisterDto.Email,                    
                    FirstName= userRegisterDto.FirstName,
                    LastName= userRegisterDto.LastName,
                    PhoneNumber= userRegisterDto.PhoneNumber,
                    UserName=userRegisterDto.Email,
                    NormalizedUserName = userRegisterDto.Email,


                };
                
                var result=await userManager.CreateAsync(user,userRegisterDto.Password);

                if(result.Succeeded)
                {
                    //generate token
                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var url = Url.Action("ConfirmEmail", "UserRegister", new
                    {
                        userId = user.Id,
                        token = code
                    });

                    //send email
                    await mailService.SendMessageAsync(user.Email, "Email Confirm for AppointmentScheduler", $"<a href='{config.GetSection("SiteLink").Value}{url}'>Please click the link to confirm your email account.</a>");


                    return RedirectToAction("Login", "Auth");

                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
                
            }

            return View(userRegisterDto);
           
        }
        //[HttpGet]
        //public async Task<IActionResult> ExampleMailTest()
        //{
        //    await mailService.SendMessageAsync("r10redius@gmail.com", "Örnek Mail","Deneme Mail");
        //    return Ok();

        //}
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if(userId== null || token==null) { TempData["message"] = "Geçersiz Token"; return View(); }
            var user=await userManager.FindByIdAsync(userId); 
            if(user==null)
            {
                TempData["message"] = "There is no such user.";
            }
            var result = await userManager.ConfirmEmailAsync(user, token);
            if(result.Succeeded)
            {
                TempData["message"] = "Your account has been confirmed.";
            }
            return View();
        }
        



    }
}


