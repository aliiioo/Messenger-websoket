using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Multi_Shop.Context;
using Multi_Shop.Context.Entites;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Multi_Shop.Repository
{
    public class Shopservice : IShopservice
    {
        private MultiShopContext _context;
        public Shopservice(MultiShopContext context)
        {
            _context = context;
        }

         public List<Shop> GetGrtoups(string Username)
        {
           List<int> groupid= (List<int>)_context.ProductsOfAdmins.Where(d=>d.Username==Username).Select(d=>d.ProId).ToList();
            List<Shop> shoping = new List<Shop>();    
            foreach (var item in groupid)
        	{
                var result=_context.Shops.SingleOrDefault(d=>d.ShopId==item);
                shoping.Add(result);
	        }
            return shoping;

        }

        public void AddAdminOfgroup(int grid, string Username)
        {
            
            var adminofpro = new ProductsAdmins()
            {
                ProId = grid,
                Username = Username
            };
            _context.ProductsOfAdmins.Add(adminofpro);
            _context.SaveChanges();
        }

        public void AddCart(CartIidd cart)
        {
            _context.CartI.Add(cart);
            _context.SaveChanges();
        }

        public void addComment(ProductComment productComment)
        {
            _context.Add(productComment);
            _context.SaveChanges();
        }

        public void AddCoustemUser(UserList userList)
        {
            _context.Usersss.Add(userList);
            _context.SaveChanges();
        }

        public void AddGroup(ShopGroup group)
        {
            _context.ShopGroups.Add(group);
            _context.SaveChanges();
        }

        public void addLike(string UserName, int id)
        {
            var UserLike = _context.Likes.SingleOrDefault(c => c.UserName1== UserName && c.ShopId == id);
            if (UserLike == null)
            {
                UserLike = new Likes()
                {
                    ShopId = id,
                    UserName1 = UserName,
                    Like = true
                };
                _context.Add(UserLike);
            }
            _context.SaveChanges();

        }

        public int AddProductss(Shop shop, IFormFile formFile)
        {
            shop.ShopImg = "offer-1.jpg";
            if (formFile != null)
            {
                shop.ShopImg = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(formFile.FileName);

                string ImgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Products", shop.ShopImg);
                using (var Stream = new FileStream(ImgPath, FileMode.Create))
                {
                    formFile.CopyTo(Stream);
                }
            }
            _context.Add(shop);
            _context.SaveChanges();
            return shop.ShopId;
        }


        public void uploadfile( IFormFile formFile, string UserName)
        {
            string name="0";
            if (formFile != null)
            {
                name= Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(formFile.FileName);

                string ImgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/WS", name);
                using (var Stream = new FileStream(ImgPath, FileMode.Create))
                {
                    formFile.CopyTo(Stream);
                }
            }
            var up = new UpoladFile()
            {
                FormUsername = UserName,
                Name = name,
            };
            _context.upoladFiles.Add(up);
            _context.SaveChanges();

        }


        public void DeleteProduct(int id)
        {
            var pro = GetProductById(id);
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Products", pro.ShopImg + ".jpg");
            if (System.IO.File.Exists(FilePath))
            {
                System.IO.File.Delete(FilePath);
            }
            _context.Shops.Remove(pro);
            _context.SaveChanges();
        }

        public void DleteComment(int id)
        {
            var selec=_context.Comments.SingleOrDefault(c => c.CommentId == id);
            _context.Comments.Remove(selec);
            _context.SaveChanges();
        }

        public int EditProduct(Shop shop, IFormFile formFile)
        {
            var E_pro = GetProductById(shop.ShopId);
            E_pro.CreatDate = shop.CreatDate;
            E_pro.Description = shop.Description;
            E_pro.GroupId = shop.GroupId;
            E_pro.Number = shop.Number;
            E_pro.Price = shop.Price;
            E_pro.ShopTitle = shop.ShopTitle;
            E_pro.Tag = shop.Tag;
            if (formFile != null)
            {
                string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Products", E_pro.ShopImg);
                if (System.IO.File.Exists(FilePath))
                {
                    System.IO.File.Delete(FilePath);
                }
                E_pro.ShopImg = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(formFile.FileName);

                string ImgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Products", E_pro.ShopImg);
                using (var Stream = new FileStream(ImgPath, FileMode.Create))
                {
                    formFile.CopyTo(Stream);
                }
            }
            _context.Update(E_pro);
            _context.SaveChanges();
            return E_pro.ShopId;
        }

        public bool GetAdminofgroup(string Usernamr)
        {
            var result = _context.ProductsOfAdmins.SingleOrDefault(d => d.Username == Usernamr);
            if (result!=null)
            {
                return true;
            }
            return false;
        }

        public List<Shop> GetAllProducts(int pageId = 1)
        {
            int skip = (pageId - 1) * 8;
            return _context.Shops.Skip(skip).Take(8).ToList();
        }

        public List<CartIidd> GetCartByUserName(string UserName)
        {
            return _context.CartI.Where(c => c.UserName == UserName).ToList();
        }

        public List<ProductComment> GetComment(int id)
        {
            var reslut = _context.Comments.Where(c => c.ProductId == id).ToList();
            return reslut;
        }

        public int GetCounterPage()
        {
            int pageCount = _context.Shops.Count() / 8;
            return pageCount;
        }

        public int GetGroupForFilter(string Filter)
        {
            int gro = _context.Shops.SingleOrDefault(s => s.ShopTitle.Contains(Filter)||s.Tag.Contains(Filter)).GroupId;
            return gro;
        }

        public List<SelectListItem> GetgroupForManage()
        {
            return _context.ShopGroups.Where(g => g.ParentId == null)
                .Select(p => new SelectListItem()
                {
                    Text = p.GroupTitle,
                    Value = p.GroupId.ToString()
                }).ToList();

        }

        public string GetGroupName(int id)
        {
            return _context.ShopGroups.Where(d => d.GroupId == id).Single().GroupTitle;
        }

        public int GetLikes(int id)
        {
            var num= _context.Likes.Where(c => c.ShopId == id).Select(c => c.Like).ToList();
            return num.Count(c => c == true);
            
        }

        public int GetNumberOfComment(int proId)
        {
            return _context.Comments.Where(c => c.ProductId == proId).Count();
        }

        public int GetNum_Group(int id)
        {
            var Num = _context.Shops.Where(c => c.GroupId == id).ToList();
            return Num.Count();
        }

        public Shop GetProductById(int id)
        {
            return _context.Shops.SingleOrDefault(c => c.ShopId == id);
        }

        public Tuple<List<Shop>, int> GetProductsForShow(int pageId = 1, string filter = "", int GroupId = 2)
        {
            IQueryable<Shop> result = _context.Shops;
            if (!string.IsNullOrEmpty(filter))
            {
                result = result.Where(a => a.ShopTitle.Contains(filter)||a.Tag.Contains(filter));
            }
            result = result.OrderByDescending(c => c.CreatDate);
            result = result.Where(x => x.GroupId == GroupId);

            int skip = (pageId - 1) * 8;
            int pageCount = result.Count() / 8;

            var query = result.Skip(skip).Take(8).ToList();

            return Tuple.Create(query, pageCount);
        }


        public List<ShopGroup> GetShopGroups()
        {
            return _context.ShopGroups.ToList();
        }

        public void RemoveCart(string UserName)
        {
            var carts=_context.CartI.Where(u=>u.UserName==UserName).ToList();
            foreach (var item in carts)
            {
                _context.CartI.Remove(item);
            }
            _context.SaveChanges();
        }

        public void RemoveJustOneCart(int id)
        {
            var cart= _context.CartI.Where(c => c.CarttId == id).Single();
            _context.Remove(cart);
            _context.SaveChanges();
        }

        public bool GetAdminOfgroupsespitial(int grid, string Username)
        {
            var result = _context.ProductsOfAdmins.SingleOrDefault(d => d.Username == Username&&d.ProId==grid);
            if (result != null)
            {
                return true;
            }
            return false;

        }

        public void JoinUserForGroup_Chanel(int proid, string Username)
        {
            var result = new UsersGroup_chanel()
            {
                ProId = proid,
                Username = Username
            };
            _context.usersGroup_Chanels.Add(result);
            _context.SaveChanges();
        }

        public List<Shop> GetGroup_chanelforUser(string username)
        {
            List<int> groupid = (List<int>)_context.usersGroup_Chanels.Where(d => d.Username == username).Select(d => d.ProId).ToList();
            List<Shop> shoping = new List<Shop>();
            foreach (var item in groupid)
            {
                var result = _context.Shops.SingleOrDefault(d => d.ShopId == item);
                shoping.Add(result);
            }
            return shoping;

        }

        public List<UpoladFile> GetUploadFile(string username)
        {
            return _context.upoladFiles.ToList();
        }

        public void savemessage(string username, string message)
        {
            var mes = new message()
            {
                messageing = message,
                username = username,
            };
            _context.messages.Add(mes);
            _context.SaveChanges();
        }


    }
}
