//using Microsoft.AspNetCore.Mvc;

//namespace Desk_Booking.Controllers
//{
//    public class AccountController : Controller
//    {
//        public IActionResult Index()
//        {
//            return View();
//        }
//    }
//}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Desk_Booking.Data;
using Desk_Booking.Models;


namespace Desk_booking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        //these three lines are from previous commented code snippets
        public IActionResult Index()
        {
            return View();
        }



        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }


        //public async Task<IActionResult> Register(RegisterViewModel model)
        //{
        //if (ModelState.IsValid)
        //{
        //    var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        //    var result = await _userManager.CreateAsync(user, model.Password);

        //    if (result.Succeeded)
        //    {
        //        if (!await _roleManager.RoleExistsAsync("Admin"))
        //        {
        //            await _roleManager.CreateAsync(new IdentityRole("Admin"));
        //        }
        //        if (!await _roleManager.RoleExistsAsync("User"))
        //        {
        //            await _roleManager.CreateAsync(new IdentityRole("User"));
        //        }

        //        if (model.Email == "admin@example.com")
        //        {
        //            await _userManager.AddToRoleAsync(user, "Admin");
        //        }
        //        else
        //        {
        //            await _userManager.AddToRoleAsync(user, "User");
        //        }

        //       // await _signInManager.SignInAsync(user, isPersistent: false);
        //        return RedirectToAction("Login", "Account");
        //    }
        //    //return RedirectToAction("Login", "Account");

        //}

        //return View(model);
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync("Admin"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    }
                    if (!await _roleManager.RoleExistsAsync("User"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("User"));
                    }

                    if (model.Email == "admin@example.com")
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                    }

                    // Redirect to Login page after successful registration
                    //TempData["SuccessMessage"] = "Registration Sucessfull! Please log in.";

                    // Return JSON response indicating success
                    return Json(new { success = true, message = "Registration suceesfull! Redirecting to login page..." });


                    //return RedirectToAction("Login", "Account");
                }

                // Return JSON response indicating failure
                return Json(new { success = false, errors = result.Errors.Select(e => e.Description).ToArray() });

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            else
            {
                // Log or debug ModelState errors
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            return View(model);
        }



        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Login(LoginViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
        //        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        //        var result = await _userManager.CreateAsync(user, model.Password);

        //        if (result.Succeeded)
        //        {
        //            // Validate user details
        //            var user = await _userManager.FindByEmailAsync(model.Email);
        //            if (user != null)
        //            {
        //                // Redirect to home page to book a seat
        //                return RedirectToAction("Index", "Home");
        //            }
        //            else
        //            {
        //                ModelState.AddModelError(string.Empty, "User details validation failed.");
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        //        }


        //    }

        //    return View(model);
        //}



        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // Redirect to home page to book a seat
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            return View(model);
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
