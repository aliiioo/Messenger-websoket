using Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Multi_Shop.Context.Entites;
using Multi_Shop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Multi_Shop.Controllers
{
    [Authorize(Roles = "Onwer,Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IShopservice _shopservice;



        public AdminController(IShopservice shopservice)
        {
            _shopservice = shopservice;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult indexOfGroups()
        {
            var result=_shopservice.GetGrtoups(User.Identity.Name);
            return View(result);
        }

        public IActionResult ShopIndex()
        {
            return View(_shopservice.GetAllProducts());
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult AddProducts()
        {
            var groups = _shopservice.GetgroupForManage();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text");
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult AddProducts(AddProductVM model1,IFormFile imgCourseUp, string Sll,string username)
        {
           
            if (!ModelState.IsValid)
            {
                return View(model1);
            }
            var model = new Shop()
            {
                //CreatDate = model1.CreatDate,
                //Description = Sll,
                GroupId = model1.GroupId,
                //Number = model1.Number,
                //Price = model1.Price,
                ShopImg = model1.ShopImg,
                ShopTitle = model1.ShopTitle,
                Number = 100,
                Color = "red",
                Size = "ls",
                //Tag = model1.Tag,
                //Color=model1.Color,
                //Size=model1.Size
            };
            
            
            _shopservice.AddProductss(model, imgCourseUp);
            _shopservice.AddAdminOfgroup(model.ShopId, username);
            return RedirectToAction("ShopIndex");
        }
        [HttpGet]
        public IActionResult AddGroup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddGroup(string NameGroup)
        {
            var gr = new ShopGroup()
            {
                GroupTitle = NameGroup,
                ParentId = null,
                IsDelete = false
            };
            _shopservice.AddGroup(gr);
            return RedirectToAction("ShopIndex");
        }

        public IActionResult DeleteProduct(int id)
        {
            var admin=_shopservice.GetAdminofgroup(User.Identity.Name);
            if (admin==true)
            {
                 _shopservice.DeleteProduct(id);
            return RedirectToAction("ShopIndex");
            }
            return NotFound();
        }

        public IActionResult EditProduct(int id)
        {
            var pro=_shopservice.GetProductById(id);
            var VM = new AddProductVM()
            {
                CreatDate = pro.CreatDate,
                Description = pro.Description,
                GroupId = pro.GroupId,
                Number = pro.Number,
                Price = pro.Price,
                ShopImg = pro.ShopImg,
                ShopTitle = pro.ShopTitle,
                Tag = pro.Tag
            };
            ViewData["Ids"] = id;
            var groups = _shopservice.GetgroupForManage();
            ViewData["Groups"] = new SelectList(groups, "Value", "Text");
            return View(VM);
        }
        [HttpPost]
        public  IActionResult EditProduct(AddProductVM model1, IFormFile imgCourseUp,int IdShop, string SllEdit)
        {
            if (!ModelState.IsValid)
            {
                return View(model1);
            }
            var model = new Shop()
            {
                ShopId = IdShop,
                CreatDate = model1.CreatDate,
                Description = SllEdit,
                GroupId = model1.GroupId,
                Number = model1.Number,
                Price = model1.Price,
                ShopImg = model1.ShopImg,
                ShopTitle = model1.ShopTitle,
                Tag = model1.Tag
            };
            _shopservice.EditProduct(model, imgCourseUp);
            return RedirectToAction("ShopIndex");
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult JoinNewAdmin(int GroupId,string username)
        {
            var RealAdmin=_shopservice.GetAdminOfgroupsespitial(GroupId,username);
            if (RealAdmin == false)
            {
                return NotFound();
            }
            return View(GroupId);
        }

        [HttpPost]
        public IActionResult JoinNewAdmin(int GroId,string UserAdmin,int namm)
        {
            namm = 1;
              _shopservice.AddAdminOfgroup(GroId, UserAdmin);
            return View(GroId);
        }







    }
}
