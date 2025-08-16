using Microsoft.AspNetCore.Mvc;
using TaskManagement.Data;
using TaskManagement.Models;

namespace TaskManagement.Controllers;

public class TaskAssignedController : Controller
{
    private readonly TaskDbContext _dbContext;

    public TaskAssignedController(TaskDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {
        var assignedTasks = _dbContext.AssignedTasks.ToList();

        return View(assignedTasks);
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(AssignedTask assignedTask)
    {
        if (ModelState.IsValid)
        {
            _dbContext.AssignedTasks.Add(assignedTask);
            if (_dbContext.SaveChanges() > 0)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Failed to create assigned task. Please try again.");

        }
        return View(assignedTask);
    }

}
