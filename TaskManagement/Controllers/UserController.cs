using Microsoft.AspNetCore.Mvc;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Controllers;

public class UserController : Controller
{
    private readonly TaskDbContext _dbContext;

    public UserController(TaskDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public IActionResult Index()
    {
        var users = _dbContext.Users.ToList();
        return View(users);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(User user)
    {
        if (ModelState.IsValid)
        {
            _dbContext.Users.Add(user);
            if (_dbContext.SaveChanges() > 0)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Failed to create user. Please try again.");
        }
        return View(user);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var user = _dbContext.Users.Find(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    [HttpPost]
    public IActionResult Edit(User user)
    {
        if (ModelState.IsValid)
        {
            _dbContext.Users.Update(user);
            if (_dbContext.SaveChanges() > 0)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Failed to update user. Please try again.");
        }
        return View(user);
    }

}
