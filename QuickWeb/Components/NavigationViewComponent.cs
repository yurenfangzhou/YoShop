﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Quick.Models.Dto;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QuickWeb.Components
{
    /// <summary>
    /// 视图组件
    /// </summary>
    [ViewComponent(Name = "Navigation")]
    public class NavigationViewComponent : ViewComponent
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        /// <summary>
        /// 视图组件
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public NavigationViewComponent(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// 渲染菜单
        /// </summary>
        /// <returns></returns>
        [ResponseCache(VaryByHeader = "User-Agent", Duration = 60 * 10, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "json", "menus.json");
            var menus = await Task.Run(() => JsonConvert.DeserializeObject<MenuDto[]>(File.ReadAllText(filePath)).ToList());
            return View("Default", menus);
        }
    }
}
