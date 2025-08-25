using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.Controllers
{
    public class AuthenticUserController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }

    }
}
