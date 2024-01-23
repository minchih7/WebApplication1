using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ApiController : Controller
    {
        private readonly MyDBContext _dbContext;
        public IActionResult Index()
        {
            System.Threading.Thread.Sleep(5000);
            //return Content("<h1>15FF<h1>","text/html");
            return Content("<h1>15FF您好<h1>", "text/html",System.Text.Encoding.UTF8);
            

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

        public IActionResult Avatar(int id=1) 
        {
            Member? member = _dbContext.Members.Find(id);
            if(member!=null)
            {
                byte[] img = member.FileData;
                return File(img, "image/jpeg");

            }
            return NotFound();
        }

        public IActionResult Register(string name,int age = 26)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = "Guest";
            }
            return Content($"hello{name},age=>{age}");
        }

    }
}
