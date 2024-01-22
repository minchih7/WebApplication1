using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ApiController : Controller
    {
        private readonly MyDBContext _dbContext;
        public IActionResult Index()
        {
            return View();
        }

        public ApiController(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Cities()
        {
            var cities = _dbContext.Addresses.Select(x => x.City).Distinct();
            return Json(cities);
        }
    }
}
