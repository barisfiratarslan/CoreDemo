using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class BlogController : Controller
    {
        BlogManager blogManager = new BlogManager(new EfBlogRepository());

        public IActionResult Index()
        {
            var values = blogManager.GetBlogListWithCategory();
            return View(values);
        }

        public IActionResult BlogReadAll(int id)
        {
            ViewBag.i = id;
            var values = blogManager.GetBlogByID(id);
            return View(values);
        }

        public IActionResult BlogListByWriter()
        {
            var values = blogManager.GetListWithCategoryByWriter(1);
            return View(values);
        }

        [HttpGet]
        public IActionResult BlogAdd()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryRepository());
            List<SelectListItem> categoryValues = (from x in categoryManager.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
            ViewBag.cv = categoryValues;
            return View();
        }

        [HttpPost]
        public IActionResult BlogAdd(Blog blog)
        {
            BlogValidator validationRules = new BlogValidator();
            ValidationResult validationResult = validationRules.Validate(blog);

            if (validationResult.IsValid)
            {
                blog.BlogStatus = true;
                blog.BlogCreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                blog.WriterID = 1;
                blogManager.TAdd(blog);
                return RedirectToAction("BlogListByWriter", "Blog");
            }
            foreach (var item in validationResult.Errors)
            {
                ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            }
            CategoryManager categoryManager = new CategoryManager(new EfCategoryRepository());
            List<SelectListItem> categoryValues = (from x in categoryManager.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
            ViewBag.cv = categoryValues;
            return View();
        }

        public IActionResult DeleteBlog(int id)
        {
            var blogValue = blogManager.GetByID(id);
            blogManager.TDelete(blogValue);
            return RedirectToAction("BlogListByWriter");
        }

        [HttpGet]
        public IActionResult EditBlog(int id)
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryRepository());
            List<SelectListItem> categoryValues = (from x in categoryManager.GetList()
                                                   select new SelectListItem
                                                   {
                                                       Text = x.CategoryName,
                                                       Value = x.CategoryID.ToString()
                                                   }).ToList();
            ViewBag.cv = categoryValues;
            var blogValue = blogManager.GetByID(id);
            return View(blogValue);
        }

        [HttpPost]
        public IActionResult EditBlog(Blog blog)
        {
            blog.BlogStatus = true;
            blog.BlogCreateDate = blogManager.GetByID(blog.BlogID).BlogCreateDate;
            blog.WriterID = 1;
            blogManager.TUpdate(blog);
            return RedirectToAction("BlogListByWriter");
        }
    }
}
