﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace QuickWeb.Controllers.Common
{
    /// <summary>
    /// 错误处理控制器
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// AccessNoRight
        /// </summary>
        /// <returns></returns>
        [Route("AccessNoRight")]
        public IActionResult AccessNoRight()
        {
            //Response.StatusCode = 401;
            if (Request.Method.ToLower().Equals("get"))
            {
                return View();
            }
            return Json(new
            {
                StatusCode = 401,
                Success = false,
                Message = "没有权限！"
            });
        }

        /// <summary>
        /// PageNotFound
        /// </summary>
        /// <returns></returns>
        [Route("PageNotFound")]
        public IActionResult PageNotFound()
        {
            //Response.StatusCode = 404;
            if (Request.Method.ToLower().Equals("get"))
            {
                return View();
            }
            return Json(new
            {
                StatusCode = 404,
                Success = false,
                Message = "页面未找到！"
            });
        }

        /// <summary>
        /// ServerError
        /// </summary>
        /// <returns></returns>
        [Route("ServerError")]
        public IActionResult ServerError()
        {
            //Response.StatusCode = 500;
            if (Request.Method.ToLower().Equals("get"))
            {
                return View();
            }
            return Json(new
            {
                StatusCode = 500,
                Success = false,
                Message = "服务器内部错误！"
            });
        }

        /// <summary>
        /// ServiceUnavailable
        /// </summary>
        /// <returns></returns>
        [Route("ServiceUnavailable")]
        public IActionResult ServiceUnavailable()
        {
            //Response.StatusCode = 503;
            if (Request.Method.ToLower().Equals("get"))
            {
                return View();
            }
            return Json(new
            {
                StatusCode = 503,
                Success = false,
                Message = "服务不可用！"
            });
        }

        /// <summary>
        /// 自定义错误页面1
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if (Request.Method.ToLower().Equals("get"))
            {
                return View();
            }
            return Json(new
            {
                status = 0,
                msg = "Error",
                data = "系统异常"
            });
        }

        /// <summary>
        /// 自定义错误页面2
        /// </summary>
        /// <returns></returns>
        public IActionResult ParamsError()
        {
            if (Request.Method.ToLower().Equals("get"))
            {
                return View("~/Views/Error/ParamsError.cshtml");
            }
            return Json(new
            {
                status = 0,
                msg = "ParamsError",
                data = "请求参数异常"
            });
        }

        /// <summary>
        /// 自定义错误页面2
        /// </summary>
        /// <returns></returns>
        public IActionResult NoOrDeleted()
        {
            if (Request.Method.ToLower().Equals("get"))
            {
                return View("~/Views/Error/NoOrDeleted.cshtml");
            }
            return Json(new
            {
                status = 0,
                msg = "NoOrDeleted",
                data = "请求的数据不存在或已经被删除"
            });
        }
    }
}