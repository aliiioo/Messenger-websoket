using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Multi_Shop.Context.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Multi_Shop.Repository
{
    public interface IShopservice
    {
        #region Gruop
        List<ShopGroup> GetShopGroups();
        public List<SelectListItem> GetgroupForManage();
        public int GetNum_Group(int id);
        public void AddGroup(ShopGroup group);
        #endregion
        #region Products
        public int AddProductss(Shop shop, IFormFile formFile);
        public List<Shop> GetAllProducts(int pageId = 1);
        public Shop GetProductById(int id); 
        public void DeleteProduct(int id);
        public int EditProduct(Shop shop, IFormFile formFile);
        public Tuple<List<Shop>,int> GetProductsForShow(int pageId = 1, string filter = "", int GroupId= 2);
        public int GetCounterPage();
        public string GetGroupName(int id);
        public void addLike(string UserName,int id);
        public int GetLikes(int id);
        public List<ProductComment> GetComment(int id);
        public void addComment(ProductComment productComment);
        public void DleteComment(int id);
        public int GetNumberOfComment(int proId);
        public int GetGroupForFilter(string Filter);

        #endregion
        #region User
        public void AddCoustemUser(UserList userList);
        #endregion

        #region cart
        public void AddCart(CartIidd cart);
        public void RemoveCart(string UserName);
        public List<CartIidd> GetCartByUserName(string UserName);
        public void RemoveJustOneCart(int id);
        #endregion



        #region Websocket
        public void AddAdminOfgroup(int grid, string Username);
        public bool GetAdminofgroup(string Usernamr);
        public List<Shop> GetGrtoups(string Username);
        public bool GetAdminOfgroupsespitial(int grid, string Username);
        public void JoinUserForGroup_Chanel(int proid, string Username);
        public List<Shop> GetGroup_chanelforUser(string username);

        public void uploadfile(IFormFile formFile, string UserName);
        public List<UpoladFile> GetUploadFile(string username);

        public void savemessage(string username,string message);

        #endregion
    }
}
