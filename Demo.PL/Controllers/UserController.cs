using AutoMapper;
using Demo.DAL.Entites;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Demo.PL.Controllers
{
    [Authorize(Roles = "Admin,user")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index(string SearchValue)
        {
            var users = Enumerable.Empty<ApplicationUser>().ToList();

            if (string.IsNullOrEmpty(SearchValue))
            {
                ViewBag.search = null;
                users.AddRange(_userManager.Users);
            }
            else
            {
                var Result = await _userManager.FindByEmailAsync(SearchValue);
                if (Result != null)
                    users.Add(Result);
                else
                {
                    ViewBag.search = "There's No Users By That Email";
                    return View();
                }
            }
            return View(users);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel Addeduser)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<CreateUserViewModel, ApplicationUser>(Addeduser);
                var Result = await _userManager.CreateAsync(user, Addeduser.Password);

                if (Result.Succeeded)
                    return RedirectToAction(nameof(Index));
                foreach (var error in Result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(Addeduser);
        }



        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            if (id == null)
                return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            return View(ViewName, user);
        }

        public async Task<IActionResult> Edit(string Id)
        {
            return await Details(Id, "Edit");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string Id, ApplicationUser updatedUser)
        {
            if (Id != updatedUser.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(Id);
                    user.UserName = updatedUser.UserName;
                    user.PhoneNumber = updatedUser.PhoneNumber;
                    user.Email = updatedUser.Email;
                    user.SecurityStamp = updatedUser.SecurityStamp;

                    await _userManager.UpdateAsync(user);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    return View(updatedUser);
                }
            }
            return View(updatedUser);
        }


        public async Task<IActionResult> Delete(string Id)
        {
            return await Details(Id, "Delete");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string Id, ApplicationUser deletedUser)
        {
            if (Id != deletedUser.Id)
                return BadRequest();
            try
            {
                var user = await _userManager.FindByIdAsync(Id);

                await _userManager.DeleteAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(deletedUser);
            }
        }


    }
}
