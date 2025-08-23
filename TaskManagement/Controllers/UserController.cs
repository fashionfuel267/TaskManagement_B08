using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Controllers;
[Authorize(Roles = "Admin,Super Admin,Employee")]
public class UserController : Controller
{
    private readonly TaskDbContext _dbContext;
    private UserManager<IdentityUser> _userManager;
    public UserController(TaskDbContext dbContext, UserManager<IdentityUser>userManager)
    {
        _dbContext = dbContext;
        _userManager=userManager;
    }
    public IActionResult Index()
    {
        if(User.IsInRole("Employee"))
        {
            var employees = _dbContext.Employees.Where(e=>e.Email.Equals(User.Identity.Name)).ToList();
            return View(employees);
        }
        var users = _dbContext.Employees.ToList();
        return View(users);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Employee user)
    {
        if (ModelState.IsValid)
        {
            _dbContext.Employees.Add(user);
            if (_dbContext.SaveChanges() > 0)
            {
                string password =$"{user.Name.ToUpper().Substring(0,3)}*566#p";
                var identityUser = new IdentityUser
                {
                    UserName = user.Email,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNo
                };
           var result=    await _userManager.CreateAsync(identityUser, password);
                if(result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(identityUser, "Employee");
                }
                else
                {
                    
                    string msg = "";
                    foreach (var error in result.Errors)
                    {
                        msg += $"{error.Code} - {error.Description} \n";
                    }
                    //ViewBag.Msg = msg;
                    ModelState.AddModelError("", msg);
                    return View(user);
                }
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Failed to create user. Please try again.");
        }
        else
        {
            var message = string.Join(" | ", ModelState.Values
   .SelectMany(v => v.Errors)
   .Select(e => e.ErrorMessage));
            ModelState.AddModelError(" ", message);
        }
        return View(user);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var user = _dbContext.Employees.Find(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    [HttpPost]
    public IActionResult Edit(Employee user)
    {
        if (ModelState.IsValid)
        {
            _dbContext.Employees.Update(user);
            if (_dbContext.SaveChanges() > 0)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Failed to update user. Please try again.");
        }
        return View(user);
    }

}
