﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Masuit.Tools.Core.Linq;
using Masuit.Tools.Logging;
using Microsoft.AspNetCore.Mvc;
using Quick.IService;
using Quick.Models.Dto;
using Quick.Models.Entity.Table;
using QuickWeb.Controllers.Common;
using QuickWeb.Extensions;
using QuickWeb.Extensions.Common;
using QuickWeb.Models.ViewModel;

namespace QuickWeb.Controllers
{
    /// <summary>
    /// 商品管理
    /// </summary>
    public class GoodsController : AdminBaseController
    {
        /// <summary>
        /// yoshop_category对象业务方法
        /// </summary>
        public Iyoshop_categoryService CategoryService { get; set; }

        /// <summary>
        /// yoshop_goods对象业务方法
        /// </summary>
        public Iyoshop_goodsService GoodsService { get; set; }

        /// <summary>
        /// yoshop_upload_file对象业务方法
        /// </summary>
        public Iyoshop_upload_fileService UploadFileService { get; set; }

        #region 商品管理
        /// <summary>
        /// 商品列表页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region 商品分类管理
        /// <summary>
        /// 商品分类列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/goods.category/index")]
        public async Task<IActionResult> CategoryIndex()
        {
            var list = await GetCategories(l => true);
            return View(list);
        }

        /// <summary>
        /// 商品分类添加页面
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("/goods.category/add")]
        public async Task<IActionResult> CategoryAdd()
        {
            var list = await GetCategories(l => l.parent_id == 0);
            ViewData["first"] = list;
            return View(new CategoryDto());
        }

        /// <summary>
        /// 添加商品分类
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost, Route("/goods.category/add")]
        public IActionResult CategoryAdd(CategoryDto viewModel)
        {
            viewModel.wxapp_id = GetAdminSession().wxapp_id;
            viewModel.create_time = DateTime.Now;
            viewModel.update_time = DateTime.Now;
            try
            {
                var model = viewModel.Mapper<yoshop_category>();
                CategoryService.AddEntity(model);
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }

            return YesRedirect("添加成功！", "/goods.category/index");
        }

        /// <summary>
        /// 商品分类编辑页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet, Route("/goods.category/edit/category_id/{id}")]
        public async Task<IActionResult> CategoryEdit(uint id)
        {
            var model = CategoryService.GetById(id).Mapper<CategoryDto>();
            if (model == null) return NoOrDeleted();
            var file = await UploadFileService.GetFirstEntityAsync(l=>l.file_id == model.image_id);
            ViewData["file_url"] = file?.file_url;
            var list = await GetCategories(l => l.parent_id == 0);
            ViewData["first"] = list;
            return View(model);
        }

        /// <summary>
        /// 编辑商品分类
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, Route("/goods.category/edit/category_id/{id}")]
        public IActionResult CategoryEdit(CategoryDto viewModel, uint id)
        {
            var model = CategoryService.GetById(id);
            if (model == null) return NoOrDeleted();
            try
            {
                model.name = viewModel.name;
                model.parent_id = viewModel.parent_id;
                model.sort = viewModel.sort;
                model.image_id = viewModel.image_id;
                model.update_time = DateTime.Now.ConvertToTimeStamp();
                CategoryService.UpdateEntity(model);
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return YesRedirect("编辑成功！", "/goods.category/index");
        }

        /// <summary>
        /// 删除商品分类
        /// </summary>
        /// <param name="category_id"></param>
        /// <returns></returns>
        [HttpPost, Route("/goods.category/delete")]
        public IActionResult CategoryDelete(uint category_id)
        {
            try
            {
                CategoryService.Delete(l => l.category_id == category_id);
            }
            catch (Exception e)
            {
                LogManager.Error(GetType(), e);
                return No(e.Message);
            }
            return YesRedirect("删除成功！", "/goods.category/index");
        }


        private async Task<List<CategoryDto>> GetCategories(Expression<Func<yoshop_category, bool>> where)
        {
            Expression<Func<yoshop_category, bool>> cond = l => l.wxapp_id == GetAdminSession().wxapp_id;
            cond = cond.And(where);
            var list = await CategoryService.LoadOrderedEntities<int>(cond, s => s.sort, true).ToListAsync();
            return list.Mapper<List<CategoryDto>>();
        }

        #endregion
    }
}