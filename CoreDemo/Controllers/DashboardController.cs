using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class DashboardController : Controller
    {
        BlogManager blogManager = new BlogManager(new EfBlogRepository());
        CategoryManager categoryManager = new CategoryManager(new EfCategoryRepository());

        [AllowAnonymous]
        public IActionResult Index()
        {
            ViewBag.v1 = blogManager.GetTotalBlogCount();
            ViewBag.v2 = blogManager.GetBlogCountByWriter(1);
            ViewBag.v3 = categoryManager.GetTotalCategoryCount();
            return View();
        }
    }
}
