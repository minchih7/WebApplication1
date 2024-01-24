using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.DTO;

namespace WebApplication1.Controllers
{
    public class ApiController : Controller
    {
        private readonly MyDBContext _dbContext;
        private readonly IWebHostEnvironment _host
            ;

        public ApiController(MyDBContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _host = webHostEnvironment;
        }

        public IActionResult Index()
        {
            System.Threading.Thread.Sleep(5000);
            //return Content("<h1>15FF<h1>","text/html");
            return Content("<h1>15FF您好<h1>", "text/html",System.Text.Encoding.UTF8);
            

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

        //public IActionResult Register(string name,int age = 26)
        //{
        //    if (string.IsNullOrEmpty(name))
        //    {
        //        name = "Guest";
        //    }
        //    return Content($"hello{name},age=>{age}");
        //}

        public IActionResult Register(Member member,IFormFile Avatar) 
        {
            //if (string.IsNullOrEmpty(_user.Name))
            //{
            //    _user.Name = "Guest";
            //}
            //return Content($"Hello{_user.Name},{_user.Age}歲了,電子郵件是{_user.Email},{_user.Avatar?.FileName}-{_user.Avatar?.Length}-{_user.Avatar?.ContentType}");
            if (Avatar == null) return Content("請上船檔案");

            return View();
            
            
        }

        [HttpPost]
        public IActionResult Spot([FromBody]SearchDto _search)
        {
            //根據分類取景點資料
            var spots = _search.CategoryId == 0? _dbContext.SpotImagesSpots:_dbContext.SpotImagesSpots.Where(s=>s.CategoryId== _search.CategoryId);

            if (!string.IsNullOrEmpty(_search.Keyword))
            {
                spots = spots.Where(s => s.SpotTitle.Contains(_search.Keyword) || s.SpotDescription.Contains(_search.Keyword));
            }

            switch (_search.SortBy)
            {
                case "spotTitle":
                    spots = _search.SoryType == "asc" ? spots.OrderBy(s => s.SpotTitle) : spots.OrderByDescending(s => s.SpotTitle);
                    break;
                case "categoryId":
                    spots = _search.SoryType == "asc" ? spots.OrderBy(s => s.CategoryId) : spots.OrderByDescending(s => s.CategoryId);
                    break;
                default:
                    spots = _search.SoryType =="asc"?
                        spots.OrderBy(s=>s.SpotId):spots.OrderByDescending
                        (s=>s.SpotId);
                    break;
            }
            
            int TotalCount = spots.Count();
            int pageSize = _search.PageSize ?? 9;
            int TotalPages = (int)Math.Ceiling((decimal)TotalCount / pageSize);
            int page = _search.Page ?? 1;

            spots = spots.Skip((page-1)*pageSize).Take(pageSize);

            SpotsPagingDTO spotsPaging = new SpotsPagingDTO();
            spotsPaging.TotalPages = TotalPages;
            spotsPaging.SpotsResult=spots.ToList();
            
            
            return Json(spotsPaging);

        }

        
    }
}
