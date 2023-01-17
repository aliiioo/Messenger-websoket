using Microsoft.AspNetCore.Mvc;
using Multi_Shop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Multi_Shop.ViewComponents
{
    public class ShopGroupComponent:ViewComponent
    {
        private IShopservice _shopservice;
        public ShopGroupComponent(IShopservice shopservice)
        {
            _shopservice = shopservice;
        }


        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult) View(("ShopGroup"),_shopservice.GetShopGroups()));
        }

    }
}
