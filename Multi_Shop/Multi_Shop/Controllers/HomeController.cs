using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Multi_Shop.Context.Entites;
using Multi_Shop.Models;
using Multi_Shop.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Multi_Shop.Controllers
{
    public class HomeController : Controller
    {
        private IHostingEnvironment env { get; }
        private readonly ILogger<HomeController> _logger;
        private IShopservice _shopservice;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, IShopservice shopservice, SignInManager<IdentityUser> si, IHostingEnvironment env)
        {
            this.env = env;
            _signInManager = si;
            _shopservice = shopservice;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult Index(int PageId=1)
        {
            var products = _shopservice.GetAllProducts(PageId);
            var Category = _shopservice.GetShopGroups();
            ViewBag.Categoryies = Category;
            ViewBag.Pagecount = _shopservice.GetCounterPage();
            return View(products);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult Products(int pageId=1,string Filter="",int groupId=2)
        {

            if (!string.IsNullOrEmpty(Filter))
            {
                groupId = _shopservice.GetGroupForFilter(Filter);
            }
            ViewBag.Group = _shopservice.GetGroupName(groupId);
            ViewBag.Pagecount = _shopservice.GetCounterPage();
            var product = _shopservice.GetProductsForShow(pageId, Filter, groupId);
            return View(product.Item1);
        }

        public IActionResult ProductsOfwonUser(int pageId = 1, string Filter = "", int groupId = 2)
        {
            var result = _shopservice.GetGroup_chanelforUser(User.Identity.Name);
            return View(result);
        }


        public IActionResult DetailOfProducts(int id)
        {
            
            var pro = _shopservice.GetProductById(id);
            var permisson = _shopservice.GetAdminOfgroupsespitial(pro.ShopId, User.Identity.Name);
            var upload = _shopservice.GetUploadFile(User.Identity.Name);
            pro.Like = _shopservice.GetLikes(id);
            var Coloring = pro.Color.Split('-');
            var Sizeing = pro.Size.Split('-');
            ViewBag.upload=upload;
            ViewBag.permission = permisson;
            ViewBag.Idd = id;
            ViewBag.GroupId = pro.GroupId;
            ViewBag.Colo = Coloring;
            ViewBag.Siz = Sizeing;
            ViewBag.Num_comment = _shopservice.GetNumberOfComment(id);
            ViewBag.Commnt = _shopservice.GetComment(id);
            return View(pro);
        }
        [Authorize]
        public IActionResult Like(int iidd)
        {
            var Userid = _signInManager.UserManager.GetUserName(User);   
            _shopservice.addLike(Userid, iidd);
            ViewBag.ProId = iidd;
            return View();
        }

        [Authorize]
        public IActionResult CreatComment(string Sll,int Por,string nam)
        {
            var comment = new ProductComment()
            {
                Comment = Sll,
                CreateDate = DateTime.Now,
                IsDelete = false,
                UserName = nam,
                ProductId= Por
            };
            _shopservice.addComment(comment);
            ViewBag.ProId = Por;
            return View();
        }
        [Authorize(Roles ="Admin,Onwer")]
        public IActionResult DeleteComment(int id,int ProIId)
        {
            _shopservice.DleteComment(id);
            ViewBag.ProId = ProIId;
            return View("CreatComment");
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddCart(string color="red",string size="L",string ProName="Name",string Price="0",int NumberOfPro=1,string Img="a")
        {
            var user= _signInManager.UserManager.GetUserName(User);
            CartIidd cart = new CartIidd()
            {
                UserName = user,
                Coloring = color,
                Numbers = NumberOfPro,
                Price = Price,
                ProductName = ProName,
                Sizing = size,
                ImgName=Img
            };
            _shopservice.AddCart(cart);

            return RedirectToAction("IndexOfCart");
        }
        [Authorize]
        public IActionResult IndexOfCart()
        {
            var user = _signInManager.UserManager.GetUserName(User);
            if (user==""|| user==null)
            {
                return BadRequest();
            }
            var carts = _shopservice.GetCartByUserName(user);
            return View(carts);
        }
        [Authorize]
        public IActionResult DeleteCartJust1(int id)
        {
            _shopservice.RemoveJustOneCart(id);
            return RedirectToAction("IndexOfCart");
        }
        [Authorize]
        public IActionResult RemoveAllListOfCart()
        {
            var user = _signInManager.UserManager.GetUserName(User);
            _shopservice.RemoveCart(user);
            return RedirectToAction("IndexOfCart");
        }

       public IActionResult Join(int proid,string Username)
        {
            _shopservice.JoinUserForGroup_Chanel(proid, Username);
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //public JsonResult Upload1()
        //{
        //    string fileName = Request.Headers["X-File-Name"];
        //    string fileType = Request.Headers["X-File-Type"];
        //    int fileSize = Convert.ToInt32(Request.Headers["X-File-Size"]);
        //    //File's content is available in Request.InputStream property
        //    System.IO.Stream fileContent = Request.InputStream;
        //    //Creating a FileStream to save file's content
        //    System.IO.FileStream fileStream = System.IO.File.Create(Server.MapPath("~/") + fileName);
        //    fileContent.Seek(0, System.IO.SeekOrigin.Begin);
        //    //Copying file's content to FileStream
        //    fileContent.CopyTo(fileStream);
        //    fileStream.Dispose();
        //    return Json("File uploaded");
        //}
        //[HttpPost]
        //public async Task<IActionResult> Index(IList<IFormFile> files)
        //{
        //    foreach (IFormFile source in files)
        //    {
        //        string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.Trim('"');

        //        filename = this.EnsureCorrectFilename(filename);

        //        using (FileStream output = System.IO.File.Create(this.GetPathAndFilename(filename)))
        //            await source.CopyToAsync(output);
        //    }

        //    return this.View();
        //}

        //private string EnsureCorrectFilename(string filename)
        //{
        //    if (filename.Contains("\\"))
        //        filename = filename.Substring(filename.LastIndexOf("\\") + 1);

        //    return filename;
        //}

        //private string GetPathAndFilename(string filename)
        //{
        //    return this.hostingEnvironment.WebRootPath + "\\uploads\\" + filename;
        //}



        [HttpPost]
        public IActionResult uploader(IFormFile imgCourseUp,string UserName, string proid)
        {
            _shopservice.uploadfile(imgCourseUp, UserName);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult saveMessage(string message)
        {
            string username = "Ali";
            _shopservice.savemessage(username, message);    
            return View();
        }

        


    }
}
